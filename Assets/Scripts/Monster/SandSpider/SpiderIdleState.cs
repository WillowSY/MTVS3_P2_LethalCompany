using UnityEngine;
using UnityEngine.XR;

public class SpiderIdleState : MonsterState
{
    private bool activation = false;
    public SpiderIdleState(StateMachine stateMachine, Animator anim)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        Debug.Log("Spider : Idle State");
        IsPatrol();              // 플레이어와 거미줄 충돌 시 정찰 상태 진입
    }

    public override void Exit()
    {
        
    }
    
    /* 플레이어와 거미줄 충돌 시 정찰 상태 진입 */
    void IsPatrol()
    {
        if (anim.GetBool("isWebTriggerOn") && activation == false)  // activation : 초기 한번만 실행하도록 제한
        {
            Debug.Log("Change to Idle -> Patorl");
            activation = true;
            stateMachine.ChangeState(MonsterEnums.State.Patrol);
        }
    }
}
