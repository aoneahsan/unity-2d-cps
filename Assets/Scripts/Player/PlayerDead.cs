using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDead : MonoBehaviour
{
  Player player;
  private void Awake()
  {
    player = transform.parent.GetComponent<Player>();
  }

  public void GameOver()
  {
    if (player != null)
    {
      player.GameManager.GameUIManager.ShowGameOverScreen();
    }
  }
}
