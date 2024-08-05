﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : DogPattern
{
    public int hp = 10;
    
    private DogPatrol dogPatrol;
    private DogTracking dogTracking;
    private DogSprint dogSprint;
    public SoundReceiver soundReceiver;
    
    public Transform target;
    public Transform player;
    private Rigidbody rigid;
    private AudioSource audioSource;
    public AudioClip alertSound;
    public bool isPatrol = false;
    public bool isAlerted = false;
    public bool isAttack = false;
    public bool isSprint = false;
    public List<Vector3> Monster_Patrol_Positions;
    public NavMeshAgent agent;
    public Vector3 followPosition;
    private Vector3 localRotate;
    public float distance;
    public int damage = 100;
    public StatusController statusController;

    public void Start()
    {
        dogPatrol = GetComponent<DogPatrol>();
        dogTracking = GetComponent<DogTracking>();
        dogSprint = GetComponent<DogSprint>();
        Monster_Patrol_Positions = dogPatrol.InitPatrol();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Patrol();
    }
    
    public void Update()
    {
        if (soundReceiver.IsPatorl())
        {
             Patrol();
        }
        if (soundReceiver.IsDetected())
        {
            Tracking();
        }
        if (soundReceiver.IsSprint())
        {
            Sprint();
        }
    }

    // 모듈리스트
    override public void Idle()
    {
        Debug.Log("Dog-Idle");
    }
    
    override public void Patrol()
    {
        if (!isPatrol)
        {
            Debug.Log("Dog-Patrol");
            dogPatrol.doTimer(Monster_Patrol_Positions, agent);
            isPatrol = true;
        }
        
    }
    
    override public void Tracking()
    {
        if (!isAlerted)
        {
            followPosition = player.position;
        }
    
        isPatrol = false;
        isAlerted = true;
        isSprint = false;
        
        if(dogPatrol.patrolCoroutine!=null){
            dogPatrol.stopPatrol();
        }

    
        if (isAlerted)
        {
           dogTracking.TurnToPlayer(agent, target, audioSource, alertSound, followPosition);
            Debug.Log("Alerted");
        }
    }
    public void Sprint()
    {
        Debug.Log("DogSprint");
        if (!isSprint)
        {
            isSprint = true;
            followPosition = player.position;
            dogSprint.doSprint(agent, followPosition, audioSource);
        }
    }
    
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            statusController.TakeDamage(damage);
        }
    }
    
    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
    
}
