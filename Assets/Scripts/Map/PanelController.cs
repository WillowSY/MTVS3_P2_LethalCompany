using System;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public ShipController _ship;
    public RightDoorOpener _rDO;
    public LeftDoorOpener _lDO;
    private bool isTriggerActive = false;
    
    void Start()
    {

    }
    
    void Update()
    {
        if (isTriggerActive)
        {
            _ship.MoveShip(); // Ship MoveTo Vector3 start
            _rDO.OpenRightDoor(); 
            _lDO.OpenLeftDoor();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           if(Input.GetKey(KeyCode.E))
           {
                Debug.Log("E버튼 입력");
                isTriggerActive = true;
                Debug.Log("커멘더 센터 지정 좌표로 이동");
           } 
        }
    }
}
