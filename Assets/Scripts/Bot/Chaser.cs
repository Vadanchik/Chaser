using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private Transform _target;


    public float GetDistance()
    {
        return (_target.position - transform.position).magnitude;
    }
    public Vector3 GetDirection()
    {
        return (_target.position - transform.position).normalized;
    }
}
