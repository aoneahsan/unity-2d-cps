using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : Enemy
{
  protected override void Move()
  {
    // if (!enemyAnimator.GetBool("Walk"))
    // {
    //   StartMoveAnimation();
    // }

    // if (transform.position == endingPosition.position)
    // {
    //   PlayAttackAnimation();
    //   FlipMossGiantSprite(true);
    //   currentTarget = startingPosition.position;
    // }
    // else if (transform.position == startingPosition.position)
    // {
    //   if (isStartingRound)
    //   {
    //     isStartingRound = false;
    //   }
    //   else
    //   {
    //     PlayIdleAnimation();
    //   }
    //   FlipMossGiantSprite(false);
    //   currentTarget = endingPosition.position;
    // }

    // if (currentTarget == startingPosition.position)
    // {
    //   FlipMossGiantSprite(true);
    // }
    // else if (currentTarget == endingPosition.position)
    // {
    //   FlipMossGiantSprite(false);
    // }

    // transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
  }

  void PlayAttackAnimation()
  {
    // WORKING HERE (make this trigger for spider and then continue with the rest of the spider attack flow.)
    enemyAnimator.SetTrigger("Attack");
  }
}
