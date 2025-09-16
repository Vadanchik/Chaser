using UnityEngine;

[RequireComponent(typeof(RigidbodyMovement))]
[RequireComponent(typeof(Chaser))]
public class Bot : MonoBehaviour
{
    [SerializeField] private float _distanceToStop;

    private RigidbodyMovement _movement;
    private Chaser _chaser;

    private void Awake()
    {
        _movement = GetComponent<RigidbodyMovement>();
        _chaser = GetComponent<Chaser>();
    }

    private void Update()
    {
        if (_chaser.GetDistance() > _distanceToStop)
        {
            _movement.Move(_chaser.GetDirection());
        }
        else
        {
            _movement.Stop();
        }
    }
}
