using System.Collections;
using UnityEngine;

public abstract class UIBaseState : MonoBehaviour, IUIBase
{
    protected bool isRootState;
    public UIStateManager uiStateManager { get; set; }
    public UIStateFactory uiStateFactory { get; set; }
    protected UIBaseState currentUISuperState { get; set; }
    public UIBaseState currentUISubState { get; set; }

    public abstract Canvas UICanvas { get; }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    protected void SwitchSuperState(UIBaseState _newUISubState)
    {
        if (!isRootState)
        {
            ExitState();
            currentUISuperState.ExitState();
        }

        _newUISubState.EnterState();
        uiStateManager.uiCurrentState = _newUISubState;
    }

    protected void SwitchSubState(UIBaseState _newUISubState)
    {
        if (!isRootState)
        {
            ExitState();
            _newUISubState.currentUISuperState = currentUISuperState;
            currentUISuperState.currentUISubState = _newUISubState;
        }
        else
        {
            _newUISubState.currentUISuperState = this;
            currentUISubState = _newUISubState;
        }
        _newUISubState.EnterState();
    }


}
