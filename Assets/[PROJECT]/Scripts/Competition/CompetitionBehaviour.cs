using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CompetitionBehaviour : MonoBehaviour
{
    private QuestionSlotBehaviour _questionSlotBehaviour;
    private QuestionSlotBehaviour questionSlotBehaviour { get { return _questionSlotBehaviour ? _questionSlotBehaviour : _questionSlotBehaviour = EventManager.QuestionSlotBehaviour?.Invoke(); } }

    public GameData gameData;

    public Text scoreText;
    public Text questionNumberText;
    public Text countdownText;

    public Image questionProgressImage;

    public int questionNumber;
    private int score;
    private int timer;
    private float tmpTimer;

    private bool isAnswerCorrect;
    private bool isTimesStarted;
    private bool isLastQuestion;

    private void OnEnable()
    {
        DataManager.LoadData(gameData);

        EventManager.CompetitionBehaviour += () => this;
        EventManager.QuestionReady += () => isTimesStarted = true;
    }
    private void OnDisable()
    {
        EventManager.CompetitionBehaviour -= () => this;
        EventManager.QuestionReady -= () => isTimesStarted = true;
    }

    public void Init()
    {
        isLastQuestion = false;
        tmpTimer = 1;
        timer = gameData.competitionData.countdownTime;
        score = gameData.playerData.playerScore;
        scoreText.text = score.ToString();

        countdownText.text = gameData.competitionData.countdownTime.ToString();

        questionProgressImage.fillAmount = (questionNumber + 1) / gameData.questionData.questions.Count;

        questionSlotBehaviour.Init(questionNumber);
    }

    private void Update()
    {
        if (isTimesStarted)
        {
            if(tmpTimer > 0)
            {
                tmpTimer -= Time.deltaTime;
            }
            else
            {
                tmpTimer = 1;
                timer--;
                countdownText.text = timer.ToString();
            }

            if(timer <= 0)
            {
                isTimesStarted = false;
                NoReplyInTime();
            }
        }
    }

    public void OnClickAnswerButton(int _answerValue) //Activated by Answer Buttons
    {
        isTimesStarted = false;

        if ((int)gameData.questionData.questions[questionNumber].answer == _answerValue)
        {
            isAnswerCorrect = true;
            gameData.questionData.questions[questionNumber].answerStatus = global::AnswerStatus.Correct;
            EventManager.CorrectAnswerGiven?.Invoke(_answerValue);
        }
        else
        {
            isAnswerCorrect = false;
            gameData.questionData.questions[questionNumber].answerStatus = global::AnswerStatus.Wrong;

            for (int i = 0; i < 4; i++)
            {
                if(i == (int)gameData.questionData.questions[questionNumber].answer)
                {
                    EventManager.WrongAnswerGiven?.Invoke(i, _answerValue);
                    break;
                }
            }


        }

        AnswerStatus(isAnswerCorrect);

    }

    public void AnswerStatus(bool _isCorrect)
    {
        Debug.Log("Answer is " + _isCorrect);

        gameData.playerData.playerScore += 
            _isCorrect ?
                gameData.competitionData.correctAnswerPoint :
                gameData.playerData.playerScore + gameData.competitionData.wrongAnswerPoint < 0 ? 
                    -gameData.playerData.playerScore:
                    gameData.competitionData.wrongAnswerPoint;

        if (questionNumber + 1 >= gameData.questionData.questions.Count)
        {
            isLastQuestion = true;
            StartCoroutine(nameof(WaitForToEnd));
        }
        questionNumber++;

        ScoreUpdate();

    }

    public void NoReplyInTime()
    {
        Debug.Log("No Reply In Time");

        gameData.questionData.questions[questionNumber].answerStatus = global::AnswerStatus.TimesUp;

        EventManager.QuestionNotReplied?.Invoke();

        gameData.playerData.playerScore +=
            gameData.playerData.playerScore + gameData.competitionData.notReplyInTimePoint < 0 ?
                gameData.playerData.playerScore :
                gameData.competitionData.notReplyInTimePoint;

        if (questionNumber + 1 >= gameData.questionData.questions.Count)
        {
            isLastQuestion = true;
            StartCoroutine(nameof(WaitForToEnd));
        }
        questionNumber++;

        ScoreUpdate();

    }

    private void ScoreUpdate()
    {
        int _value = gameData.playerData.playerScore;

        DOTween.To(() => score, (x) => score = x, _value, 1f).
            OnUpdate(() => { scoreText.text = score.ToString(); }).
            OnComplete(()=> 
            {
                if(!isLastQuestion)
                    StartCoroutine(nameof(WaitForInit));
            });

    }

    private IEnumerator WaitForInit()
    {
        yield return new WaitForSeconds(1);
        Init(); 
    }

    private IEnumerator WaitForToEnd()
    {
        yield return new WaitForSeconds(1);

        questionSlotBehaviour.Init(questionNumber);

        yield return new WaitForSeconds(1);
        
        EventManager.ToEndCompetition?.Invoke();
    }
}
