using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    private StateMachine stateMachine;      // spider 상태 머신
    private BasePatrol basePatrol;
    private BaseTracking baseTracking;
    public WebTrigger webTrigger;
    
    public Animator anim;                   // spider animator
    private NavMeshAgent agent;             // spider navmesh agent
    public Transform playerTrans;           // 플레이어 Transform
    
    private void Awake()
    {
        basePatrol = GetComponent<BasePatrol>();
        agent = GetComponent<NavMeshAgent>();
        baseTracking = GetComponent<BaseTracking>();
        
        stateMachine = new SpiderStateMachine(basePatrol, baseTracking, anim, agent, playerTrans);
        if (stateMachine == null)
        {
            Debug.LogError("MonsterController : stateMachine is null");
        }
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