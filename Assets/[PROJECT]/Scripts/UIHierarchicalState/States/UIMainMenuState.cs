using UnityEngine;

public class UIMainMenuState : UIBaseState
{
    private Canvas uiCanvas;

    public override Canvas UICanvas { get { return uiCanvas == null ? uiCanvas = GetComponent<Canvas>() : uiCanvas; } }

    public override void EnterState()
    {
        isRootState = true;
        UICanvas.enabled = true;
        SwitchSubState(uiStateFactory.uiMainLobbyState);
    }

    public override void ExitState()
    {
        UICanvas.enabled = false;
    }

    public override void UpdateState()
    {
        if(currentUISubState) currentUISubState.UpdateState();
    }

}
