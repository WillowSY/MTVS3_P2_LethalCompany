using System;
using UnityEngine;

public class BellController : MonoBehaviour
{
    public AudioSource audioSource;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                audioSource.Play();
            }
        }
    }
}
