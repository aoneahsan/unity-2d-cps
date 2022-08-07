using System;
using UnityEngine;
using UnityEngine.Pool;

public class OP_Luncher : MonoBehaviour
{
  [SerializeField] OP_Bullet bulletPrefab;

  IObjectPool<OP_Bullet> bulletPool;

  void Awake()
  {
    bulletPool = new ObjectPool<OP_Bullet>(CreateBullet, HandleOnTakePoolItem, HandleOnReleasePoolItem, HandleOnDestroyPoolItem, true, 1, 6);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      bulletPool.Get();
    }
  }

  public OP_Bullet CreateBullet()
  {
    Debug.Log("[CreateBullet] called.");
    if (bulletPrefab != null)
    {
      OP_Bullet bullet = Instantiate(bulletPrefab);
      bullet.SetPool(bulletPool);

      return bullet;
    }
    else
    {
      Debug.LogError("[CreateBullet] - bulletPrefab is null.");
      return null;
    }
  }

  void HandleOnTakePoolItem(OP_Bullet bullet)
  {
    Debug.Log("[HandleOnTakePoolItem] called.");
    bullet.gameObject.SetActive(true);
    bullet.transform.position = Vector3.zero;
  }

  void HandleOnReleasePoolItem(OP_Bullet bullet)
  {
    Debug.Log("[HandleOnReleasePoolItem] called.");
    bullet.gameObject.SetActive(false);
  }

  void HandleOnDestroyPoolItem(OP_Bullet bullet)
  {
    Debug.Log("[HandleOnDestroyPoolItem] called.");
    Destroy(bullet.gameObject);
  }
}
