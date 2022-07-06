using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasableItem : MonoBehaviour
{
  [SerializeField] int price = 100;

  public int Price
  {
    get
    {
      return price;
    }
  }
}
