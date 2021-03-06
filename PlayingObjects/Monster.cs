using UnityEngine;

public class Monster : MonoBehaviour
{
    private PatrolPoint[] _patrolPoints;
    private int _currentPoint;

    private const float _monsterSpeed = 1f;
    private const float _monsterPointSensitivity = 0.2f;

    private void Start()
    {
        _currentPoint = 0;
        _patrolPoints = transform.parent.GetComponentsInChildren<PatrolPoint>();
    }

    private void Update()
    {
        MoveToNextPatrolPoint();  
    }

    private void MoveToNextPatrolPoint()
    {
        if (_patrolPoints == null || _patrolPoints.Length == 0) return;

        PatrolPoint target = _patrolPoints[_currentPoint];
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _monsterSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) <= _monsterPointSensitivity)
        {
            _currentPoint++;

            if (_currentPoint >= _patrolPoints.Length)
            {
                _currentPoint = 0;
            }
        }
    }
}

