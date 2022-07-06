using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
  [SerializeField] GameObject shopUI;
  [SerializeField] TextMeshProUGUI playerDiamondsBalanceText;

  GameManager gameManager;

  private void Awake()
  {
    CloseShop();
  }

  private void Start()
  {
    gameManager = GetComponent<GameManager>();
  }

  public bool IsShopOpen()
  {
    if (shopUI != null)
    {
      return shopUI.activeInHierarchy;
    }
    else
    {
      return false;
    }
  }

  public void OpenShop()
  {
    if (shopUI != null && gameManager != null && gameManager.Player != null && playerDiamondsBalanceText != null)
    {
      playerDiamondsBalanceText.text = $"{gameManager.Player.Diamonds}G";
      shopUI.SetActive(true);
    }
  }

  public void CloseShop()
  {
    if (shopUI != null)
    {
      shopUI.SetActive(false);
    }
  }
}
