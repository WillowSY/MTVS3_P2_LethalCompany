using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DogPatrolState : MonsterState
{
    public DogPatrolState(StateMachine stateMachine, Animator anim)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
    }
    public override void Enter()
    {
        // Debug.Log("Patrol Enter");
        // 랜덤 위치 순찰 시작
        stateMachine._basePatrol.StartUpdate();
    }
    
    public override void Execute()
    {
        //Debug.Log("Patrol");
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
                Debug.Log("IsPause On");
                anim.SetBool("isPause", true);
            }
            else
            {
                Debug.Log("IsPause Off");
                anim.SetBool("isPause", false);
            }
        }
    }
}