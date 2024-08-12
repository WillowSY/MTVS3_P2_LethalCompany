using System.Collections;
using UnityEngine;

public class DogAttackState : MonsterState
{
    private bool isAttackTrackingOn = false;
    public DogAttackState(StateMachine stateMachine, Animator anim)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
    }
    
    public override void Enter()
    {
        //Debug.Log("Attack Enter");
        //FIXME : ScriptableObject로 변경
        //FIXME : Roar 상태에서 이동하지 않도록 변경.
        
        // Roar모션이 끝날때까지 기다림.
        stateMachine._baseTracking.StartCoroutine(WaitForAnimation());
        anim.SetBool("isPause", false);
    }
    public override void Execute()
    {
        Debug.Log("Combat");
        // FIXME : Execute()에서 실행이 아닌 Start()로 옮겨서 테스트 해보기
        stateMachine._baseTracking.StartCoroutine(WaitForTracking());       // Tracking 
        IsCompleteMoving();                                                       // 목표 위치 도착 시 Lunge 재생

    }
    public override void Exit()
    {
    }
    
    /* 목표 위치에 도착하면 Lunge 모션 재생 */
    public void IsCompleteMoving()
    {
        if (stateMachine != null)
        {
            if (stateMachine._agent.remainingDistance <= stateMachine._agent.stoppingDistance)
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
    
    /* 트래킹 상태가 될때까지(isAttackTrackingOn == true) 대기 후 Tracking 실행 */
    IEnumerator WaitForTracking()
    {
        yield return new WaitUntil(() => isAttackTrackingOn);
        stateMachine._baseTracking.Tracking(stateMachine._playerTrans.position, 3.5f*1.5f, 120f);
    }
}