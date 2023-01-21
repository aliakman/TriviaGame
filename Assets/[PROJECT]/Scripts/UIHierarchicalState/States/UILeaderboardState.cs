using UnityEngine;

public class UILeaderboardState : UIBaseState
{
    private Canvas uiCanvas;
    public override Canvas UICanvas { get { return uiCanvas == null ? uiCanvas = GetComponent<Canvas>() : uiCanvas; } }

    private LeaderboardGeneralData _leaderboardData;
    private LeaderboardGeneralData leaderboardGeneral { get { return _leaderboardData ? _leaderboardData : _leaderboardData = EventManager.LeaderboardGeneralData?.Invoke(); } }

    private ScrollBehaviour _scrollBehaviour;
    private ScrollBehaviour scrollBehaviour { get { return _scrollBehaviour ? _scrollBehaviour : _scrollBehaviour = EventManager.ScrollBehaviour?.Invoke(); } }

    public GameObject NotConnectionObject;


    public override void EnterState()
    {
        isRootState = false;
        UICanvas.enabled = true;

        if (NetConnectionForScroll())
            scrollBehaviour.SetScroll();

    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        UICanvas.enabled = false;
        NotConnectionObject.SetActive(false);
    }

    public void OnClickXButton() //Activated by X Button
    {
        SwitchSubState(uiStateFactory.uiMainLobbyState);
    }

    private bool NetConnectionForScroll()
    {
        if (!APIHelper.CheckNetConnection())
        {
            NotConnectionObject.SetActive(true);
            return false;
        }

        leaderboardGeneral.SetLeaderboardData();

        return true;
    }

}
