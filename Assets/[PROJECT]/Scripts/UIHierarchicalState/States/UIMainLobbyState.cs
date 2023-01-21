using System.Collections;
using UnityEngine;

public class UIMainLobbyState : UIBaseState
{
    private Canvas uiCanvas;

    public override Canvas UICanvas { get { return uiCanvas == null ? uiCanvas = GetComponent<Canvas>() : uiCanvas; } }

    public override void EnterState()
    {
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

    public void OnClickPlayButton() //Activated by Play Button
    {
        SwitchSuperState(uiStateFactory.uiGameMenuState);
    }

    public void OnClickLeaderboardButton() //Activated by Leaderboard Button
    {
        SwitchSubState(uiStateFactory.uiLeaderboardState);
    }
}
