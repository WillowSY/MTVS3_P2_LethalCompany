using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField]
    private GameObject[] quickSlots;
    [SerializeField]
    private GameObject[] currentSlots;
    
    
    public Inventory inventory;
    public int currentQuickSlot = 0;

    private readonly Color _defaultColor = Color.white;
    private readonly Color _selectedColor = Color.green;
    
    private RaycastHit hit;
    private const float RayLength = 2.0f;
    private const float SphereRadius = 1f;
    
    void Update()
    {
        if (inventory == null) return;

        HandleQuickSlotSelection();
        HandleItemPickup();
        HandleItemDrop();
    }

    private void OnDrawGizmos()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        if (Physics.SphereCast(rayOrigin, SphereRadius, rayDirection, out hit, RayLength))
        {
            DrawGizmos(rayOrigin, hit.point, hit.distance);
        }
    }

    private void DrawGizmos(Vector3 origin, Vector3 hitPoint, float distance)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, Camera.main.transform.forward * distance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin + Camera.main.transform.forward * distance, SphereRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(hitPoint, SphereRadius * 0.25f);
    }

    void HandleQuickSlotSelection()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentQuickSlot = i;
                ResetSlotColors();
                HighlightSelectedSlot(currentQuickSlot);
                inventory.HoldItemInHand(currentQuickSlot);
                break;
            }
        }
    }

    void ResetSlotColors()
    {
        foreach (var slot in currentSlots)
        {
            if (slot != null)
            {
                var image = slot.GetComponent<Image>();
                if (image != null)
                    image.color = _defaultColor;
            }
        }
    }

    void HighlightSelectedSlot(int slotIndex)
    {
        var slot = currentSlots[slotIndex];
        if (slot != null)
        {
            var image = slot.GetComponent<Image>();
            if (image != null)
                image.color = _selectedColor;
        }
    }

    private void HandleItemPickup()
    {
        if (Input.GetKeyDown(KeyCode.E) && hit.transform != null)
        {
            Scrap scrap = hit.transform.GetComponent<Scrap>();
            if (scrap != null)
            {
                ScrapData scrapData = scrap.scrap;
                if (scrapData != null)
                {
                    inventory.AddItemToQuickSlot(currentQuickSlot, scrapData);
                    currentQuickSlot = FindSlotIndexForItem(scrapData);
                    inventory.HoldItemInHand(currentQuickSlot);
                    Destroy(hit.transform.gameObject);
                }
                else
                {
                    Debug.LogWarning("ItemData is null on the picked item.");
                }
            }
            else
            {
                Debug.LogWarning("No Item component found on the picked object.");
            }
        }
    }

    void HandleItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventory.DropItemFromQuickSlot(currentQuickSlot);
        }
    }

    int FindSlotIndexForItem(ScrapData item)
    {
        for (int i = 0; i < inventory.scraps.Length; i++)
        {
            if (inventory.scraps[i] == item)
                return i;
        }
        return -1;
    }
}