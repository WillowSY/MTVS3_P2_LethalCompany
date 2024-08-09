using UnityEngine;
using UnityEngine.AI;

public class SpiderPatrol : SpiderPattern
{
    private NavMeshAgent agent;
    public NavMeshHit hit;
    public Vector3 curPos;
    public BasePatrol basePatrol;

    public SpiderPatrol(NavMeshAgent agent, BasePatrol basePatrol)
    {
        this.agent = agent;
        this.basePatrol = basePatrol;
    }
    public override void EnterPattern(Vector3 curPos)
    {
        SetActive(true);
        this.curPos = curPos;
        basePatrol.StartUpdate(curPos);
    }
    public override void DoPattern(Vector3 curPos)
    {
        Debug.Log("Spider: Patorl");
        basePatrol.curPos = curPos;
    }
    public override void ExitPattern()
    {
        SetActive(false);
        basePatrol.StopUpdate();
    }
    
    
    
    
    
    

}