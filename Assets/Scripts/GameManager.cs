using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // yes below implement code is not right, sorry
    public void LoadMainMenuScene()
    {
        if (levelManager != null)
        {
            levelManager.LoadMainMenuScene();
        }
    }

    public void LoadGameScene()
    {
        if (levelManager != null)
        {
            levelManager.LoadGameScene();
        }
    }

    public void LoadGameOverScene()
    {
        if (levelManager != null)
        {
            levelManager.LoadGameOverScene();
        }
    }

    public void QuitGame()
    {
        if (levelManager != null)
        {
            levelManager.QuitGame();
        }
    }
}
