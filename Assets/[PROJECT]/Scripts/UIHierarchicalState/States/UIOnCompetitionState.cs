using UnityEngine;

public class UIOnCompetitionState : UIBaseState
{
    private Canvas uiCanvas;

    private CompetitionBehaviour _competitionBehaviour;
    private CompetitionBehaviour competitionBehaviour{ get { return _competitionBehaviour ? _competitionBehaviour : _competitionBehaviour = EventManager.CompetitionBehaviour?.Invoke(); } }

    public override Canvas UICanvas { get { return uiCanvas == null ? uiCanvas = GetComponent<Canvas>() : uiCanvas; } }

    private void OnEnable()
    {
        EventManager.ToEndCompetition += ToEndCompetition;
    }
    private void OnDisable()
    {
        EventManager.ToEndCompetition -= ToEndCompetition;
    }

    public override void EnterState()
    {
        isRootState = false;
        UICanvas.enabled = true;

        competitionBehaviour.Init();
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        UICanvas.enabled = false;
    }

    private void ToEndCompetition()
    {
        SwitchSubState(uiStateFactory.uiEndCompetitionState);
    }
}
