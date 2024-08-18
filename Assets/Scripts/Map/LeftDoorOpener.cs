using System;
using UnityEngine;

public class LeftDoorOpener : MonoBehaviour
{
    
    public bool open = false;
    
    public void OpenLeftDoor()
    {
        Vector3 speed = Vector3.zero;
        Vector3 doorOpen = new Vector3(12f, transform.localPosition.y, transform.localPosition.z); // LocalPosition of Door
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, doorOpen, ref speed, 0.1f);
    }

    public void CloseLeftDoor()
    {
        Vector3 speed = Vector3.zero;
        Vector3 doorClose = new Vector3(3.65f, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = doorClose;
    }
    
    public void OpenLeftDoorInBuilding()
    {
        Vector3 speed = Vector3.zero;
        Vector3 doorOpen = new Vector3(20f, transform.localPosition.y, transform.localPosition.z); // LocalPosition of Door
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, doorOpen, ref speed, 0.1f);
    }
}
