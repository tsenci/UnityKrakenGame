using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] public QuestionSetup _questionSetup;
    private bool _isCorrect;
    [SerializeField]
    private TextMeshProUGUI _answerText;
    [SerializeField] 
    private QuestionSetup _scoreUpdater;

    public void SetAnswerText(string newText)
    {
        _answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        _isCorrect = newBool;
    }

    public void OnClick()
    {
        if (_isCorrect)
        {
            Debug.Log("Correct Answer");
            _scoreUpdater.IncreaseScore(1);
            //_pointsGained++;
            //Debug.Log(_pointsGained);
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

        //next question
        _questionSetup.GoToNextQuestion();
    }
}
