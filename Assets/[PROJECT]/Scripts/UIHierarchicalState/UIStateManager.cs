using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    [HideInInspector] public UIBaseState uiCurrentState;
    [HideInInspector] public UIStateFactory uiStateFactory;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        uiStateFactory.Init(this);
        uiCurrentState = uiStateFactory.uiMainMenuState;
        uiCurrentState.EnterState();
    }

    private void Update()
    {
        uiCurrentState.UpdateState();
    }
}
