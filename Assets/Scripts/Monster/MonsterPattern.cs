using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterPattern : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshHit hit;

    // public List<SpiderPattern> patterns = new List<SpiderPattern>();
    // public SpiderPattern curPattern;
    // private BasePatrol basePatrol;
    // private BaseTracking baseTracking;
    //
    // public StatusController statusController;
    //
    // public int damage = 50;
    //
    //
    // public void Start()
    // {
    //     agent = GetComponent<NavMeshAgent>();
    //     basePatrol = GetComponent<BasePatrol>();
    //     baseTracking = GetComponent<BaseTracking>();
    //     patterns.Add(new SpiderIdle());
    //     patterns.Add(new SpiderPatrol(basePatrol));
    //     patterns.Add(new SpiderCombat(baseTracking));
    //
    //     patterns[(int)PatternNumber.Pattern.Idle].SetActive(true);
    //     curPattern = patterns[(int)PatternNumber.Pattern.Idle];
    //     Debug.Log("MonsterPattern curPattern: "+curPattern);
    // }
}
