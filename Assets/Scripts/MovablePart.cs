using UnityEngine;

public class MovablePart : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CharacterController _characterController;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxSpeed = 2f;
    [SerializeField] private AudioSource _moveSound;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();

        _rigidbody.maxLinearVelocity = _maxSpeed;
    }

    public void StartMove() { _moveSound?.Play(); }

    public void Move(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;

        _rigidbody.AddForce(direction * _speed);
    }
}
