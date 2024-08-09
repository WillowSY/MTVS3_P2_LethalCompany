using System;
using UnityEngine;

public class RightDoorOpener : MonoBehaviour
{
    
    public bool open = false;

    public void ChangeR_DoorState()
    {
        open = !open;
    }

    
    public void OpenRightDoor()
    {
        Vector3 speed = Vector3.zero;
        Vector3 doorOpen = new Vector3(-13.75f, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, doorOpen, ref speed, 0.05f );
    }

    public void CloseRightDoor()
    {
        Vector3 speed = Vector3.zero;
        Vector3 doorClose = new Vector3(-5.9f, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = doorClose;
    }
}
