using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  [SerializeField] int damageAmount = 1;

  [SerializeField] bool canHit = true;

  void OnTriggerEnter2D(Collider2D other)
  {
    IDamageable hit = other.GetComponent<IDamageable>();
    if (hit != null && canHit)
    {
      StartCoroutine(DelayAttack());
      hit.Damage(damageAmount);

    }
  }

  IEnumerator DelayAttack()
  {
    canHit = false;
    yield return new WaitForSeconds(.45f);
    canHit = true;
  }
}
