using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E버튼 입력");
                SceneManager.LoadScene(1); // Company Scene Start
                Debug.Log("건물 입장");
            } 
        }
    }
}
