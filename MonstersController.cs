using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monster))]

public class MonstersController : MonoBehaviour
{
    [SerializeField] private GameObject _monsterPrefab;
    [SerializeField] private int _spawnCount;

    private const float SPAWN_TIME = 2f;
    private const float MONSTER_SPEED = 1f;
    private const float MONSTER_POINT_SENSITIVITY = 0.2f;

    private List<Monster> _monsters;

    private void Start()
    {
        _monsters = new List<Monster>();
        StartCoroutine(SpawnMonsters());
    }

    private void Update()
    {
        if (_monsters != null && _monsters.Count > 0)
        {
            foreach (Monster monster in _monsters)
            {
                if (monster.PatrolPoints == null || monster.PatrolPoints.Count == 0)
                    continue;

                Transform target = monster.PatrolPoints[monster.CurrentPoint];
                Vector3 targetPos = new Vector3(target.position.x, monster.MonsterPrefab.transform.position.y, monster.MonsterPrefab.transform.position.z);
                monster.MonsterPrefab.transform.position = Vector3.MoveTowards(monster.MonsterPrefab.transform.position, targetPos, MONSTER_SPEED * Time.deltaTime);
                
                if (Vector3.Distance(monster.MonsterPrefab.transform.position, targetPos) <= MONSTER_POINT_SENSITIVITY)
                {
                    monster.CurrentPoint++;
                    if (monster.CurrentPoint >= monster.PatrolPoints.Count)
                    {
                        monster.CurrentPoint = 0;
                    }
                }
            }
        }
    }

    private IEnumerator SpawnMonsters()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(SPAWN_TIME);
        Transform[] spawnPlaces = GetComponentsInChildren<Transform>();

        for (int i = 0; i < _spawnCount; i++)
        {
            foreach (Transform child in spawnPlaces)
            {
                if (child.CompareTag("SpawnPlace"))
                {
                    Monster monster = child.gameObject.AddComponent<Monster>();
                    monster.MonsterPrefab = Instantiate(_monsterPrefab, child.position, Quaternion.identity, child);
                    monster.MonsterPrefab.layer = LayerMask.NameToLayer("Monsters");
                    monster.PatrolPoints = GetPatrolPoints(child);

                    _monsters.Add(monster);

                    yield return waitForSeconds;
                }
            }
        }
    }

    private List<Transform> GetPatrolPoints(Transform child)
    {
        Transform[] patrolPoints = child.GetComponentsInChildren<Transform>();
        List<Transform> pointsList = new List<Transform>(child.childCount);

        foreach (Transform point in patrolPoints)
        {
            if (point.CompareTag("Point"))
            {
                pointsList.Add(point);
            }
        }
        return pointsList;
    }

}
