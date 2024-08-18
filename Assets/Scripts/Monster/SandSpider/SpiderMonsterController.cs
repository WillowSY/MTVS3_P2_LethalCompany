using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    private StateMachine stateMachine;      // spider 상태 머신
    private BasePatrol basePatrol;
    private BaseTracking baseTracking;
    private WebTrigger webTrigger;
    
    public Animator anim;                   // spider animator
    private NavMeshAgent agent;             // spider navmesh agent
    public Transform playerTrans;           // 플레이어 Transform
    
    private void Awake()
    {
        basePatrol = GetComponent<BasePatrol>();
        agent = GetComponent<NavMeshAgent>();
        baseTracking = GetComponent<BaseTracking>();

        if (basePatrol == null)
        {
            Debug.Log("SpiderMonsterController : basePatrol is null");
        }
        if (agent == null)
        {
            Debug.Log("SpiderMonsterController : agent is null");
        }
        if (baseTracking == null)
        {
            Debug.Log("SpiderMonsterController : baseTracking is null");
        }
        playerTrans = GameObject.FindWithTag("Player").transform;
        Debug.Log(playerTrans);
        stateMachine = new SpiderStateMachine(basePatrol, baseTracking, anim, agent, playerTrans);
        Debug.Log(stateMachine);
        
        
        webTrigger = GameObject.Find("Webs").GetComponent<WebTrigger>();
        if (webTrigger == null)
        {
            Debug.Log("SpiderMonsterController : webTrigger is null");
        }
        if (stateMachine == null)
        {
            Debug.LogError("MonsterController : stateMachine is null");
        }
        Debug.Log(webTrigger);
    }
    private void Start()
    {
        
        // 초기 spider 상태 : Idle
        stateMachine.ChangeState(MonsterEnums.State.Idle);
    }
    public void Update()
    {
        // webTriggerOn 여부 판별
        SetWebTriggerOn();
        stateMachine.Update();
    }

    public void SetWebTriggerOn()
    {
        if (webTrigger.isWebTriggerOn)
        {
            anim.SetBool("isWebTriggerOn", true);
        }
    }
}