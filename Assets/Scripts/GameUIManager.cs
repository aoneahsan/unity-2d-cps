using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameUIManager : MonoBehaviour
{
  [Header("GameOver UI")]
  [SerializeField] GameObject gameOverUI;

  [Header("Shop UI Elements")]
  [SerializeField] GameObject shopUI;
  [SerializeField] GameObject purchaseSuccessMessage;
  [SerializeField] GameObject purchaseFailedMessage;
  [SerializeField][Tooltip("this is for shop UI, so we can show player balance when shop opens.")] TextMeshProUGUI playerBalanceTextShopUI;

  [Header("Player UI Elements")]
  [SerializeField] GameObject[] playerHealthBars;
  public int PlayerHealthBarCount { get { return playerHealthBars.Length; } }
  [SerializeField][Tooltip("This is to show player balance below player health bars.")] TextMeshProUGUI playerBalanceText;

  GameManager gameManager;

  private void Awake()
  {
    ClearScreen();
  }

  private void ClearScreen()
  {
    CloseShop();
    CloseGameOverScreen();
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
    if (shopUI != null && gameManager != null && gameManager.Player != null && playerBalanceTextShopUI != null)
    {
      playerBalanceTextShopUI.text = $"{gameManager.Player.Diamonds}G";
      shopUI.SetActive(true);

      if (gameManager.AdsManager != null)
      {
        gameManager.AdsManager.LoadAd();
      }
    }
  }

  public void CloseShop()
  {
    if (shopUI != null)
    {
      shopUI.SetActive(false);
    }
    if (purchaseSuccessMessage != null)
    {
      purchaseSuccessMessage.SetActive(false);
    }
    if (purchaseFailedMessage != null)
    {
      purchaseFailedMessage.SetActive(false);
    }
  }

  public void ShowPurchaseSuccessMessage()
  {
    if (shopUI != null)
    {
      shopUI.SetActive(false);
    }
    if (purchaseSuccessMessage != null)
    {
      purchaseSuccessMessage.SetActive(true);
    }
    if (purchaseFailedMessage != null)
    {
      purchaseFailedMessage.SetActive(false);
    }
  }

  public void ShowPurchaseFailedMessage()
  {
    if (shopUI != null)
    {
      shopUI.SetActive(false);
    }
    if (purchaseSuccessMessage != null)
    {
      purchaseSuccessMessage.SetActive(false);
    }
    if (purchaseFailedMessage != null)
    {
      purchaseFailedMessage.SetActive(true);
    }
  }

  public void UpdatePlayerHealthBar(int health)
  {
    if (playerHealthBars.Length > 0)
    {
      for (int i = 0; i < playerHealthBars.Length; i++)
      {
        if (i < health)
        {
          playerHealthBars[i].SetActive(true);
        }
        else
        {
          playerHealthBars[i].SetActive(false);
        }
      }
    }
  }

  public void UpdatePlayerBalance(int diamonds)
  {
    if (playerBalanceText != null)
    {
      playerBalanceText.text = $"{diamonds}G";
    }
  }

  public void ShowGameOverScreen()
  {
    if (gameOverUI != null)
    {
      gameOverUI.SetActive(true);
    }
  }

  public void CloseGameOverScreen()
  {
    if (gameOverUI != null)
    {
      gameOverUI.SetActive(false);
    }
  }

  public void LoadMenuScene()
  {
    SceneManager.LoadScene("Menu");
  }

  public void LoadGameScene()
  {
    SceneManager.LoadScene("Game");
  }

  public void CloseGame()
  {
    Application.Quit();
  }

  public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
