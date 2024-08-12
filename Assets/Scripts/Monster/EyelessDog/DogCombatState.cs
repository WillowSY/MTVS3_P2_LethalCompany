using UnityEngine;

public class DogCombatState : MonsterState
{
    public DogCombatState(StateMachine stateMachine, Animator anim)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
    }
    
    public override void Enter()
    {
        //Debug.Log("Combat Enter");
        //FIXME : ScriptableObject로 변경
        stateMachine._baseTracking.Tracking(stateMachine._playerTrans.position, 3.5f *1.5f, 120f);
        anim.SetBool("isPause", false);
    }
    public override void Execute()
    {
        //Debug.Log("Combat");
        IsAgentMoving();                // agent가 이동중이면 Walk 애니메이션 재생
    }
    public override void Exit()
    {
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
}
