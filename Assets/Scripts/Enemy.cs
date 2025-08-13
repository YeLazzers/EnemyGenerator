using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private FollowMover _followMover;

    [SerializeField] private float _speed;

    public UnityAction<Enemy> TargetReached;

    private void Awake()
    {
        _followMover = new FollowMover(this.transform, _speed);
    }

    private void Update()
    {
        _followMover.Update(Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Target target))
        {
            TargetReached?.Invoke(this);
        }
    }


    public Enemy Initialize(Target target)
    {
        _followMover.SetTarget(target);

        return this;
    }
}
