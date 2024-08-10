using System;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine
{
    private Transform _playerTrans;
    private MonsterState _curState;
    private Type _stateType;
    private BasePatrol _basePatrol;
    private Animator _anim;
    private NavMeshAgent _agent;
    private StateMachine _Instance;
    private BaseTracking _baseTracking;

    public Transform playerTrans
    {
        get { return _playerTrans; }
    }
    public BasePatrol basePatrol
    {
        get{ return _basePatrol; }
    }

    public BaseTracking baseTracking
    {
        get { return _baseTracking;  }
    }
    public Animator anim
    {
        get{ return _anim; }
    }

    public NavMeshAgent agent
    {
        get { return _agent; }
    }
    public StateMachine(BasePatrol basePatrol, BaseTracking baseTracking, Animator anim, NavMeshAgent agent, Transform playerTrans)
    {
        this._basePatrol = basePatrol;
        this._baseTracking = baseTracking;
        this._anim = anim;
        this._agent = agent;
        this._playerTrans = playerTrans;
        _Instance = this;
        Debug.Log("Statemachine Instance : " + _Instance);
        if (this._basePatrol == null)
        {
            Debug.LogError("Statemachine : basePatrol is null.");
            return;
        }
        if (this._anim == null)
        {
            Debug.LogError("Statemachine : anim is null.");
            return;
        }
        if (this._agent == null)
        {
            Debug.LogError("Statemachine : agent is null.");
            return;
        }
    }
    public void ChangeState(MonsterEnums.State state)
    {
        
        string newStateName = $"{state}State";
        _stateType = Type.GetType(newStateName);
        if (_stateType == null)
        {
            Debug.LogError("Statemachine : stateType is null");
            return;
        }
        if (_curState != null)
        {
            _curState.Exit();
            if(_curState.GetType() == _stateType)
            {
                return;
            }
        }
        
        

        // 상태 클래스 생성
        _curState = (MonsterState)Activator.CreateInstance(_stateType, new object[] { _Instance, _anim });
        // 상태 커스텀 NavMeshAgent 초기화 설정
        // methodInfo = stateType.GetMethod("SetAgent", BindingFlags.Instance | BindingFlags.Public);
        // if (methodInfo!=null)
        // {
        //     methodInfo.Invoke(curState, new object[] {agent});
        // }
        // // 상태 커스텀 BasePatrol 초기화 설정
        // methodInfo = stateType.GetMethod("SetBasePatrol", BindingFlags.Instance | BindingFlags.Public);
        // if (methodInfo!=null)
        // {
        //     methodInfo.Invoke(curState, new object[] {basePatrol});
        // }
        
        if (_curState == null)
        {
            Debug.LogError("Statemachine : curState is null");
            return;
        }
        _curState.Enter();
    }

    public void Update()
    {
        Debug.Log("StateMachine Updating");
        //Debug.Log("curState : "+ _curState );
         if (_curState != null)
         {
             _curState.Execute();
         }
         else
         {
             Debug.LogError("StateMachine : curState is null");
         }
         
         float noise = anim.GetFloat("noise");
         IsCombat(noise);
         IsAttack(noise);
    }

    public void IsCombat(float noise)
    {
        if (noise > 3f && noise < 8f)
        {
            Debug.Log("isCombatActivated");
            ChangeState(MonsterEnums.State.Combat);
        }
    }

    public void IsAttack(float noise)
    {
        if (noise > 8f)
        {
            if (_curState.GetType() != typeof(AttackState))
            {
                anim.SetTrigger("isAttack");
            }
            ChangeState(MonsterEnums.State.Attack);
        }
    }
}