using UnityEngine;

public class SpiderPatrolState : MonsterState
{
    public SpiderPatrolState(StateMachine stateMachine, Animator anim)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
    }
    public override void Enter()
    {
        // 랜덤 위치 순찰 시작
        stateMachine._basePatrol.StartUpdate();
    }
    
    public override void Execute()
    {
        //Debug.Log("Spider : Patrol");
        // 플레이어 시야 감지 판별
        IsDetected();
        // agent가 이동중이면 Walk 애니메이션 재생
        IsAgentMoving();
    }
    public override void Exit()
    {
        // 랜덤 위치 순찰 종료
        stateMachine._basePatrol.StopUpdate();
    }
    
    /* 현재 agent가 움직이고 있으면 걷는 모션 재생 */
    public void IsAgentMoving()
    {
        if (stateMachine != null)
        {

            if (stateMachine._agent.remainingDistance <= stateMachine._agent.stoppingDistance)
            {
                anim.SetBool("isPause", true);
            }
            else
            {
                anim.SetBool("isPause", false);
            }
        }
    }
    
    /* 플레이어가 시야에 감지 시 Attack State 진입 */
    public void IsDetected()
    {
        if (stateMachine != null)
        {

            if (anim.GetBool("isDetected"))
            {
                stateMachine.ChangeState(MonsterEnums.State.Attack);
            }
        }
    }
}
