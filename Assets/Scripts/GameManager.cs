using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  GameUIManager gameUIManager;
  public GameUIManager GameUIManager { get { return gameUIManager; } }

  private void Awake()
  {
    gameUIManager = GetComponent<GameUIManager>();
  }


}
