using System;
using UnityEngine;
using UnityEngine.AI;

public class DogStateMachine : StateMachine
{
    public DogStateMachine(BasePatrol basePatrol, BaseTracking baseTracking, Animator anim, NavMeshAgent agent, Transform playerTrans)
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
    public override void ChangeState(MonsterEnums.State state)
    {
        
        string newStateName = $"Dog{state}State";
        _stateType = Type.GetType(newStateName);        // string으로 해당 상태 클래스 Type 가져오기
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
        
        

        // 변경할 상태 인스턴스 생성
        _curState = (MonsterState)Activator.CreateInstance(_stateType, new object[] { _Instance, _anim });
        
        if (_curState == null)
        {
            Debug.LogError("Statemachine : curState is null");
            return;
        }
        _curState.Enter();
    }

    public override void Update()
    {
        //Debug.Log("StateMachine Updating");
        //Debug.Log("curState : "+ _curState );
        if (_curState != null)
        {
            _curState.Execute();
        }
        else
        {
            Debug.LogError("StateMachine : curState is null");
        }
        
        // 소음 세기에 따라 상태 전환
        float noise = _anim.GetFloat("noise");
        IsCombat(noise);
        IsAttack(noise);
    }
    
    /* Combat 상태(소리 진원지로 이동) 전환 */
    public void IsCombat(float noise)
    {
        if (noise > 3f && noise < 8f)
        {
            Debug.Log("isCombatActivated");
            ChangeState(MonsterEnums.State.Combat);
        }
    }

    /* Attack 상태(소리 진원지로 달려간 후 Lunge) 전환 */
    public void IsAttack(float noise)
    {
        if (noise > 8f)
        {
            if (_curState.GetType() != typeof(DogAttackState))
            {
                _anim.SetTrigger("isAttack");
            }
            ChangeState(MonsterEnums.State.Attack);
        }
    }
}
