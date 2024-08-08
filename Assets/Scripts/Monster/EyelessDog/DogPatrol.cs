using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogPatrol : MonoBehaviour
{
    public Vector3 waypointTarget;
    public Vector3 patrolWaypoints;
    public Coroutine patrolCoroutine = null;

    public List<Vector3> InitPatrol()
    {
        return new List<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(10, 0, 10),
            new Vector3(-10, 0, -10),
            new Vector3(10, 0, -10),
            new Vector3(-10, 0, 10)
        };
    }

    public void SetPatrolWayPoints(Vector3 posDiff)
    {
        patrolWaypoints = transform.position + posDiff;
    }

    public void UpdateDestination(List<Vector3> Monster_Patrol_Positions, NavMeshAgent agent)
    {
        List<Vector3> positionsCopy = new List<Vector3>(Monster_Patrol_Positions);
        Vector3 selectedPositions = new Vector3();

        int randomIndex = Random.Range(0, positionsCopy.Count);
        selectedPositions = positionsCopy[randomIndex];

        SetPatrolWayPoints(selectedPositions);
        waypointTarget = patrolWaypoints;
        agent.SetDestination(waypointTarget);
    }

    public void doTimer(List<Vector3> Monster_Patrol_Positions, NavMeshAgent agent)
    {
        patrolCoroutine = StartCoroutine(YieldTimer(Monster_Patrol_Positions, agent));
    }

    private IEnumerator YieldTimer(List<Vector3> Monster_Patrol_Positions, NavMeshAgent agent)
    {
        while (true)
        {
            UpdateDestination(Monster_Patrol_Positions, agent);
            yield return new WaitForSeconds(5f);
        }
    }

    public void stopPatrol()
    {
        StopCoroutine(patrolCoroutine);
        patrolCoroutine = null;
    }
}