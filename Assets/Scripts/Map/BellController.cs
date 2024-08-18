using System;
using UnityEngine;

public class BellController : MonoBehaviour
{
    public AudioSource audioSource;
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
                tc.TenTacleMove();
            }
        }        
    }

    
}

