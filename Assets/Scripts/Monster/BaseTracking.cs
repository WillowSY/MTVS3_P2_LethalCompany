using UnityEngine;
using UnityEngine.AI;

public class BaseTracking : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform playerTransform;
    private Vector3 lastPlayerPosition;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastPlayerPosition = playerTransform.position;
    }

    public void Tracking(float speed, float acc)
    {
        if (Vector3.Distance(lastPlayerPosition, playerTransform.position) > 0.1f)
        {
            //agent.speed = speed;
            //agent.acceleration = acc;
            agent.SetDestination(playerTransform.position);
            lastPlayerPosition = playerTransform.position;
        }
        
    }
    
}