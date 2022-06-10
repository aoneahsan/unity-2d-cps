using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
  [Tooltip("Add movespeed we will use this to speed up the user.")]
  [SerializeField] int moveSpeed = 4;
  [Tooltip("Add jumpheigh we will use this to determine how high user will jump.")]
  [SerializeField] int jumpHeight = 4;

  PlayerAnimator playerAnimator;
  Rigidbody2D rb2D;
  SpriteRenderer playerSpriteRenderer;

  bool grounded = false;

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<PlayerAnimator>();
    playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
  }

  private void Update()
  {
    RaycastHit2D hitToGround = Physics2D.Raycast(transform.position, Vector2.down);
    Debug.DrawRay(transform.position, Vector2.down, Color.red);

  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Ground")
    {
      grounded = true;
      playerAnimator.SetJumpAnimation(false);
    }
  }

  void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "Ground")
    {
      grounded = false;
    }
  }

  void OnMove(InputValue input)
  {
    Vector2 movementInput = input.Get<Vector2>();
    rb2D.velocity = new Vector2(movementInput.x * moveSpeed, rb2D.velocity.y);
    playerAnimator.SetRunAnimation(Mathf.Abs(movementInput.x));

    // set player sprite facing in right direction
    if (movementInput.x < 0)
    {
      playerSpriteRenderer.flipX = true;
    }
    else if (movementInput.x > 0)
    {
      playerSpriteRenderer.flipX = false;
    }
  }

  void OnJump(InputValue input)
  {
    bool jumpCondition = input.isPressed && grounded;
    if (jumpCondition)
    {
      rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y + jumpHeight);
      playerAnimator.SetJumpAnimation(true);
    }
  }

  void OnFire(InputValue input)
  {
    if (input.isPressed && grounded)
    {
      playerAnimator.StartAttackAnimation();
    }
  }


}
