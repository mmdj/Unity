using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Monster : MonoBehaviour
{
    public GameObject MonsterPrefab { get; set; }
    public List<Transform> PatrolPoints { get; set; }
    public int CurrentPoint { get; set; }

    public Monster()
    {
        MonsterPrefab = null;
        PatrolPoints = null;
        CurrentPoint = 0;
    }
}

