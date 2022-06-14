using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
  [SerializeField] protected int health;
  [SerializeField] protected int hitDamage;
  [SerializeField] protected int speed;
  [SerializeField] protected int rewardGems;

  [SerializeField] protected Transform[] wavePoints;
  protected Transform startingPosition;
  protected Transform endingPosition;

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
  }

  void Update()
  {

  }

  protected void Attack()
  {
    Debug.Log($"The Name of the Enemy is {name}");
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

  protected abstract void Move();
}
