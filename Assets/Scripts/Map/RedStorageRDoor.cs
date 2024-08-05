using System.Collections;
using UnityEngine;

public class RedStorageRDoor : MonoBehaviour
{
    public float openAngle;

    public float openSpeed = 0.5f;

    private bool isOpen = false;

    private Quaternion defaultRotation;

    private Quaternion openRotation;
    void Start()
    {
        defaultRotation = transform.rotation;
        openRotation = Quaternion.Euler(defaultRotation.eulerAngles + Vector3.up * openAngle);
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
    
}
