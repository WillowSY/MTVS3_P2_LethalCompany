using System;
using System.Collections;
using UnityEngine;

public class BellController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource tAudioSource;
    public TentacleController tc;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                audioSource.Play();
                StartCoroutine(TentacleSound());
                StartCoroutine(TentacleAttack());
            }
        }        
    }

    IEnumerator TentacleSound()
    {
        yield return new WaitForSeconds(1);
        tAudioSource.Play();
        
    }

    IEnumerator TentacleAttack()
    {
        yield return new WaitForSeconds (3);
        tc.TenTacleMove();
    }
}

