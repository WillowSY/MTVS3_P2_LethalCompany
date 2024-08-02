<<<<<<< Updated upstream
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

=======
using UnityEngine;
>>>>>>> Stashed changes
public class PlayerRaycast : MonoBehaviour
{
    public Inventory inventory;
    public int currentQuickSlot = 0;

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
                break;
            }
        }
    }

    public void HandleItemPickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라의 중앙에 레이 생성
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // 레이에 닿았을 때
            {
                Item item = hit.transform.GetComponent<Item>(); // 아이템 데이터를 가져옴
                if (item != null)
                {
                    ItemData itemData = item.item; // ItemData를 가져옴
                    if (itemData != null)
                    {
                        inventory.AddItemToQuickSlot(currentQuickSlot, itemData); // 인벤토리에 아이템데이터를 넣음
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
}