using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class OP_Bullet : MonoBehaviour
{
  [SerializeField] Vector3 speed = new Vector3(10, 0, 0);

  IObjectPool<OP_Bullet> bulletPool;


  void Update()
  {
    transform.position += speed * Time.deltaTime;
  }

  void OnBecameInvisible()
  {
    Debug.Log("[OnBecameInvisible] - called.");
    if (bulletPool != null)
    {
      Debug.Log("[OnBecameInvisible] - releasing this bullet.");
      bulletPool.Release(this);
    }
  }

  public void SetPool(IObjectPool<OP_Bullet> pool)
  {
    Debug.Log("[SetPool] - Setting Pool On Bullet");
    bulletPool = pool;
  }
}
