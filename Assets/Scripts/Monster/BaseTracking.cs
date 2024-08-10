using UnityEngine;
using UnityEngine.AI;

public class BaseTracking : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 lastPlayerPosition;

    public void Awake()
    {
        this.agent = GetComponent<NavMeshAgent>();
    }

    public void Tracking(Vector3 targetPos, float speed, float acc)
    {
        Debug.Log("Tracking");
        if ( lastPlayerPosition==null || Vector3.Distance(lastPlayerPosition, targetPos) > 0.1f)
        {
            agent.speed = speed;
            agent.acceleration = acc;
            agent.SetDestination(targetPos);
            lastPlayerPosition = targetPos;
        }
    }
    
}