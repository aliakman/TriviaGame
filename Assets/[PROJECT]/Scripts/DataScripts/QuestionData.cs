using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Question Data",menuName ="DATA/Question Data", order = 2)]
public class QuestionData : ScriptableObject
{
    public List<QuestionInfo> questions = new List<QuestionInfo>();
}

[System.Serializable]
public class QuestionInfo
{
    public string category;
    public Sprite categorySprite;
    public string question;
    public List<string> choices = new List<string>(4);
    public AnswerTypes answer;
    public AnswerStatus answerStatus;
}

public enum AnswerTypes
{
    A,
    B,
    C,
    D
}

public enum AnswerStatus
{
    Correct,
    Wrong,
    TimesUp
}
