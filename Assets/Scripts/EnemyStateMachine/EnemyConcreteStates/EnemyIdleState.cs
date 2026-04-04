using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float idleWaitTime = 2f;
    private float counterTime;
    public override void OnEnter(EnemyStateManager state)
    {
        counterTime = 0f;
        //check if previously engaged (alerted idle)
    }
    public override void OnUpdate(EnemyStateManager state)
    {
        counterTime += Time.deltaTime;
        if (counterTime > idleWaitTime)
        {
            state.SwitchState(state.enemyGuardState);
        }
    }
    public override void OnExit(EnemyStateManager state)
    {
        
    }
}