using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasePatrol : MonoBehaviour
{
    public Coroutine co;
    private NavMeshAgent agent;
    private NavMeshHit hit;
    public Animator anim;
    
    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("BasePatorl : NavMeshAgent is null");
        }
        if (anim == null)
        {
            Debug.LogError("BasePatorl : Animator is null");
        }
    }
    
    public void StartUpdate()
    {
        if (co == null)
        {
            co = StartCoroutine(UpdateDest());
        }
    }

    public void StopUpdate()
    {
        if (co == null)
        {
            Debug.LogError("BasePatrol : 존재하지 않는 코루틴 정지 시도");
        }
        else
        {
            StopCoroutine(co);
        }
    }

    public IEnumerator UpdateDest()
    {
        while (true)
        {
            if (agent == null)
            {
                Debug.LogError("BasePatrol : agent is null!");
            }
            
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * 15f;
                randomDirection += transform.position;
                NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);
                Vector3 targetPos = hit.position;
                agent.SetDestination(targetPos);
                yield return new WaitForSeconds(5f);
            }
        }
    }
}