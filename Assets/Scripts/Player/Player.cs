using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMove), typeof(PlayerAnimator), typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageable
{
  [SerializeField][Range(0, 100)] int health = 10;
  [SerializeField] int diamonds = 0;

  PlayerAnimator playerAnimator;
  Rigidbody2D playerRigidbody;
  PlayerInput playerInput;

  GameManager gameManager;
  public GameManager GameManager { get { return gameManager; } }


  bool isDead = false;
  public bool IsDead { get { return isDead; } }

  private void Awake()
  {
    gameManager = FindObjectOfType<GameManager>();
    playerAnimator = GetComponent<PlayerAnimator>();
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerInput = GetComponent<PlayerInput>();
  }

  public void Damage(int damageAmount)
  {
    if (!isDead)
    {
      health -= damageAmount;
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

    Debug.Log($"current diamonds: {diamonds}");
  }
}
