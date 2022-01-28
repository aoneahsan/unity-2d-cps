using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int enemyScorePoints = 100;

    public int GetEnemyScorePoints()
    {
        return enemyScorePoints;
    }
}
