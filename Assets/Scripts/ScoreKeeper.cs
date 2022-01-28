using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswersCount;

    int answersSeenCount;

    public int GetCorrectAnswersCount()
    {
        return correctAnswersCount;
    }

    public void IncrementCorrectAnswersCount()
    {
        correctAnswersCount++;
    }

    public int GetAnswersSeenCount()
    {
        return answersSeenCount;
    }

    public void IncrementAnswersSeenCount()
    {
        answersSeenCount++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswersCount / answersSeenCount * 100);
    }
}
