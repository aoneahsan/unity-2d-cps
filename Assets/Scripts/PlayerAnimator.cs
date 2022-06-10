using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
  Animator playerSpriteAnimator;
  void Start()
  {
    playerSpriteAnimator = GetComponentInChildren<Animator>();
  }

  public void SetRunAnimation(float moveSpeed)
  {
    playerSpriteAnimator.SetFloat("Move", moveSpeed);
  }

  public void SetJumpAnimation(bool state)
  {
    playerSpriteAnimator.SetBool("isJumping", state);
  }

  public void StartDeadAnimation()
  {
    playerSpriteAnimator.SetBool("IsDead", true);
  }

  public void StartAttackAnimation()
  {
    playerSpriteAnimator.SetTrigger("Attack");
  }
}
