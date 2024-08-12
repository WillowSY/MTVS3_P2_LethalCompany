using UnityEngine;

public abstract class MonsterState
{
    public StateMachine stateMachine;
    public Animator anim;
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
    
}
