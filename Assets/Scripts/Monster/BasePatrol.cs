using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasePatrol : MonoBehaviour
{
    public  Coroutine co;
    private NavMeshAgent agent;
    public Vector3 curPos;
    private NavMeshHit hit;
    
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (this.agent == null)
        {
            Debug.LogError("UpdatePatorlDestiantion : NavMeshAgent = null");
        }
    }
    
    public void StartUpdate(Vector3 curPos)
    {
        this.curPos = curPos;
        if (co == null)
        {
            co = StartCoroutine(UpdateDest());
        }
    }

    public void StopUpdate()
    {
        if (co == null)
        {
            Debug.LogError("UpdatePtrolDestination : 존재하지 않는 코루틴 정지 시도");
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
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * 5f;
                randomDirection += curPos;
                NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);
                Vector3 targetPos = hit.position;
                agent.SetDestination(targetPos);
                yield return new WaitForSeconds(4f);
            }
        }
    }
}