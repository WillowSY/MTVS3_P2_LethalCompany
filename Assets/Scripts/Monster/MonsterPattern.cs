using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public abstract class MonsterPattern : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshHit hit;
    // public abstract void Idle();
    // public abstract void Patrol();
    // public abstract void Tracking();
    
    public List<SpiderPattern> patterns = new List<SpiderPattern>();
    public SpiderPattern curPattern;
    private BasePatrol basePatrol;
    private BaseTracking baseTracking;
    public void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        basePatrol = GetComponent<BasePatrol>();
        baseTracking = GetComponent<BaseTracking>();
        patterns.Add(new SpiderIdle());
        patterns.Add(new SpiderPatrol(agent, basePatrol));
        patterns.Add(new SpiderCombat(baseTracking));
        
       patterns[(int)PatternNumber.Pattern.Idle].SetActive(true);
       curPattern = patterns[(int)PatternNumber.Pattern.Idle];
    }
    
}
