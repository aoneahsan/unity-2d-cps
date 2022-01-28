using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float timeToCompleteQuestion = 30f;

    [SerializeField]
    float timeToShowAnswer = 5f;

    public float timerAmount;

    public float fillFraction;

    public bool isAnsweringQuestion = true;

    public bool loadNextQuestion;

    void Awake()
    {
        timerAmount = timeToCompleteQuestion;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerAmount = 0;
    }

    void UpdateTimer()
    {
        timerAmount -= Time.deltaTime;
        if (isAnsweringQuestion)
        {
            if (timerAmount > 0)
            {
                fillFraction = timerAmount / timeToCompleteQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerAmount = timeToShowAnswer;
            }
        }
        else
        {
            if (timerAmount > 0)
            {
                fillFraction = timerAmount / timeToShowAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerAmount = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
