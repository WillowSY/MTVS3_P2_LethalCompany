using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    public Inventory inventory;
    public int currentQuickSlot = 0;
    public GameObject pickSlot1;           // quickSlot UI1
    public GameObject pickSlot2;           // quickSlot UI2
    public GameObject pickSlot3;           // quickSlot UI3
    public GameObject pickSlot4;           // quickSlot UI4

    private readonly Color _defaultColor = Color.white; // 기본 퀵슬롯 색상
    private readonly Color _selectedColor = Color.green; // 선택됐을 때 색상

    void Update()
    {
        if (inventory == null) return; // inventory가 할당되지 않았을 때 조기 종료

        HandleQuickSlotSelection();
        HandleItemPickup();
        HandleItemDrop();
    }

    void HandleQuickSlotSelection()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentQuickSlot = i;
                Debug.Log($"currentQuickSlot = {currentQuickSlot + 1}");
                ResetSlotColors();
                HighlightSelectedSlot(currentQuickSlot);
                inventory.HoldItemInHand(currentQuickSlot);
                break;
            }
        }
    }

    void ResetSlotColors()
    {
        if (pickSlot1 != null)
            pickSlot1.GetComponent<Image>().color = _defaultColor;
        if (pickSlot2 != null)
            pickSlot2.GetComponent<Image>().color = _defaultColor;
        if (pickSlot3 != null)
            pickSlot3.GetComponent<Image>().color = _defaultColor;
        if (pickSlot4 != null)
            pickSlot4.GetComponent<Image>().color = _defaultColor;
    }
    
    void HighlightSelectedSlot(int slotIndex)
    {
        switch (slotIndex)
        {
            case 0:
                if (pickSlot1 != null)
                    pickSlot1.GetComponent<Image>().color = _selectedColor;
                break;
            case 1:
                if (pickSlot2 != null)
                    pickSlot2.GetComponent<Image>().color = _selectedColor;
                break;
            case 2:
                if (pickSlot3 != null)
                    pickSlot3.GetComponent<Image>().color = _selectedColor;
                break;
            case 3:
                if (pickSlot4 != null)
                    pickSlot4.GetComponent<Image>().color = _selectedColor;
                break;
        }
    }

    public void HandleItemPickup()
    {
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // 카메라의 중앙에 레이 생성

            if (Physics.Raycast(ray, out RaycastHit hit, 10, layerMask)) // 레이에 닿았을 때
            {
                Scrap scrap = hit.transform.GetComponent<Scrap>(); // 아이템 데이터를 가져옴
                if (scrap != null)
                {
                    ScrapData scrapData = scrap.scrap; // ItemData를 가져옴
                    if (scrapData != null)
                    {
                        inventory.AddItemToQuickSlot(currentQuickSlot, scrapData); // 인벤토리에 아이템데이터를 넣음
                        currentQuickSlot = FindSlotIndexForItem(scrapData);
                        inventory.HoldItemInHand(currentQuickSlot);
                        Destroy(hit.transform.gameObject); // 아이템을 줍고나서 삭제
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
    }

    void HandleItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventory.DropItemFromQuickSlot(currentQuickSlot); // 현재 슬롯의 아이템 버리기
        }
    }
    
        int FindSlotIndexForItem(ScrapData item)
        {
            for (int i = 0; i < inventory.scraps.Length; i++)
            {
                if (inventory.scraps[i] == item)
                    return i;
            }
            return -1; // 해당 아이템이 없는 경우
        }
}