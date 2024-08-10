using System.Collections;
using UnityEngine;

public class AttackState : MonsterState
{
    private bool isAttackTrackingOn = false;
    public AttackState(StateMachine stateMachine, Animator anim)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
    }
    
    public override void Enter()
    {
        Debug.Log("Attack Enter");
        //FIXME : ScriptableObject로 변경
        
        // Roar 하기
        anim.SetBool("isAttack", true);
        stateMachine.baseTracking.StartCoroutine(WaitForAnimation());
        // Roar 모션 끝나면 Tracking (소리 근원지 계속 따라다님)
        // 근원지 도착하면 일정시간동안 Lunge 반복
        anim.SetBool("isPause", false);
    }
    public override void Execute()
    {
        Debug.Log("Combat");
        stateMachine.baseTracking.StartCoroutine(WaitForTracking());
        IsAgentMoving();

    }
    public override void Exit()
    {
    }
    public void IsAgentMoving()
    {
        if (stateMachine != null)
        {
            if (stateMachine.agent.remainingDistance <= stateMachine.agent.stoppingDistance)
            {
                anim.SetBool("isAttackLunge", true);
            }
            else
            {
                anim.SetBool("isAttackLunge", false);
            }
        }
    }

    /* 애니메이션 길이만큼 대기 */
    IEnumerator WaitForAnimation()
    {
        Debug.Log("애니메이션 재생 시간 : "+anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return null;
    }
    IEnumerator WaitForTracking()
    {
        yield return new WaitUntil(() => isAttackTrackingOn);
        stateMachine.baseTracking.Tracking(stateMachine.playerTrans.position, 3.5f, 120f);
    }
}