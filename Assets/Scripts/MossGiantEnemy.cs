using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiantEnemy : Enemy
{
  bool isMovingTowardsStart = false;

  SpriteRenderer mossGiantSpriteRenderer;
  Animator mossGiantAnimator;

  protected override void Start()
  {
    base.Start();

    mossGiantSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    mossGiantAnimator = GetComponentInChildren<Animator>();

    StartWalkAnimation();
  }

  void Update()
  {
    Move();
  }

  protected override void Move()
  {
    if (transform.position == endingPosition.position && !isMovingTowardsStart)
    {
      isMovingTowardsStart = true;
    }
    else if (transform.position == startingPosition.position && isMovingTowardsStart)
    {
      isMovingTowardsStart = false;
    }

    if (isMovingTowardsStart)
    {
      FlipMossGiantSprite(true);
      transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, speed * Time.deltaTime);

    }
    else
    {
      FlipMossGiantSprite(false);

      transform.position = Vector3.MoveTowards(transform.position, endingPosition.position, speed * Time.deltaTime);
    }
  }

  void FlipMossGiantSprite(bool state)
  {
    if (mossGiantSpriteRenderer != null && mossGiantSpriteRenderer.flipX != state)
    {
      mossGiantSpriteRenderer.flipX = state;
    }
  }

  void StartWalkAnimation()
  {
    if (mossGiantAnimator != null)
    {
      mossGiantAnimator.SetTrigger("Walk");
    }
  }
}
