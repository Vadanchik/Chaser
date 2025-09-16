using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 input)
    {
        if (_characterController != null)
        {
            if (input.sqrMagnitude < 0.1f)
                return;

            float scaledMoveSpeed = _speed * Time.deltaTime;

            Vector3 diretion = transform.forward * input.y + transform.right * input.x;

            if (_characterController.isGrounded)
            {
                _characterController.Move(diretion * scaledMoveSpeed + Vector3.down);
            }
            else
            {
                _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
            }
        }
    }
}
