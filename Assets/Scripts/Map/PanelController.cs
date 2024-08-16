using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public ShipController _ship;
    public RightDoorOpener _rDO;
    public LeftDoorOpener _lDO;
    private bool isTriggerActive = false;
    public AudioSource waitAudioSource;
    
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

            StartCoroutine(StopAudioSource());
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
                waitAudioSource.Play();
                waitAudioSource.loop = true;
                Debug.Log("커멘더 센터 지정 좌표로 이동");
           }

           if (Input.GetKey(KeyCode.F))
           {
               Debug.Log("F버튼 입력");
               SceneManager.LoadScene(2);
               ScrapDataContoroller._isScrapPosition = true;
           }
        }
    }

    IEnumerator StopAudioSource()
    {
        yield return new WaitForSeconds(50);
        waitAudioSource.Stop();
    }
}
