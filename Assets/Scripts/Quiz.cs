using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField]
    TextMeshProUGUI questionText;

    [SerializeField]
    List<QuestionScriptableObject>
        questions = new List<QuestionScriptableObject>();

    QuestionScriptableObject currentQuestion;

    [Header("Answers")]
    [SerializeField]
    GameObject[] answers;

    [Header("Answer Buttons Colors/Sprites")]
    [SerializeField]
    Sprite defaultAnswerImage;

    [SerializeField]
    Sprite correctAnswerImage;

    [Header("Timer")]
    [SerializeField]
    Image timerImage;

    Timer timer;

    [Header("ScoreKeeper")]
    [SerializeField]
    TextMeshProUGUI scoreText;

    ScoreKeeper scoreKeeper;

    bool hasAnsweredEarlly = true;

    [Header("ProgressBar")]
    [SerializeField]
    Slider progressBar;

    public bool isGameCompleted;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        GetNextQuestion();
    }

    void Update()
    {
        UpdateTimerImageFill();
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isGameCompleted = true;
                return;
            }
            hasAnsweredEarlly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarlly && !timer.isAnsweringQuestion)
        {
            DisplayAnswers(-1);
        }
    }

    void UpdateTimerImageFill()
    {
        timerImage.fillAmount = timer.fillFraction;
    }

    void GetNextQuestion()
    {
        GetRandomQuestion();
        DisplayQuestion();
        SetAnswerButtonsImageToDefault();
        ChangeButtonsState(true);
        scoreKeeper.IncrementAnswersSeenCount();
        progressBar.value = scoreKeeper.GetAnswersSeenCount();
    }

    private void GetRandomQuestion()
    {
        if (questions.Count > 0)
        {
            int index = Random.Range(0, questions.Count);
            currentQuestion = questions[index];
            questions.RemoveAt (index);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.getQuestion();

        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI buttonText =
                answers[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.getAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarlly = true;
        DisplayAnswers (index);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplayAnswers(int index)
    {
        int correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
        if (index == correctAnswerIndex)
        {
            questionText.text = "Coorect :)";
            scoreKeeper.IncrementCorrectAnswersCount();
        }
        else
        {
            string correctAnswer =
                currentQuestion.getAnswer(correctAnswerIndex);
            questionText.text =
                "Sorry, the correct answer was;\n" + correctAnswer;
        }
        Image buttonImage = answers[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerImage;
        ChangeButtonsState(false);
    }

    void ChangeButtonsState(bool state)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].GetComponent<Button>().interactable = state;
        }
    }

    void SetAnswerButtonsImageToDefault()
    {
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].GetComponent<Image>().sprite = defaultAnswerImage;
        }
    }
}
