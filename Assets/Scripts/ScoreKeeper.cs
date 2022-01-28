using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI playerScoreTMPro;

    int playerScore = 0;

    void Start()
    {
        UpdateScoreUI();
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void IncreasePlayerScore(int points)
    {
        if (points > 0)
        {
            playerScore += points;
            UpdateScoreUI();
        }
    }

    public void ResetPlayerScore()
    {
        playerScore = 0;
    }

    void UpdateScoreUI()
    {
        if (playerScoreTMPro != null)
        {
            playerScoreTMPro.text = playerScore.ToString("000000000");
        }
    }
}
