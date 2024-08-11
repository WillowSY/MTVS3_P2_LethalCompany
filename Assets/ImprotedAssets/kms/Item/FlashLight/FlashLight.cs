using System;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light getLight;
    public bool flashOnOff = false;

    

    void Start()
    {
        getLight.enabled = flashOnOff;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TurnOnOff();
        } 
    }
    
    void TurnOnOff()
    {
        flashOnOff = !flashOnOff;
        getLight.enabled = flashOnOff;
    }
}
