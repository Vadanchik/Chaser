using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        direction.y = 0;

        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f))
        {
            Vector3 normalSurface = hit.normal;
            Vector3 surfaceTangent = Vector3.ProjectOnPlane(direction, normalSurface).normalized;

            _rigidbody.velocity = surfaceTangent * _speed + Vector3.down;
            transform.forward = direction;
        }
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
