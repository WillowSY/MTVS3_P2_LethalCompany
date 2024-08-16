using System;
using System.Collections;
using UnityEngine;

public class ButtonOfDoor : MonoBehaviour
{
    private bool isTriggerActive = false;
    public RightDoorOpener _rDO;
    public LeftDoorOpener _lDO;
    public ScrapDataContoroller sdCon;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E))
            {
                Debug.Log("E버튼 입력");
                Debug.Log("문 닫힘");
               OnButtonPlay();
                sdCon.SavePosition();
            }

            if (Input.GetKey(KeyCode.F))
            {
                OnButtonOpen();
            }
        }
    }


    private void OnButtonPlay()
    {
        _rDO.CloseRightDoor();
        _lDO.CloseLeftDoor();
    }

    private void OnButtonOpen()
    {
        _rDO.OpenRightDoor();
        _lDO.OpenLeftDoor();
    }
}
