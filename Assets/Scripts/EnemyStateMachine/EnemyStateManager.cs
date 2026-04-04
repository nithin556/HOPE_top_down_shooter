using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public EnemyIdleState enemyIdleState = new EnemyIdleState();
    public EnemyGuardState enemyGuardState = new EnemyGuardState();
    public EnemyChaseState enemyChaseState = new EnemyChaseState();
    public EnemyAttackState enemyAttackState = new EnemyAttackState();

    [SerializeField] public TowerScript tower;

    void Start()
    {

        currentState = enemyIdleState;
        currentState.OnEnter(this);
    }
    void Update()
    {
        currentState.OnUpdate(this);
    }
    public void SwitchState(EnemyBaseState state)
    {
        currentState.OnExit(this);
        currentState = state;
        currentState.OnEnter(this);
    }
}
