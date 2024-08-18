using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private GameObject[] quickSlots;
    [SerializeField] private GameObject[] currentSlots;
    public Inventory inventory;
    public int currentQuickSlot = 0;

    private RaycastHit hit;
    private const float RayLength = 2.0f;
    private const float SphereRadius = 1f;

    [SerializeField] private GameObject getItemMsg;
    [SerializeField] private GameObject twoHandedMsg;

    private SoundEmitter _soundEmitter;

    private void Start()
    {
        _soundEmitter = FindFirstObjectByType<SoundEmitter>();
    }

    void Update()
    {
        if (inventory == null) return;

        HandleQuickSlotSelection();
        HandleItemPickup();
        HandleItemDrop();
        UpdateUI();
    }

    private void OnDrawGizmos()
    {
        if (hit.transform != null)
        {
            Vector3 rayOrigin = Camera.main.transform.position;
            Vector3 rayDirection = Camera.main.transform.forward;
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
        if (inventory.isTwoHandedEquipped)
        {
            Debug.Log("Cannot change quick slots while holding a two-handed item.");
            return;
        }

        int quickSlotsLength = quickSlots.Length;
        for (int i = 0; i < quickSlotsLength; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentQuickSlot = i;
                inventory.SelectQuickSlot(currentQuickSlot);
                break;
            }
        }
    }

    private void HandleItemPickup()
    {
        if (inventory.isTwoHandedEquipped)
        {
            Debug.Log("Cannot pick up items while holding a two-handed item.");
            return;
        }

        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        // 디버깅용 로그: 레이캐스트 시작 위치와 방향 확인
        Debug.Log($"Ray Origin: {rayOrigin}, Ray Direction: {rayDirection}");

        // 레이캐스트 실행
        if (Physics.SphereCast(rayOrigin, SphereRadius, rayDirection, out hit, RayLength))
        {
            Debug.Log("Raycast hit something!");

            // Scrap 컴포넌트를 확인
            Scrap scrap = hit.transform.GetComponent<Scrap>();

            // 디버깅용 로그: Scrap 컴포넌트 확인
            Debug.Log(scrap != null ? $"Hit object with Scrap component: {scrap.name}" : "No Scrap component found on hit object.");

            // scrap이 null이 아닌 경우 UI 메시지 활성화
            if (scrap != null)
            {
                getItemMsg.SetActive(true);

                // 디버깅용 로그: UI 메시지 활성화 확인
                Debug.Log("getItemMsg UI is set to active.");

                // 아이템 픽업 로직
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ScrapData scrapData = scrap.scrap;
                    if (scrapData != null)
                    {
                        inventory.AddItemToQuickSlot(currentQuickSlot, scrapData);
                        currentQuickSlot = FindSlotIndexForItem(scrapData);
                        inventory.SelectQuickSlot(currentQuickSlot);
                        Destroy(hit.transform.gameObject);
                        _soundEmitter.PlayDropItem();
                    }
                    else
                    {
                        Debug.LogWarning("ItemData is null on the picked item.");
                    }
                }
            }
            else
            {
                // scrap이 없으면 UI 메시지 비활성화
                getItemMsg.SetActive(false);
            }
        }
        else
        {
            // 디버깅용 로그: 레이캐스트 실패 확인
            Debug.Log("Raycast did not hit anything.");
            
            // 레이캐스트가 아무것도 감지하지 않으면 UI 메시지 비활성화
            getItemMsg.SetActive(false);
        }
    }

    void HandleItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventory.DropItemFromQuickSlot(currentQuickSlot);
            _soundEmitter.PlayDropItem();
        }
    }

    int FindSlotIndexForItem(ScrapData item)
    {
        int inventoryLength = inventory.scraps.Length;
        for (int i = 0; i < inventoryLength; i++)
        {
            if (inventory.scraps[i] == item)
                return i;
        }
        return 0;
    }

    private void UpdateUI()
    {
        twoHandedMsg.SetActive(inventory.isTwoHandedEquipped);
    }
}