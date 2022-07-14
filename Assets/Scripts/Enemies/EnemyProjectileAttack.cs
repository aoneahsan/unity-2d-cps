using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAttack : MonoBehaviour
{
  [SerializeField] GameObject projectilePrefab;
  Enemy enemy;

  private void Awake()
  {
    enemy = transform.parent.GetComponent<Enemy>();
  }

  public void AttackProjectile()
  {
    if (enemy == null || enemy.IsDead || projectilePrefab == null)
    {
      return;
    }

    Instantiate(projectilePrefab, transform.position, Quaternion.identity);
  }
}
