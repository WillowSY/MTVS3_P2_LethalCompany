using System;
using UnityEngine;
using UnityEngine.AI;

// 몬스터 공통 stateMachine
public abstract class StateMachine
{
    public Transform _playerTrans;
    public MonsterState _curState;
    public Type _stateType;
    public BasePatrol _basePatrol;
    public Animator _anim;
    public NavMeshAgent _agent;
    public StateMachine _Instance;
    public BaseTracking _baseTracking;

    public abstract void ChangeState(MonsterEnums.State state);
    public abstract void Update();
    
}