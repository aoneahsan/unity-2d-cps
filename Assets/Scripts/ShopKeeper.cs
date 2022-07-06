using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
  GameManager gameManager;

  private void Awake()
  {
    gameManager = FindObjectOfType<GameManager>();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player" && gameManager != null && !gameManager.GameUIManager.IsShopOpen())
    {
      Debug.Log("player entered shop");
      gameManager.GameUIManager.OpenShop();
    }
  }
}
