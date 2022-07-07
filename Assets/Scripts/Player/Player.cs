using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMove), typeof(PlayerAnimator), typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageable
{
  [SerializeField] int diamonds = 0;
  public int Diamonds { get { return diamonds; } }

  PlayerAnimator playerAnimator;
  Rigidbody2D playerRigidbody;
  PlayerInput playerInput;

  GameManager gameManager;
  public GameManager GameManager { get { return gameManager; } }

  bool isDead = false;
  public bool IsDead { get { return isDead; } }
  bool hasFlameSward = false;
  public bool HasFlameSward { get { return hasFlameSward; } }
  bool hasBootsOfFlight = false;
  public bool HasBootsOfFlight { get { return hasBootsOfFlight; } }
  bool hasKeyToCastle = false;
  public bool HasKeyToCastle { get { return hasKeyToCastle; } }
  [SerializeField] int health = 0; // we will set this using gameUIManager, by getting the number of player health bars.

  private void Awake()
  {
    gameManager = FindObjectOfType<GameManager>();
    playerAnimator = GetComponent<PlayerAnimator>();
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerInput = GetComponent<PlayerInput>();
  }

  private void Start()
  {
    if (gameManager != null)
    {
      health = gameManager.GameUIManager.PlayerHealthBarCount;
    }
    UpdatePlayerBalance();
  }

  public void Damage(int damageAmount)
  {
    if (!isDead)
    {
      health -= damageAmount;
      if (gameManager != null)
      {
        gameManager.GameUIManager.UpdatePlayerHealthBar(health);
      }
      if (health <= 0)
      {
        Die();
      }
    }
  }

  public void Die()
  {
    if (isDead) return;

    if (playerAnimator != null)
    {
      playerAnimator.StartDeadAnimation();
    }

    isDead = true;
  }

  public void CollectPickable(PickableType pickableType, int amount)
  {
    if (amount < 1) return;

    switch (pickableType)
    {
      case PickableType.Diamond:
        diamonds += amount;
        break;
    }

    UpdatePlayerBalance();
  }

  private void UpdatePlayerBalance()
  {
    if (gameManager != null)
    {
      gameManager.GameUIManager.UpdatePlayerBalance(diamonds);
    }
  }

  public bool BuyItem(ShopItems shopItem, int price)
  {
    if (diamonds < price) return false;
    diamonds -= price;
    switch (shopItem)
    {
      case ShopItems.FlameSward:
        hasFlameSward = true;
        break;
      case ShopItems.BootsOfFlight:
        hasBootsOfFlight = true;
        break;
      case ShopItems.KeyToCastle:
        hasKeyToCastle = true;
        break;
    }
    return true;
  }
}
