using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndCompetitionState : UIBaseState
{
    private Canvas uiCanvas;
    public override Canvas UICanvas { get { return uiCanvas == null ? uiCanvas = GetComponent<Canvas>() : uiCanvas; } }

    public GameData gameData;

    public List<Sprite> answerTypeSprites = new List<Sprite>();
    
    public List<Image> answerTypeImages = new List<Image>();
    public List<Image> categoryImages = new List<Image>();
    public List<Text> categoryTexts = new List<Text>();

    public override void EnterState()
    {
        SetEndCompetitionInfo();
        isRootState = false;
        UICanvas.enabled = true;
    }

    public override void ExitState()
    {
        UICanvas.enabled = false;
    }

    public override void UpdateState()
    {
        
    }

    public void OnClickMainMenuButton()
    {
        SwitchSuperState(uiStateFactory.uiMainMenuState);
    }

    private void SetEndCompetitionInfo()
    {
        for (int i = 0; i < gameData.questionData.questions.Count; i++)
        {
            categoryImages[i].sprite = gameData.questionData.questions[i].categorySprite;
            categoryTexts[i].text = gameData.questionData.questions[i].category;
            answerTypeImages[i].sprite = GetSpriteByAnswerStatus(i);
        }
    }

    private Sprite GetSpriteByAnswerStatus(int _questionIndex)
    {
        return answerTypeSprites[(int)gameData.questionData.questions[_questionIndex].answerStatus];
    }
}
