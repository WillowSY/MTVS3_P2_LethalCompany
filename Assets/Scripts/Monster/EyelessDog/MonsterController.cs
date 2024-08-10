using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private StateMachine stateMachine;
    private BasePatrol basePatrol;
    private BaseTracking baseTracking;
    public Animator anim;
    private NavMeshAgent agent;
    public Transform playerTrans;

    private void Awake()
    {
        basePatrol = GetComponent<BasePatrol>();
        agent = GetComponent<NavMeshAgent>();
        baseTracking = GetComponent<BaseTracking>();
        stateMachine = new StateMachine(basePatrol, baseTracking, anim, agent, playerTrans);
        if (stateMachine == null)
        {
            Debug.LogError("MonsterController : stateMachine is null");
        }
    }
    
    private void Start()
    {
        stateMachine.ChangeState(MonsterEnums.State.Patrol);
    }
    
    private void Update()
    {
        Debug.Log("MonsterController Updating");
        //IsAgentMoving();
        stateMachine.Update();
        

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