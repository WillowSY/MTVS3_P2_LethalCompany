using UnityEngine;
using UnityEngine.AI;

public class DogMonsterController : MonoBehaviour
{
    private StateMachine stateMachine;  // dog 상태머신
    private BasePatrol basePatrol;
    private BaseTracking baseTracking;
    
    public Animator anim;               // dog animator
    private NavMeshAgent agent;         // dog navmesh agent
    public Transform playerTrans;       // 플레이어 Transform

    private void Awake()
    {
        basePatrol = GetComponent<BasePatrol>();
        agent = GetComponent<NavMeshAgent>();
        baseTracking = GetComponent<BaseTracking>();
        stateMachine = new DogStateMachine(basePatrol, baseTracking, anim, agent, playerTrans);
        if (stateMachine == null)
        {
            Debug.LogError("MonsterController : stateMachine is null");
        }
    }
    
    private void Start()
    {
        // 초기 dog 상태 : Patrol
        stateMachine.ChangeState(MonsterEnums.State.Patrol);
    }
    
    private void Update()
    {
        //Debug.Log("MonsterController Updating");
        stateMachine.Update();
        

    }
}