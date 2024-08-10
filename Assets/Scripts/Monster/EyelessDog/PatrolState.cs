using UnityEngine;

public class PatrolState : MonsterState
{
    public PatrolState(StateMachine stateMachine, Animator anim)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
    }
    public override void Enter()
    {
        Debug.Log("Patrol Enter");
        stateMachine.basePatrol.StartUpdate();
    }
    
    public override void Execute()
    {
        Debug.Log("Patrol");
        IsAgentMoving();
    }
    public override void Exit()
    {
        stateMachine.basePatrol.StopUpdate();
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