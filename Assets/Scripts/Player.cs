using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _interactAction;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed = 20f;
    [SerializeField] private float _gravity = 1f;

    [Space]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _cameraSpeed = 360f;
    private float _cameraAngleX = 0f;
    [SerializeField] private float _lookLowerLimit = -45f;
    [SerializeField] private float _lookUpperLimit = 75f;
    [Space]
    [SerializeField] private float _interactionRange = 10f;

    private bool _interacting = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _interactAction = InputSystem.actions.FindAction("Attack");
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

        if (moveValue != Vector2.zero) Move(moveValue);

        bool interactValue = _interactAction.IsPressed();

        if (interactValue != _interacting) Interact(interactValue);
    }

    private void Move(Vector2 value)
    {
        Vector3 velocity = new Vector3(value.x, -1f * _gravity, value.y) * _moveSpeed * Time.deltaTime;

        _characterController.Move(transform.rotation * velocity);
    }

    private void Look(Vector2 value)
    {
        transform.Rotate(new Vector3(0f, value.x, 0f));

        _cameraAngleX += value.y * _cameraSpeed * Time.deltaTime;
		_cameraAngleX = Mathf.Clamp (_cameraAngleX, _lookLowerLimit, _lookUpperLimit);
		_cameraTransform.localEulerAngles = new Vector3 (_cameraAngleX, 0.0f, 0.0f);
    }

    private void Interact(bool value)
    {
        _interacting = value;

        Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _interactionRange);

        if (hit.transform == null) return;

        bool movablePart = hit.transform.tag == "Puzzle_Movable";
        

    }
}
