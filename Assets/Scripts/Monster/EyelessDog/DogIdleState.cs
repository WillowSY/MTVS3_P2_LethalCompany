// using UnityEngine;
// using UnityEngine.AI;
//
// public class DogIdleState : MonsterState
// {
//     public DogIdleState(StateMachine stateMachine, Animator anim)
//     {
//         this.stateMachine = stateMachine;
//         this.anim = anim;
//     }
//     
//     public override void Enter()
//     {
//         Debug.Log("Idle Enter");    
//     }
//     public override void Execute()
//     {
//         Debug.Log("Idle Execute");
//         if (IsAgentMoving())
//         {
//             anim.SetBool("isPatrol", true);
//             stateMachine.ChangeState(MonsterEnums.State.Idle);
//         }
//     }
//     public override void Exit()
//     {
//         Debug.Log("Idle Exit");
//     }
//
//     public bool IsAgentMoving()
//     {
//         if (stateMachine.agent.pathPending)
//         {
//             return false;
//         }
//
//         if (stateMachine.agent.remainingDistance > stateMachine.agent.stoppingDistance)
//         {
//             return true;
//         }
//
//         if (stateMachine.agent.velocity.sqrMagnitude > 0f)
//         {
//             return true;
//         }
//
//         return false;
//     }
// }
