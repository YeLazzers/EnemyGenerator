using UnityEngine;

public class FollowMover
{
    private Transform _characterTransform;
    private Target _target;
    private float _movementSpeed;

    public FollowMover(Transform characterTransform, float movementSpeed)
    {
        _characterTransform = characterTransform;
        _movementSpeed = movementSpeed;
    }

    public void SetTarget(Target target) => _target = target;

    public void Update(float deltaTime)
    {
        if (_target != null)
        {
            _characterTransform.LookAt(_target.transform.position);
            _characterTransform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
        }
    }
}
