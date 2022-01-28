using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreText.text = levelManager.GetPlayerScore().ToString("000000000");
    }
}
