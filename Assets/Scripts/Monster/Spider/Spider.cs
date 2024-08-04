using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonsterPattern
{
    
    public void Update()
    {
        foreach (SpiderPattern pattern in patterns)
        {
            if (pattern.IsActive())
            {
                pattern.DoPattern(transform.position);
            }
        }
    }
    // override public void Idle()
    // {
    //     Debug.Log("Spider-Idle");
    // }
    //
    // override public void Patrol()
    // {
    //     Debug.Log("Spider-Patrol");
    //     _spiderPatrol.SetAgent(agent);
    //     _spiderPatrol.UpdateDestination(transform.position, hit);
    // }
    //
    // override public void Tracking()
    // {
    //     Debug.Log("Spdier-Tracking");
    // }
    //
    // private IEnumerator YieldTimer(List<Vector3> Monster_Patrol_Positions, NavMeshAgent agent)
    // {
    //     while (true)
    //     {
    //         _spiderPatrol.UpdateDestination(transform.position, hit);
    //         yield return new WaitForSeconds(5f);
    //     }
    // }
    //
    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Web"))
    //     {
    //         if (isAlerted = false)
    //         {
    //             isAlerted = true;
    //             //basePatrol.Patrol(transform.position, hit);
    //         }
    //     }
    // }
}
