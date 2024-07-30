using UnityEngine;

public class ButtonOfDoor : MonoBehaviour
{
    private bool isTriggerActive = false;
    public RightDoorOpener _rDO;
    public LeftDoorOpener _lDO;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E버튼 입력");
                Debug.Log("문 제어");
            } 
        }
    }
}
