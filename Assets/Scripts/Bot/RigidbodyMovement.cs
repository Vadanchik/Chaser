using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _raycastDistance = 1.1f;
    [SerializeField] private float _stepHeight = 0.3f;
    [SerializeField, Range(0f, 90f)] private float _slopeLimit = 45f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rigidbody;
    private Vector3 _inputDirection;
    private Vector3 _surfaceNormal = Vector3.up;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        _inputDirection = direction.normalized;
    }

    public void Stop()
    {
        _inputDirection = Vector3.zero;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        bool isGrounded = Physics.Raycast(rayOrigin, Vector3.down, out hit, _raycastDistance, _groundLayer);

        if (isGrounded)
        {
            _surfaceNormal = hit.normal;
        }
        else
        {
            _surfaceNormal = Vector3.up;
        }

        float slopeAngle = Vector3.Angle(_surfaceNormal, Vector3.up);

        if (slopeAngle <= _slopeLimit && _inputDirection != Vector3.zero)
        {
            Vector3 projectedDirection = Vector3.ProjectOnPlane(_inputDirection, _surfaceNormal).normalized;
            Vector3 desiredVelocity = projectedDirection * _speed;

            if (isGrounded)
            {
                Vector3 stepRayOrigin = transform.position + Vector3.up * (_stepHeight + 0.1f);
                RaycastHit stepHit;
                if (Physics.Raycast(stepRayOrigin, Vector3.down, out stepHit, _stepHeight + 0.1f, _groundLayer))
                {
                    float stepUpAmount = stepHit.point.y - transform.position.y;
                    if (stepUpAmount > 0 && stepUpAmount <= _stepHeight)
                    {
                        _rigidbody.MovePosition(new Vector3(transform.position.x, stepHit.point.y, transform.position.z));
                    }
                }
            }

            _rigidbody.velocity = new Vector3(desiredVelocity.x, _rigidbody.velocity.y, desiredVelocity.z);
        }
        else
        {
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
        }

        if (_inputDirection != Vector3.zero)
        {
            _rigidbody.transform.forward = new Vector3(_inputDirection.x, 0, _inputDirection.z);
        }
    }
}