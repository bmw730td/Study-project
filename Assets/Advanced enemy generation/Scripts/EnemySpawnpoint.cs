using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyPrefab;
    [SerializeField] private TargetMovement _enemyTarget;

    public void SummonEnemy()
    {
        var createdEnemy = Instantiate(_enemyPrefab, transform);

        createdEnemy.SetTarget(_enemyTarget.transform);
    }
}
