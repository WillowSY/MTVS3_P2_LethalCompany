using System;
using System.Collections;
using UnityEngine;


public class ShipController : MonoBehaviour
{
    private float _Sec;
    private int _Min;
    public void MoveShip()
    {
        Vector3 destination = new Vector3(19f, 0.5f, -33f);
        
        Vector3 speed = Vector3.zero;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, destination, ref speed, 0.3f);
        
    }
    
    private void Start()
    {
        Debug.Log(transform.localPosition);
        //CountTime();
    }

    void CountTime() // Timer of InGame
    {
        _Sec += Time.deltaTime * 4;
        
        Debug.Log($"{_Min}, {_Sec}");
        
        if ((int)_Sec > 59)
        {
            _Sec = 0;
            _Min++;
        }
    }
}
