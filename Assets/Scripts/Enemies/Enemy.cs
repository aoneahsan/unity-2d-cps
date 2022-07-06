using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
  [SerializeField][Range(0, 10)] protected int health = 3;
  [SerializeField][Range(0, 10)] protected int hitDamage = 1;
  [SerializeField][Range(0, 10)] protected float speed = 1f;
  [SerializeField][Range(0, 10)] protected int rewardGems = 5;
  [SerializeField][Range(1, 10)] protected int stopDistanceFromPlayer = 2;
  [SerializeField] protected int dropDiamondsOnDeath = 1;
  [SerializeField] protected GameObject diamondPrefab;

  [SerializeField] protected Transform[] wavePoints;
  protected Transform startingPosition;
  protected Transform endingPosition;
  protected SpriteRenderer enemySpriteRenderer;
  protected Animator enemyAnimator;

  protected Vector3 currentTarget;
  protected Player player;


  protected bool isStartingRound = true;
  protected bool isDead = false;
  public bool IsDead { get { return isDead; } }

  private void Awake()
  {
    player = FindObjectOfType<Player>();
  }

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
    if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || isDead || (player != null && player.IsDead))
    {
      return;
    }
    if (DistanceFromPlayer() < stopDistanceFromPlayer)
    {
      FacePlayerAndAttack();
    }
    else
    {
      Move();
    }
  }

  protected virtual void Move()
  {
    if (!enemyAnimator.GetBool("Walk"))
    {
      StartMoveAnimation();
    }

    if (transform.position == endingPosition.position)
    {
      PlayIdleAnimation();
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
        PlayIdleAnimation();
      }
      FlipMossGiantSprite(false);
      currentTarget = endingPosition.position;
    }

    if (currentTarget == startingPosition.position)
    {
      FlipMossGiantSprite(true);
    }
    else if (currentTarget == endingPosition.position)
    {
      FlipMossGiantSprite(false);
    }

    transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
  }

  protected virtual void FlipMossGiantSprite(bool state)
  {
    if (enemySpriteRenderer != null && enemySpriteRenderer.flipX != state)
    {
      enemySpriteRenderer.flipX = state;
    }
  }

  protected virtual Transform GetStartPosition()
  {
    if (wavePoints.Length == 0)
    {
      Debug.LogError("No wave points assigned to enemy");
      return null;
    }
    return wavePoints[0];
  }

  protected virtual Transform GetEndPosition()
  {
    if (wavePoints.Length == 0)
    {
      Debug.LogError("No wave points assigned to enemy");
      return null;
    }
    return wavePoints[wavePoints.Length - 1];
  }

  public virtual void Damage(int damage)
  {
    health -= damage;

    if (health <= 0)
    {
      Die();
    }
    else
    {
      SetAttackAnimation(true);
    }
  }

  public virtual void Die()
  {
    if (health <= 0)
    {
      isDead = true;
      GetComponent<BoxCollider2D>().enabled = false;
      if (enemyAnimator != null)
      {
        PlayDeadAnimation();
      }
      if (dropDiamondsOnDeath > 0 && diamondPrefab != null)
      {
        DropDiamonds();
      }
      Destroy(gameObject, 3);
    }
  }

  private void DropDiamonds()
  {
    if (dropDiamondsOnDeath > 0 && diamondPrefab != null)
    {
      GameObject diamondObj = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
      diamondObj.GetComponent<DiamondPickable>().SetAmount(dropDiamondsOnDeath);
    }
  }

  protected virtual float DistanceFromPlayer()
  {
    return Vector3.Distance(transform.position, player.transform.position);
  }

  protected virtual void FacePlayerAndAttack()
  {
    float distance = player.transform.position.x - transform.position.x;

    if (distance > 0)
    {
      enemySpriteRenderer.flipX = false;
    }
    else
    {
      enemySpriteRenderer.flipX = true;
    }

    StartAttackAnimation();
  }

  protected virtual void StartMoveAnimation()
  {
    // start moving anim and stop attacking anim
    SetWalkAnimation(true);
    SetAttackAnimation(false);
  }

  protected virtual void StartAttackAnimation()
  {
    // stop moving anim and start attacking anim
    SetWalkAnimation(false);
    SetAttackAnimation(true);
  }

  protected virtual void SetWalkAnimation(bool walkAnimationState)
  {
    if (enemyAnimator != null)
    {
      enemyAnimator.SetBool("Walk", walkAnimationState);
    }
  }

  protected virtual void SetAttackAnimation(bool attackAnimationState)
  {
    if (enemyAnimator != null)
    {
      enemyAnimator.SetBool("Attack", attackAnimationState);
    }
  }

  protected virtual void PlayIdleAnimation()
  {
    if (enemyAnimator != null)
    {

      enemyAnimator.SetTrigger("Idle");
    }
  }

  protected virtual void PlayHitAnimation()
  {
    if (enemyAnimator != null)
    {
      enemyAnimator.SetTrigger("Hit");
    }
  }

  protected virtual void PlayDeadAnimation()
  {
    if (enemyAnimator != null)
    {
      SetWalkAnimation(false);
      SetAttackAnimation(false);
      enemyAnimator.SetTrigger("Dead");
    }
  }
}
