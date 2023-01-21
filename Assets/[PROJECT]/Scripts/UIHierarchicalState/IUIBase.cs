using UnityEngine;

public interface IUIBase
{
    public abstract Canvas UICanvas { get; }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
