using UnityEngine;

public class CombatState : MonsterState
{
    public CombatState(StateMachine stateMachine, Animator anim)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
    }
    
    public override void Enter()
    {
        Debug.Log("Combat Enter");
        //FIXME : ScriptableObject로 변경
        stateMachine.baseTracking.Tracking(stateMachine.playerTrans.position, 3.5f, 120f);
        anim.SetBool("isPause", false);
    }
    public override void Execute()
    {
        Debug.Log("Combat");
        IsAgentMoving();
    }
    public override void Exit()
    {
    }
    public void IsAgentMoving()
    {
        if (stateMachine != null)
        {
            // if (stateMachine.agent.pathPending)
            // {
            //     return false;
            // }

            if (stateMachine.agent.remainingDistance <= stateMachine.agent.stoppingDistance)
            {
                anim.SetBool("isPause", true);
            }
            else
            {
                anim.SetBool("isPause", false);
            }

            // if (stateMachine.agent.velocity.sqrMagnitude > 0f)
            // {
            //     return true;
            // }
        }
    }
}
