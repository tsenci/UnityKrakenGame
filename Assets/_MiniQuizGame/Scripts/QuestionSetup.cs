using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField]
    private List<QuestionData> questions;
    private QuestionData currentQuestion;

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    private int correctAnswerChoice;

    [SerializeField]
    private GameObject _endScreen;
    [SerializeField]
    private GameObject _quizGame;

    [SerializeField]
    private TextMeshProUGUI _resultsText;

    private int _currentScore;
    private int _totalQuestions;

    private void Awake()
    {
        //pull questions for quiz start
        GetQuestionAssets();
        //set end result total question to amt of questions started with
        _totalQuestions = questions.Count;
    }
    
    void Start()
    {
        SelectNewQuestion();
        SetQuestionValue();
        SetAnswerValues();
    }

    public void GoToNextQuestion()
    {
        //if no more questions, pull up end screen and reset game for if player continues
        if (questions.Count <= 0)
        {
            _quizGame.SetActive(false);
            _endScreen.SetActive(true);
            GetQuestionAssets();
            _currentScore = 0;

        }
        //otherwise go to the next question
        else
        {
            SelectNewQuestion();
            SetQuestionValue();
            SetAnswerValues();
        }
    }

    private void GetQuestionAssets()
    {
        //Get all the questions from the folder
        questions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
    }

    private void SelectNewQuestion()
    {
        //Random value for which question picked
        int randomQuestionIndex = Random.Range(0, questions.Count);
        //set question to prev random index
        currentQuestion = questions[randomQuestionIndex];
        //remove question from list of questions
        questions.RemoveAt(randomQuestionIndex);
    }

    private void SetQuestionValue()
    {
        //set question text
        questionText.text = currentQuestion.question;
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update score display so that we can see new score
        _resultsText.text =
            "Results: " + _currentScore.ToString() + "/" + _totalQuestions;
    }

    private void SetAnswerValues()
    {
        //randomize answer order
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        //set up answer buttons
        for(int i = 0; i < answerButtons.Length; i++)
        {
            //create a temp bool to pass to buttons
            bool isCorrect = false;

            //if it is the correct answer set bool to true
            if(i == correctAnswerChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
        }
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string> newList = new List<string>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            //get random number of remaining choices
            int random = Random.Range(0, originalList.Count);

            //if random number is 0, is correct choice, only used ONCE
            if (random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }

            //add this to the new list
            newList.Add(originalList[random]);
            //remove choice from original list
            originalList.RemoveAt(random);
        }

        return newList;
    }
}
