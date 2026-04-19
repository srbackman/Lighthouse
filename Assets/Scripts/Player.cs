using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _interactAction;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _gravity = 1f;
    [Space]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _cameraSpeed = 180f;
    private float _cameraAngleX = 0f;
    [SerializeField] private float _lookLowerLimit = -45f;
    [SerializeField] private float _lookUpperLimit = 75f;
    [Space]
    [SerializeField] private float _interactionRange = 10f;
    [Space]
    [SerializeField] private AudioSource _walkingAudio;
    [SerializeField] private float _walkSoundInterval = 0.75f;
    [SerializeField] private float _walkingPitchLow = 0.85f;
    [SerializeField] private float _walkingPitchHigh = 1.05f;

    private bool _interacting = false;
    private MovablePart _movablePart;
    private Coroutine _walkSoundCoroutine;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _interactAction = InputSystem.actions.FindAction("Attack");

        _interactAction.started += StartInteract;
        _interactAction.canceled += EndInteract;
    }

    void OnEnable()
    {
        // _moveAction.Enable();
        // _lookAction.Enable();
        // _interactAction.Enable();
    }

    void Update()
    {
        Vector2 lookValue = _lookAction.ReadValue<Vector2>();

        if (lookValue != Vector2.zero) Look(lookValue);

        Vector2 moveValue = _moveAction.ReadValue<Vector2>();

        Move(moveValue);

        if (_interacting && _movablePart) MoveInteract();
    }

    private void Move(Vector2 value)
    {
        Vector3 velocity = new Vector3(value.x, -1f * _gravity, value.y) * _moveSpeed * Time.deltaTime;

        _characterController.Move(transform.rotation * velocity);

        if (value == Vector2.zero && _walkSoundCoroutine != null)
        {
            StopCoroutine(_walkSoundCoroutine);
            _walkSoundCoroutine = null;
        }
        else if (value != Vector2.zero && _walkSoundCoroutine == null)
        {
            _walkSoundCoroutine = StartCoroutine(WalkingSounds());
        }
    }

    private void Look(Vector2 value)
    {
        transform.Rotate(new Vector3(0f, value.x * _cameraSpeed * Time.deltaTime, 0f));

        _cameraAngleX += value.y * -1f * _cameraSpeed * Time.deltaTime;
		_cameraAngleX = Mathf.Clamp (_cameraAngleX, _lookLowerLimit, _lookUpperLimit);
		_cameraTransform.localEulerAngles = new Vector3 (_cameraAngleX, 0.0f, 0.0f);
    }

    private void StartInteract(InputAction.CallbackContext context)
    {
         _interacting = true;

        Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _interactionRange);

        if (hit.transform == null) return;

        _movablePart = hit.transform.GetComponent<MovablePart>();

        if (_movablePart) return;

        Generator generator = hit.transform.GetComponent<Generator>();

        if (generator)
        {
            generator.TurnOn();
            return;
        }
    }

    private void EndInteract(InputAction.CallbackContext context)
    {
        _interacting = false;
        _movablePart = null;
    }

    private void MoveInteract()
    {
        Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _interactionRange);

        if (hit.transform == null) return;
        
        _movablePart?.Move(hit.point);
    }

    private IEnumerator WalkingSounds()
    {
        if (!_walkingAudio) yield break;

        float timer = 0f;

        while (true)
        {
            if (timer > _walkSoundInterval)
            {
                timer -= _walkSoundInterval;
                _walkingAudio.pitch = Random.Range(_walkingPitchLow, _walkingPitchHigh);
                _walkingAudio.Play();
            }

            yield return null;

            timer += Time.deltaTime;
        }
    }
}
