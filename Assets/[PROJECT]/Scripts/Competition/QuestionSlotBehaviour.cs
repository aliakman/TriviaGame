using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestionSlotBehaviour : MonoBehaviour
{
    public GameData gameData;

    public Image categoryImage;
    public Image questionProgressImage;

    public Text questionNumberText;
    public Text animatedScoreText;
    public Text questionText;

    public AnimationClip setOnQuestionSlotAnimationClip;
    public AnimationClip setOffQuestionSlotAnimationClip;

    public AnimationClip correctAnswerAnimatedTextAnimationClip;
    public AnimationClip wrongAnswerAnimatedTextAnimationClip;

    public Animation questionSlotAnimation;

    public List<Text> answerTexts = new List<Text>();
    public List<Button> answerButtons = new List<Button>();
    public List<Image> answerFilledImages = new List<Image>();

    private Color correctAnswerColor = Color.green;
    private Color wrongAnswerColor = Color.red;

    private float progressFillAmount;
    private int questionNumber;

    private void OnEnable()
    {
        EventManager.QuestionSlotBehaviour += () => this;
        EventManager.CorrectAnswerGiven += SetCorrectAnswer;
        EventManager.WrongAnswerGiven += SetWrongAnswer;
        EventManager.QuestionNotReplied += SetNoReplyAnswer;
    }

    private void OnDisable()
    {
        EventManager.QuestionSlotBehaviour -= () => this;
        EventManager.CorrectAnswerGiven -= SetCorrectAnswer;
        EventManager.WrongAnswerGiven -= SetWrongAnswer;
        EventManager.QuestionNotReplied -= SetNoReplyAnswer;
    }


    public void Init(int _questionNumber)
    {
        questionNumber = _questionNumber;

        if(questionNumber == 0)
            StartCoroutine(nameof(SetOnSlot));
        else
            StartCoroutine(nameof(SetOffSlot));

    }

    private void SetQuestionSlot() 
    {
        categoryImage.sprite = gameData.questionData.questions[questionNumber].categorySprite;
        questionNumberText.text = (questionNumber + 1).ToString();



        for (int i = 0; i < answerFilledImages.Count; i++)
            answerFilledImages[i].fillAmount = 0;

        QuestionInfo _questionInfo = gameData.questionData.questions[questionNumber];

        questionText.text = _questionInfo.question;

        for (int i = 0; i < answerTexts.Count; i++)
            answerTexts[i].text = _questionInfo.choices[i];

        SetAnswerButtonsInteractable(true);
    }

    private void SetCorrectAnswer(int _correctAnswerIndex)
    {
        SetAnswerButtonsInteractable(false);

        answerFilledImages[_correctAnswerIndex].color = correctAnswerColor;
        answerFilledImages[_correctAnswerIndex].DOFillAmount(1, 1);

        animatedScoreText.text = gameData.competitionData.correctAnswerPoint.ToString();
        animatedScoreText.GetComponent<Animation>().Play(correctAnswerAnimatedTextAnimationClip.name);

    }

    private void SetWrongAnswer(int _correctAnswerIndex, int _wrongAnswerIndex)
    {
        SetAnswerButtonsInteractable(false);

        answerFilledImages[_correctAnswerIndex].color = correctAnswerColor;
        answerFilledImages[_correctAnswerIndex].DOFillAmount(1, .2f);

        answerFilledImages[_wrongAnswerIndex].color = wrongAnswerColor;
        answerFilledImages[_wrongAnswerIndex].fillAmount = 1;

        animatedScoreText.text = gameData.competitionData.wrongAnswerPoint.ToString();
        animatedScoreText.GetComponent<Animation>().Play(wrongAnswerAnimatedTextAnimationClip.name);
        
        answerButtons[_wrongAnswerIndex].GetComponent<Animation>().Play();

    }

    private void SetNoReplyAnswer()
    {
        SetAnswerButtonsInteractable(false);

        animatedScoreText.text = gameData.competitionData.notReplyInTimePoint.ToString();

        animatedScoreText.GetComponent<Animation>().Play(wrongAnswerAnimatedTextAnimationClip.name);
    }

    private void SetAnswerButtonsInteractable(bool _isInteractable)
    {
        for (int i = 0; i < answerButtons.Count; i++)
            answerButtons[i].interactable = _isInteractable;
    }

    private IEnumerator SetOnSlot()
    {
        SetQuestionSlot();

        questionSlotAnimation.Play(setOnQuestionSlotAnimationClip.name);

        yield return new WaitForSeconds(1);
        
        EventManager.QuestionReady?.Invoke();
        SetAnswerButtonsInteractable(true);
    }

    private IEnumerator SetOffSlot()
    {
        questionSlotAnimation.Play(setOffQuestionSlotAnimationClip.name);

        if(questionNumber < gameData.questionData.questions.Count)
        {
            float _tmpQuestionCount = gameData.questionData.questions.Count;
            float _fillRatio = (questionNumber + .75f) / _tmpQuestionCount;

            DOTween.To(() => progressFillAmount, (x) => progressFillAmount = x, _fillRatio, 1f).
                OnUpdate(() =>
                {
                    questionProgressImage.fillAmount = progressFillAmount;
                });

        }

        yield return new WaitForSeconds(2);
        
        if(questionNumber < gameData.questionData.questions.Count)
            StartCoroutine(nameof(SetOnSlot));

    }

}
