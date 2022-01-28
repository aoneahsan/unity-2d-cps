using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    int playerLives = 3;

    int playerScore = 0;

    AudioSource myAudioSource;

    [SerializeField]
    AudioClip coinPickupAudioClip;

    [SerializeField]
    TextMeshProUGUI livesText;

    [SerializeField]
    TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        int activeGameSessions = FindObjectsOfType<GameSession>().Length;
        if (activeGameSessions > 1)
        {
            Destroy (gameObject);
        }
        else
        {
            DontDestroyOnLoad (gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGame()
    {
        FindObjectOfType<ScenePersists>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy (gameObject);
    }

    public void IncreasePlayerScore(int points)
    {
        if (points > 0)
        {
            myAudioSource.clip = coinPickupAudioClip;
            myAudioSource.Play();
            playerScore += points;
            scoreText.text = playerScore.ToString();
        }
    }
}
