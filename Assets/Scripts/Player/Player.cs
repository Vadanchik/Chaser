using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent (typeof(CameraController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Transform _cameraTransform;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _movement = GetComponent<Movement>();
        _cameraController = GetComponent<CameraController>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    void Update()
    {
        Debug.Log(_playerInput.Player.Move.ReadValue<Vector2>());

        _movement.Move(_playerInput.Player.Move.ReadValue<Vector2>());
        _cameraController.Look(_playerInput.Player.Look.ReadValue<Vector2>());
    }
}
