using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
  [SerializeField][Range(0, 10)] protected int health = 3;
  [SerializeField][Range(0, 10)] protected int hitDamage = 1;
  [SerializeField][Range(0, 10)] protected float speed = 1f;
  [SerializeField][Range(0, 10)] protected int rewardGems = 5;

  [SerializeField] protected Transform[] wavePoints;
  protected Transform startingPosition;
  protected Transform endingPosition;
  SpriteRenderer enemySpriteRenderer;
  Animator enemyAnimator;

  Vector3 currentTarget;
  Player player;

  bool isStartingRound = true;
  bool isDead = false;

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
    if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || isDead)
    {
      return;
    }
    if (DistanceFromPlayer() < 1f)
    {
      FacePlayerAndAttack();
    }
    else
    {
      Move();
    }
  }

  protected void Move()
  {
    if (enemyAnimator.GetBool("Attack"))
    {
      enemyAnimator.SetBool("Attack", false);
    }
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
    PlayAnimation("Idle");
  }

  void PlayAnimation(string anim)
  {
    if (enemyAnimator != null)
    {
      enemyAnimator.SetTrigger(anim);
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

  public void Damage(int damage)
  {
    health -= damage;

    if (health <= 0)
    {
      Die();
    }
    else
    {
      PlayAnimation("Hit");
    }
  }

  public void Die()
  {
    if (health <= 0)
    {
      isDead = true;
      GetComponent<BoxCollider2D>().enabled = false;
      if (enemyAnimator != null)
      {
        enemyAnimator.SetTrigger("Dead");
      }
      Destroy(gameObject, 3);
    }
  }

  float DistanceFromPlayer()
  {
    return Vector3.Distance(transform.position, player.transform.position);
  }

  void FacePlayerAndAttack()
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

    enemyAnimator.SetBool("Attack", true);
  }
}
