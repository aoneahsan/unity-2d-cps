using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
  [SerializeField][Range(0, 10)] protected float health;
  [SerializeField][Range(0, 10)] protected float hitDamage;
  [SerializeField][Range(0, 10)] protected float speed;
  [SerializeField][Range(0, 10)] protected float rewardGems;

  [SerializeField] protected Transform[] wavePoints;
  protected Transform startingPosition;
  protected Transform endingPosition;
  SpriteRenderer enemySpriteRenderer;
  Animator enemyAnimator;

  Vector3 currentTarget;

  bool isStartingRound = true;

  protected virtual void Start()
  {
    startingPosition = wavePoints[0];
    endingPosition = wavePoints[1];

    if (startingPosition == null || endingPosition == null)
    {
      Debug.LogError("Wave points not set in Enemy");
    }

    if (startingPosition.position == endingPosition.position)
    {
      Debug.LogError("Starting position and ending position are the same");
    }

    if (startingPosition != null) transform.position = startingPosition.position;

    enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    enemyAnimator = GetComponentInChildren<Animator>();

    currentTarget = startingPosition.position;
  }

  protected virtual void Update()
  {

    // if Idle animation is playing return
    if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
    {
      return;
    }
    Move();
  }

  protected void Move()
  {
    if (transform.position == endingPosition.position)
    {
      StartIdleAnimation();
      FlipMossGiantSprite(true);
      currentTarget = startingPosition.position;
    }
    else if (transform.position == startingPosition.position)
    {
      if (isStartingRound)
      {
        isStartingRound = false;
      }
      else
      {
        StartIdleAnimation();
      }
      FlipMossGiantSprite(false);
      currentTarget = endingPosition.position;
    }

    transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
  }

  void FlipMossGiantSprite(bool state)
  {
    if (enemySpriteRenderer != null && enemySpriteRenderer.flipX != state)
    {
      enemySpriteRenderer.flipX = state;
    }
  }

  void StartIdleAnimation()
  {
    if (enemyAnimator != null)
    {
      enemyAnimator.SetTrigger("Idle");
    }
  }

  protected Transform GetStartPosition()
  {
    if (wavePoints.Length == 0)
    {
      Debug.LogError("No wave points assigned to enemy");
      return null;
    }
    return wavePoints[0];
  }

  protected Transform GetEndPosition()
  {
    if (wavePoints.Length == 0)
    {
      Debug.LogError("No wave points assigned to enemy");
      return null;
    }
    return wavePoints[wavePoints.Length - 1];
  }
}
