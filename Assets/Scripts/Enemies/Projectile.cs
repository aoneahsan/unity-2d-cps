using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] int damageAmount = 1;
  [SerializeField] float projectileLifeTime = 3.5f;
  [SerializeField] float speed = 3f;

  private void Start()
  {
    Destroy(gameObject, projectileLifeTime);
  }

  private void Update()
  {
    transform.Translate(Vector3.right * speed * Time.deltaTime);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Player player = other.gameObject.GetComponent<Player>();
      if (player != null || !player.IsDead)
      {
        player.Damage(damageAmount);
      }
      Destroy(gameObject);
    }
  }
}
