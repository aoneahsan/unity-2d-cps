using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  [SerializeField][Range(0, 20)] int damage = 1;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<Player>().Damage(damage);
    }
  }
}
