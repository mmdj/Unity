using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject MonsterPrefab { get; set; }

    private PatrolPoint[] _patrolPoints;
    private int _currentPoint;

    private const float MONSTER_SPEED = 1f;
    private const float MONSTER_POINT_SENSITIVITY = 0.2f;

    public void Init()
    {
        _currentPoint = 0;
        _patrolPoints = transform.GetComponentsInChildren<PatrolPoint>();
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
        MonsterPrefab.transform.position = Vector3.MoveTowards(MonsterPrefab.transform.position, targetPos, MONSTER_SPEED * Time.deltaTime);

        if (Vector3.Distance(MonsterPrefab.transform.position, targetPos) <= MONSTER_POINT_SENSITIVITY)
        {
            _currentPoint++;
            if (_currentPoint >= _patrolPoints.Length)
            {
                _currentPoint = 0;
            }
        }
    }

}

