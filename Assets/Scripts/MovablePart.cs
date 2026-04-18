using UnityEngine;

public class MovablePart : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _speed = 1f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;

        _rigidbody.AddForce(direction * _speed);
    }
}
