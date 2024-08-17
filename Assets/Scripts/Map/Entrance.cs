using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    private ShipDataController sm;
    private RightDoorData rd;
    private LeftDoorData ld;
    public ShipController ship;
    public CanvasGroup canvasGroup;
    private void Start()
    {
        sm = FindObjectOfType<ShipDataController>();
        rd = FindObjectOfType<RightDoorData>();
        ld = FindObjectOfType<LeftDoorData>();
        canvasGroup = FindObjectOfType<CanvasGroup>();
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("E버튼 입력");
                SceneManager.LoadScene(2); // Company Scene Start
                Debug.Log("건물 입장");
                
                sm.SavePosition();
                Debug.Log(ship.transform.position);
                rd.SavePosition();
                ld.SavePosition();

                canvasGroup.alpha = 0;
            } 
        }
    }
}
