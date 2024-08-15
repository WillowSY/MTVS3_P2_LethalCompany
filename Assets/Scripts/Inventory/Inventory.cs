using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // 퀵슬롯에 추가할 아이콘
    public Image quickSlotIcon_1;
    public Image quickSlotIcon_2;
    public Image quickSlotIcon_3;
    public Image quickSlotIcon_4;

    public Transform itemPoint;
    public Transform shovelPoint;
    public Transform flashLightPoint;
    
    public GameObject currentHeldItem;

    public Sprite defaultSprite;

    public GameObject player;

    // 퀵슬롯 배열. 4개의 퀵슬롯을 저장할 수 있다.
    public Transform[] quickSlots = new Transform[4];


    // 각 퀵슬롯에 저장된 아이템 배열
    public ScrapData[] scraps = new ScrapData[4];
    
    [FormerlySerializedAs("_isTwoHandedEquipped")] public bool isTwoHandedEquipped = false;

    // 아이템을 퀵슬롯에 추가하는 함수
    public void AddItemToQuickSlot(int slotIndex, ScrapData scrap)
    {
        // 인덱스가 범위를 벗어나면 아무것도 하지 않음
        if (slotIndex < 0 || slotIndex >= quickSlots.Length)
        {
            Debug.LogError("Slot index is out of range.");
            return;
        }

        if (scrap == null)
        {
            Debug.LogError("ItemData is null.");
            return;
        }

        // 퀵슬롯이 null인지 확인
        if (quickSlots[slotIndex] == null)
        {
            Debug.LogError("QuickSlot Transform is null.");
            return;
        }

        // 아이콘이 null인지 확인
        if (scrap.ScrapIcon == null)
        {
            Debug.LogWarning("Item icon is null. Assign a default icon.");
        }

        // 선택된 슬롯에 이미 아이템이 있는 경우 빈 슬롯을 찾음
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

        // 아이콘을 퀵슬롯에 설정
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

    // 아이템을 퀵슬롯에서 삭제하고, 게임 월드에 아이템을 떨어뜨리는 함수
    public void DropItemFromQuickSlot(int slotIndex)
    {
        // 인덱스가 범위를 벗어나거나 해당 인덱스에 아이템이 없으면 아무것도 하지 않음
        if (slotIndex < 0 || slotIndex >= quickSlots.Length || scraps[slotIndex] == null)
        {
            Debug.LogWarning("Cannot drop item. Index is out of range or no item in slot.");
            return;
        }

        // 아이템을 현재 위치 앞에 생성
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

        // 퀵슬롯에서 아이콘 제거
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
        
        // 아이템을 삭제한 후에는 양손 아이템 상태를 초기화
        isTwoHandedEquipped = false;

    }
    
    public void HoldItemInHand(int slotIndex)
    {
        // 손에 들고 있는 기존 아이템 제거
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
            currentHeldItem = null;
        }

        // 인덱스가 범위를 벗어나거나 해당 인덱스에 아이템이 없으면 아무것도 하지 않음
        if (slotIndex < 0 || slotIndex >= scraps.Length || scraps[slotIndex] == null)
        {
            return;
        }
        
        if (scraps[slotIndex].IsShovel)
        {
            currentHeldItem = Instantiate(scraps[slotIndex].ScrapPrefab, shovelPoint);    
        }
        else if (scraps[slotIndex].IsFlashLight)
        {
            currentHeldItem = Instantiate(scraps[slotIndex].ScrapPrefab, flashLightPoint);
        }
        // 아이템 프리팹을 손 위치에 생성
        else
        {
            currentHeldItem = Instantiate(scraps[slotIndex].ScrapPrefab, itemPoint);    
        }
        
        currentHeldItem.transform.localPosition = Vector3.zero; 
        currentHeldItem.transform.localRotation = Quaternion.identity;
        
        SetLayerRecursively(currentHeldItem, LayerMask.NameToLayer("HeldItem"));
        
        // 아이템이 양손 아이템인지 여부 확인 (예시: ScrapData에 isTwoHanded 필드가 있다고 가정)
        isTwoHandedEquipped = scraps[slotIndex].IsTwoHanded;
    }
    
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
    
    public void ReleaseItem(GameObject item)
    {
        SetLayerRecursively(item, LayerMask.NameToLayer("Default"));
    }
}