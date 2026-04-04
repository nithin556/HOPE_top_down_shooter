using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void OnEnter(EnemyStateManager state);
    public abstract void OnUpdate(EnemyStateManager state);
    public abstract void OnExit(EnemyStateManager state);

}
