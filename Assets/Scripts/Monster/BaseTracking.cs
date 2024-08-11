using UnityEngine;
using UnityEngine.AI;

public class BaseTracking : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 lastPlayerPosition;         // 마지막으로 업데이트된 플레이어 위치

    public void Awake()
    {
        this.agent = GetComponent<NavMeshAgent>();
    }

    /* targetPos 트래킹 */
    public void Tracking(Vector3 targetPos, float speed, float acc)
    {
        //Debug.Log("Tracking");
        // 초기 위치 업데이트 이거나, 마지막 업데이트 위치와 현재 targetPos 위치가 다르면
        if ( lastPlayerPosition==null || Vector3.Distance(lastPlayerPosition, targetPos) > 0.1f)
        {
            agent.speed = speed;                //agent 속도 설정
            agent.acceleration = acc;           //agent 가속도 설정 : default 1000f
            agent.SetDestination(targetPos);
            lastPlayerPosition = targetPos;     // 마지막 업데이트 위치 반영.
        }
    }
    
}