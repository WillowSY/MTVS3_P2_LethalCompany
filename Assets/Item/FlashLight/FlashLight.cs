using System;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light getLight;
    public bool flashOn;
    private float time = 30f;

    void TurnOnOff()
    {
        if (flashOn)
        {
            getLight.intensity = 0;
            Debug.Log("on");
        }
        else
        {
            getLight.intensity = 5000;
            Debug.Log("Off");
        }
    }

    void Start()
    {
        flashOn = false;
    }

    private void Update()
    {
        TurnOnOff();
        if (Input.GetMouseButtonDown(0))
        {
            flashOn = !flashOn;
            FlashLightBattery();
        }
    }

    private void FlashLightBattery()
    {
        if (time > 0 && flashOn)
        {
            time -= Time.deltaTime;
        }
        else if (time <= 0f)
        {
            flashOn = false;
            Debug.Log("배터리가 없습니다.");
        }
    }
}
