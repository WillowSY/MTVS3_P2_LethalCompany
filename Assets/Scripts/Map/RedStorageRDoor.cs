using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class RedStorageRDoor : MonoBehaviour
{
    public float openAngle;
    public float openSpeed = 0.5f;
    public float closeSpeed = 0.5f;
    private bool isOpen = false;

    private bool doorRange = false;

    private Quaternion defaultRotation;
    private Quaternion openRotation;
    private Quaternion closeRotation;
    void Start()
    {
        openRotation = Quaternion.Euler(defaultRotation.eulerAngles + Vector3.up * openAngle);
        closeRotation = transform.rotation;
    }

    void Update()
    {
        if (doorRange && Input.GetKey(KeyCode.E))
        {
            if (isOpen)
            {
                StartCoroutine(CloseDoor());
            }
            else
            {
                StartCoroutine(OpenDoor());
            }
        }
        
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            doorRange = true;
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

        transform.rotation = openRotation;
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

        transform.rotation = closeRotation;
    }
    
}
