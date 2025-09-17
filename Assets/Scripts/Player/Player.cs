using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent (typeof(CameraController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Mover _movement;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Transform _cameraTransform;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _movement = GetComponent<Mover>();
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

    private void Update()
    {
        _movement.Move(_playerInput.Player.Move.ReadValue<Vector2>());
        _cameraController.Look(_playerInput.Player.Look.ReadValue<Vector2>());
    }
}
