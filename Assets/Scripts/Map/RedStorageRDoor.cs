using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class RedStorageRDoor : MonoBehaviour
{
    public float openAngle;
    public float closeAngle;
    public float openSpeed = 0.5f;
    public float closeSpeed = 0.5f;

    private bool isOpen = false;

    private Quaternion defaultRotation;
    private Quaternion openRotation;
    private Quaternion closeRotation;
    void Start()
    {
        defaultRotation = transform.rotation;
        openRotation = Quaternion.Euler(defaultRotation.eulerAngles + Vector3.up * openAngle);
        closeRotation = Quaternion.Euler(defaultRotation.eulerAngles + Vector3.up * closeAngle);
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            if(Input.GetKey(KeyCode.E))
            {
                isOpen = true; 
                StartCoroutine(OpenDoor());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(defaultRotation, openRotation, t);
            yield return null;
        }
    }

    IEnumerator CloseDoor()
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * closeSpeed;
            transform.rotation = Quaternion.Slerp(defaultRotation, closeRotation, t);
            yield return null;
        }
    }
    
}
