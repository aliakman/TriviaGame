using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameLobbyState : UIBaseState
{

    private Canvas uiCanvas;
    public override Canvas UICanvas { get { return uiCanvas == null ? uiCanvas = GetComponent<Canvas>() : uiCanvas; } }

    public GameData gameData;

    public List<Image> categoryImages = new List<Image>();
    public List<Text> categoryTexts = new List<Text>();
    public List<Text> questionAmountTexts = new List<Text>();


    public override void EnterState()
    {
        SetGameLobbyInfo();
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

    public void OnClickStartButton() //Activated by Start Button
    {
        SwitchSubState(uiStateFactory.uiOnCompetitionState);
    }

    public void OnClickBackButton() //Activated by Back Button
    {
        SwitchSuperState(uiStateFactory.uiMainMenuState);
    }

    private void SetGameLobbyInfo()
    {
        for (int i = 0; i < gameData.questionData.questions.Count; i++)
        {
            categoryImages[i].sprite = gameData.questionData.questions[i].categorySprite;
            categoryTexts[i].text = gameData.questionData.questions[i].category;
            questionAmountTexts[i].text = 1.ToString();
        }
    }

}
