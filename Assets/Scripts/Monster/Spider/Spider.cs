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
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            statusController.TakeDamage(damage);
        }
    }
}
