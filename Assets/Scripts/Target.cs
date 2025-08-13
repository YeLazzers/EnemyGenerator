using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed = 0f;


    private float _reachDistance = 0.1f;
    private int _currentWaypointIndex = 0;

    private void Awake()
    {
        if (_waypoints.Count > 0)
        {
            transform.position = _waypoints[_currentWaypointIndex].position;
            StartCoroutine(WalkToWaypoint(GetNextWaypointPosition()));
        }
    }

    private IEnumerator WalkToWaypoint(Vector3 nextPosition)
    {
        transform.LookAt(nextPosition);
        
        while ((transform.position - nextPosition).magnitude >= _reachDistance)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            yield return null;
        }
        _currentWaypointIndex++;
        StartCoroutine(WalkToWaypoint(GetNextWaypointPosition()));
    }

    private Vector3 GetNextWaypointPosition()
    {
        int _nextWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;

        return _waypoints[_nextWaypointIndex].position;
    }
}
