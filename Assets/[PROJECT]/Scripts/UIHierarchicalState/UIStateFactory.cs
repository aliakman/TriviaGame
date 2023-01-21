using System.Collections.Generic;
using UnityEngine;

public class UIStateFactory : MonoBehaviour
{
    UIStateManager uiStateManager;

    public UIMainMenuState uiMainMenuState;
    public UIGameMenuState uiGameMenuState;

    public UIMainLobbyState uiMainLobbyState;
    public UILeaderboardState uiLeaderboardState;

    public UIGameLobbyState uiGameLobbyState;
    public UIOnCompetitionState uiOnCompetitionState;
    public UIEndCompetitionState uiEndCompetitionState;

    public List<UIBaseState> uIBaseStates = new List<UIBaseState>();

    public UIStateFactory(UIStateManager _uiStateManager)
    {
        uiStateManager = _uiStateManager;
    }

    public void Init(UIStateManager _uiStateManager)
    {
        uiStateManager = _uiStateManager;

        foreach (var item in uIBaseStates)
        {
            item.uiStateManager = _uiStateManager;
            item.uiStateFactory = this;
        }
    }

}
