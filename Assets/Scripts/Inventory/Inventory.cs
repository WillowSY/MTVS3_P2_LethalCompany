using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // 퀵슬롯 아이콘 이미지들
    [SerializeField] private Image[] quickSlotIcons = new Image[4];
    public Transform itemPoint;
    public Transform shovelPoint;
    public Transform flashLightPoint;
    
    public GameObject currentHeldItem;
    public Sprite defaultSprite;
    public GameObject player;
    
    // 퀵슬롯 배열과 각 슬롯의 아이템 데이터
    public Transform[] quickSlots = new Transform[4];
    public ScrapData[] scraps = new ScrapData[4];
    
    [FormerlySerializedAs("_isTwoHandedEquipped")] public bool isTwoHandedEquipped = false;
    
    private readonly Color _defaultColor = Color.white;
    private readonly Color _selectedColor = Color.green;

    // 아이템을 퀵슬롯에 추가
    public void AddItemToQuickSlot(int slotIndex, ScrapData scrap)
    {
        if (IsInvalidSlotIndex(slotIndex) || scrap == null || quickSlots[slotIndex] == null)
        {
            Debug.LogError("Invalid slot index or scrap data.");
            return;
        }

        // 빈 슬롯 찾기
        if (scraps[slotIndex] != null)
        {
            slotIndex = FindNextEmptySlot();
            if (slotIndex == -1)
            {
                Debug.LogWarning("No empty quick slots available.");
                return;
            }
        }

        // 슬롯에 아이템 추가
        scraps[slotIndex] = scrap;
        Debug.Log("Inventory : slot index["+slotIndex+"] icon [ "+scrap.ScrapIcon);
        UpdateQuickSlotIcon(slotIndex, scrap.ScrapIcon);

        ResetAllSlotColors();
        HighlightSlot(slotIndex);
    }

    // 퀵슬롯 선택
    public void SelectQuickSlot(int slotIndex)
    {
        if (IsInvalidSlotIndex(slotIndex))
        {
            Debug.LogError("Invalid slot index.");
            return;
        }

        ResetAllSlotColors();
        HighlightSlot(slotIndex);
        HoldItemInHand(slotIndex);
    }
    
    // 퀵슬롯의 모든 색상을 기본 색상으로 초기화
    private void ResetAllSlotColors()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            ResetSlotColor(i);
        }
    }

    // 퀵슬롯 색상 강조
    private void HighlightSlot(int slotIndex)
    {
        SetSlotColor(slotIndex, _selectedColor);
    }

    // 퀵슬롯 색상 초기화
    private void ResetSlotColor(int slotIndex)
    {
        SetSlotColor(slotIndex, _defaultColor);
    }

    // 슬롯 색상 설정
    private void SetSlotColor(int slotIndex, Color color)
    {
        if (quickSlots[slotIndex] != null)
        {
            var image = quickSlots[slotIndex].GetComponent<Image>();
            if (image != null)
            {
                image.color = color;
            }
        }
    }
    
    // 아이템을 퀵슬롯에서 삭제하고, 게임 월드에 아이템을 떨어뜨림
    public void DropItemFromQuickSlot(int slotIndex)
    {
        if (IsInvalidSlotIndex(slotIndex) || scraps[slotIndex] == null)
        {
            Debug.LogWarning("Cannot drop item. Invalid index or no item in slot.");
            return;
        }

        // 아이템 떨어뜨리기
        Vector3 dropPosition = player.transform.position + player.transform.forward;
        GameObject droppedItem = Instantiate(scraps[slotIndex].ScrapPrefab, dropPosition, Quaternion.identity);
        ReleaseItem(droppedItem);

        // 슬롯과 현재 들고 있는 아이템 초기화
        scraps[slotIndex] = null;
        DestroyCurrentHeldItem();
        UpdateQuickSlotIcon(slotIndex, defaultSprite);
        isTwoHandedEquipped = false;
    }

    // 현재 아이템을 손에 들게 설정
    public void HoldItemInHand(int slotIndex)
    {
        DestroyCurrentHeldItem();

        if (IsInvalidSlotIndex(slotIndex) || scraps[slotIndex] == null)
        {
            return;
        }

        // 아이템 프리팹 생성
        Transform point = scraps[slotIndex].IsShovel ? shovelPoint :
                          scraps[slotIndex].IsFlashLight ? flashLightPoint :
                          itemPoint;

        currentHeldItem = Instantiate(scraps[slotIndex].ScrapPrefab, point);
        currentHeldItem.transform.localPosition = Vector3.zero;
        currentHeldItem.transform.localRotation = Quaternion.identity;
        
        SetLayerRecursively(currentHeldItem, LayerMask.NameToLayer("HeldItem"));

        // 양손 아이템 상태 설정
        isTwoHandedEquipped = scraps[slotIndex].IsTwoHanded;
    }

    // 현재 들고 있는 아이템 삭제
    private void DestroyCurrentHeldItem()
    {
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
            currentHeldItem = null;
        }
    }

    // 모든 자식 오브젝트에 대해 레이어 설정
    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
    
    // 아이템을 기본 레이어로 설정
    public void ReleaseItem(GameObject item)
    {
        SetLayerRecursively(item, LayerMask.NameToLayer("Default"));
    }

    // 빈 슬롯 찾기
    private int FindNextEmptySlot()
    {
        for (int i = 0; i < scraps.Length; i++)
        {
            if (scraps[i] == null)
                return i;
        }
        return -1;
    }

    // 퀵슬롯 아이콘 업데이트
    private void UpdateQuickSlotIcon(int slotIndex, Sprite icon)
    {
        if (slotIndex >= 0 && slotIndex < quickSlotIcons.Length && quickSlotIcons[slotIndex] != null)
        {
            quickSlotIcons[slotIndex].sprite = icon;
        }
    }

    // 슬롯 인덱스 유효성 검사
    private bool IsInvalidSlotIndex(int slotIndex)
    {
        return slotIndex < 0 || slotIndex >= quickSlots.Length;
    }
}