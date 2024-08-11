using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanyEntranceController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    void Update()
    {
        canvasGroup = FindObjectOfType<CanvasGroup>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("E버튼 입력");
                SceneManager.LoadScene(0); // // Back to StartFeild
                Debug.Log("건물 입장");
                ShipDataController._isShipPosition = true;
                RightDoorData._isR_DoorPosition = true;
                LeftDoorData._isL_DoorPosition = true;

                canvasGroup.alpha = 1;
            } 
        }
    }
}