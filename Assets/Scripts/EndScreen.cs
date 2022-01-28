using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI winText;

    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        winText.text =
            "Congratulations! \n You Scored " +
            scoreKeeper.CalculateScore() +
            "%";
    }
}
