using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _sensivity;

    private float _cameraAngle;

    private void Awake()
    {
        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    public void Look(Vector2 input)
    {
        if (input.sqrMagnitude < Mathf.Epsilon)
            return;

        float scaledSensivity = _sensivity * Time.deltaTime;

        transform.Rotate(new Vector3(0, input.x, 0) * scaledSensivity);

        _cameraAngle -= input.y * scaledSensivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, -89, 89);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;
    }
}
