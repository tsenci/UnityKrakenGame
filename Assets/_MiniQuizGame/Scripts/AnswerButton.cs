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
            _questionSetup.IncreaseScore(1);
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

        //next question
        _questionSetup.GoToNextQuestion();
    }
}
