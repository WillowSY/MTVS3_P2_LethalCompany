using System;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light getLight;
    public bool flashOnOff = false;
    private Inventory _inventory;
    private PlayerRaycast _playerRaycast;

    

    void Start()
    {
        _inventory = FindFirstObjectByType<Inventory>();
        _playerRaycast = FindFirstObjectByType<PlayerRaycast>();
        getLight.enabled = flashOnOff;
    }

    private void Update()
    {
        if (_inventory.scraps[_playerRaycast.currentQuickSlot] != null && 
            _inventory.scraps[_playerRaycast.currentQuickSlot].IsFlashLight)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TurnOnOff();
            } 
        }
    }
    
    void TurnOnOff()
    {
        flashOnOff = !flashOnOff;
        getLight.enabled = flashOnOff;
    }
}
