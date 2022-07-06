using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
  [SerializeField] protected int amount = 1;
  [SerializeField] protected PickableType pickableType = PickableType.Diamond;

  public void SetAmount(int amount)
  {
    if (amount < 1) return;

    this.amount = amount;
  }

  public virtual void SetPickableType(PickableType pickableType)
  {
    this.pickableType = pickableType;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      Player player = other.gameObject.GetComponent<Player>();
      if (player != null)
      {
        player.CollectPickable(pickableType, amount);
        Destroy(gameObject);
      }
    }
  }
}
