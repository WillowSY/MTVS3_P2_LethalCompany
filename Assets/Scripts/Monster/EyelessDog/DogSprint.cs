using UnityEngine;
using UnityEngine.AI;

public class DogSprint : MonoBehaviour
{
    public AudioClip roarSound;
    public void doSprint(NavMeshAgent agent, Vector3 target, AudioSource audioSource)
    {
        Debug.Log("Sprint");
        //agent.speed = agent.speed * 1.5f;
        //audioSource.PlayOneShot(roarSound);
        agent.speed = agent.speed * 1.5f;
        agent.acceleration = 1000f;
        agent.SetDestination(target);
    }

    public void doJumpIn(NavMeshAgent agent, Vector3 target, AudioSource audioSource)
    {
        
    }
}
