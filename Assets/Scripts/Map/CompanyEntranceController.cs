using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanyEntranceController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public AudioSource audioSource;
    void Update()
    {
        canvasGroup = FindObjectOfType<CanvasGroup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play(3);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("E버튼 입력");
                SceneManager.LoadScene(1); // // Back to StartFeild
                Debug.Log("건물 입장");
                ShipDataController._isShipPosition = true;
                RightDoorData._isR_DoorPosition = true;
                LeftDoorData._isL_DoorPosition = true;

                canvasGroup.alpha = 1;
            } 
        }
    }
}