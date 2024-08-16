using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // 퀵슬롯에 추가할 아이콘 이미지들
    public Image quickSlotIcon_1;
    public Image quickSlotIcon_2;
    public Image quickSlotIcon_3;
    public Image quickSlotIcon_4;

    // 아이템이 장착될 위치들
    public Transform itemPoint;
    public Transform shovelPoint;
    public Transform flashLightPoint;
    
    // 현재 플레이어가 손에 들고 있는 아이템
    public GameObject currentHeldItem;

    // 기본 아이콘 스프라이트 (아이템이 없을 때 보여줄 이미지)
    public Sprite defaultSprite;

    // 플레이어 객체
    public GameObject player;

    // 4개의 퀵슬롯을 저장할 수 있는 배열
    public Transform[] quickSlots = new Transform[4];
    
    // 각 퀵슬롯에 저장된 아이템 데이터를 저장할 배열
    public ScrapData[] scraps = new ScrapData[4];
    
    // 두 손으로 아이템을 들고 있는지 여부를 나타내는 플래그
    [FormerlySerializedAs("_isTwoHandedEquipped")] public bool isTwoHandedEquipped = false;
    
    // 퀵슬롯의 기본 색상과 선택된 색상
    private readonly Color _defaultColor = Color.white;
    private readonly Color _selectedColor = Color.green;

    // 아이템을 퀵슬롯에 추가하는 함수
    public void AddItemToQuickSlot(int slotIndex, ScrapData scrap)
    {
        // 인덱스가 유효 범위를 벗어나면 오류 로그를 출력하고 함수를 종료
        if (slotIndex < 0 || slotIndex >= quickSlots.Length)
        {
            Debug.LogError("Slot index is out of range.");
            return;
        }

        // 추가하려는 아이템 데이터가 null이면 오류 로그를 출력하고 함수를 종료
        if (scrap == null)
        {
            Debug.LogError("ItemData is null.");
            return;
        }

        // 퀵슬롯이 할당되어 있지 않으면 오류 로그를 출력하고 함수를 종료
        if (quickSlots[slotIndex] == null)
        {
            Debug.LogError("QuickSlot Transform is null.");
            return;
        }

        // 아이템 아이콘이 null인지 확인하고 경고 로그 출력
        if (scrap.ScrapIcon == null)
        {
            Debug.LogWarning("Item icon is null. Assign a default icon.");
        }

        // 선택된 슬롯에 이미 아이템이 있는 경우, 빈 슬롯을 찾음
        if (scraps[slotIndex] != null)
        {
            slotIndex = FindNextEmptySlot();
            if (slotIndex == -1)
            {
                Debug.LogWarning("No empty quick slots available.");
                return;
            }
        }

        // 해당 슬롯에 아이템 데이터 저장
        scraps[slotIndex] = scrap;

        // 아이템의 아이콘을 해당 퀵슬롯에 설정
        switch (slotIndex)
        {
            case 0:
                if (quickSlotIcon_1 != null)
                    quickSlotIcon_1.sprite = scrap.ScrapIcon;
                break;
            case 1:
                if (quickSlotIcon_2 != null)
                    quickSlotIcon_2.sprite = scrap.ScrapIcon;
                break;
            case 2:
                if (quickSlotIcon_3 != null)
                    quickSlotIcon_3.sprite = scrap.ScrapIcon;
                break;
            case 3:
                if (quickSlotIcon_4 != null)
                    quickSlotIcon_4.sprite = scrap.ScrapIcon;
                break;
        }
        
        // 모든 퀵슬롯의 색상을 기본 색상으로 초기화한 후, 선택된 슬롯을 강조
        ResetAllSlotColors();
        HighlightSlot(slotIndex);

        // 빈 슬롯을 찾는 내부 함수
        int FindNextEmptySlot()
        {
            for (int i = 0; i < scraps.Length; i++)
            {
                if (scraps[i] == null)
                    return i;
            }

            return -1;
        }

    }
    
    // 특정 퀵슬롯을 선택하는 함수
    public void SelectQuickSlot(int slotIndex)
    {
        // 모든 퀵슬롯의 색상을 기본 색상으로 초기화한 후, 선택된 퀵슬롯만 초록색으로 변경
        ResetAllSlotColors();
        HighlightSlot(slotIndex);

        // 퀵슬롯 선택에 따른 추가 로직 (예: 아이템 장착)
        HoldItemInHand(slotIndex);
    }
    
    // 모든 퀵슬롯의 색상을 기본 색상으로 초기화하는 함수
    private void ResetAllSlotColors()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            ResetSlotColor(i);
        }
    }
    
    // 특정 퀵슬롯의 색상을 강조하는 함수 (초록색으로 변경)
    private void HighlightSlot(int slotIndex)
    {
        if (quickSlots[slotIndex] != null)
        {
            var image = quickSlots[slotIndex].GetComponent<Image>();
            if (image != null)
            {
                image.color = _selectedColor;
            }
        }
    }
    
    // 특정 퀵슬롯의 색상을 기본 색상으로 초기화하는 함수
    private void ResetSlotColor(int slotIndex)
    {
        if (quickSlots[slotIndex] != null)
        {
            var image = quickSlots[slotIndex].GetComponent<Image>();
            if (image != null)
            {
                image.color = _defaultColor;
            }
        }
    }
    
    // 아이템을 퀵슬롯에서 삭제하고, 게임 월드에 아이템을 떨어뜨리는 함수
    public void DropItemFromQuickSlot(int slotIndex)
    {
        // 인덱스가 범위를 벗어나거나 해당 인덱스에 아이템이 없으면 경고 로그를 출력하고 함수를 종료
        if (slotIndex < 0 || slotIndex >= quickSlots.Length || scraps[slotIndex] == null)
        {
            Debug.LogWarning("Cannot drop item. Index is out of range or no item in slot.");
            return;
        }

        // 플레이어의 현재 위치 앞에 아이템을 생성
        Vector3 dropPosition = player.transform.position + player.transform.forward;
        GameObject droppedItem =
            Instantiate(scraps[slotIndex].ScrapPrefab, dropPosition, Quaternion.identity);
        ReleaseItem(droppedItem);

        // 해당 인덱스의 아이템을 null로 설정
        scraps[slotIndex] = null;

        // 손에 들고 있는 아이템을 제거
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
            currentHeldItem = null;
        }

        // 퀵슬롯에서 아이콘을 기본 아이콘으로 변경
        switch (slotIndex)
        {
            case 0:
                if (quickSlotIcon_1 != null)
                    quickSlotIcon_1.sprite = defaultSprite;
                break;
            case 1:
                if (quickSlotIcon_2 != null)
                    quickSlotIcon_2.sprite = defaultSprite;
                break;
            case 2:
                if (quickSlotIcon_3 != null)
                    quickSlotIcon_3.sprite = defaultSprite;
                break;
            case 3:
                if (quickSlotIcon_4 != null)
                    quickSlotIcon_4.sprite = defaultSprite;
                break;
        }
        
        // 아이템을 삭제한 후 양손 아이템 상태를 초기화
        isTwoHandedEquipped = false;
    }
    
    // 퀵슬롯에서 선택된 아이템을 손에 들게 하는 함수
    public void HoldItemInHand(int slotIndex)
    {
        // 손에 들고 있는 기존 아이템을 제거
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
            currentHeldItem = null;
        }

        // 인덱스가 유효 범위를 벗어나거나 해당 인덱스에 아이템이 없으면 함수를 종료
        if (slotIndex < 0 || slotIndex >= scraps.Length || scraps[slotIndex] == null)
        {
            return;
        }
        
        // 아이템이 삽인지 확인하고, 그렇다면 삽 위치에 아이템을 생성
        if (scraps[slotIndex].IsShovel)
        {
            currentHeldItem = Instantiate(scraps[slotIndex].ScrapPrefab, shovelPoint);    
        }
        // 아이템이 손전등인지 확인하고, 그렇다면 손전등 위치에 아이템을 생성
        else if (scraps[slotIndex].IsFlashLight)
        {
            currentHeldItem = Instantiate(scraps[slotIndex].ScrapPrefab, flashLightPoint);
        }
        // 기본 아이템 위치에 아이템을 생성
        else
        {
            currentHeldItem = Instantiate(scraps[slotIndex].ScrapPrefab, itemPoint);    
        }
        
        // 생성된 아이템의 위치와 회전을 초기화
        currentHeldItem.transform.localPosition = Vector3.zero; 
        currentHeldItem.transform.localRotation = Quaternion.identity;
        
        // 생성된 아이템의 레이어를 "HeldItem"으로 설정
        SetLayerRecursively(currentHeldItem, LayerMask.NameToLayer("HeldItem"));
        
        // 아이템이 양손 아이템인지 확인하고 상태를 설정
        isTwoHandedEquipped = scraps[slotIndex].IsTwoHanded;
    }
    
    // 아이템과 그 하위 객체들의 레이어를 재귀적으로 설정하는 함수
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
    
    // 아이템을 월드에 드롭했을 때 레이어를 "Default"로 설정하는 함수
    public void ReleaseItem(GameObject item)
    {
        SetLayerRecursively(item, LayerMask.NameToLayer("Default"));
    }
}