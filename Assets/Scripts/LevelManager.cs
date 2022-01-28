using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    float delayInScene = 2f;

    // we will set this from game scene and use at gameover scene
    int playerScoreToDisplay;

    private void Awake()
    {
        ManageSingleten();
    }

    private void ManageSingleten()
    {
        int instancesCount = FindObjectsOfType<LevelManager>().Length;
        if (instancesCount > 1)
        {
            gameObject.SetActive(false);
            Destroy (gameObject);
        }
        else
        {
            DontDestroyOnLoad (gameObject);
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad("GameOver", delayInScene));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene (sceneName);
    }

    public void SetPlayerScore(int score)
    {
        playerScoreToDisplay = score;
    }

    public int GetPlayerScore()
    {
        return playerScoreToDisplay;
    }
}
