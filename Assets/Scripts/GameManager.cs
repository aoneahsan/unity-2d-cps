using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  GameUIManager gameUIManager;
  public GameUIManager GameUIManager { get { return gameUIManager; } }
  Player player;
  public Player Player { get { return player; } }

  private void Awake()
  {
    gameUIManager = GetComponent<GameUIManager>();
    player = FindObjectOfType<Player>();
  }
}
