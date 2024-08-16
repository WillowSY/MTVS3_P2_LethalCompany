using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private GameObject[] quickSlots;
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

    private void Update()
    {
        if (inventory == null) return;

        HandleQuickSlotSelection();
        HandleItemPickup(); // PerformRaycast 통합됨
        HandleItemDrop();
        UpdateUI();
    }

    private void HandleQuickSlotSelection()
    {
        if (inventory.isTwoHandedEquipped)
        {
            Debug.Log("Cannot change quick slots while holding a two-handed item.");
            return;
        }

        for (int i = 0; i < quickSlots.Length; i++)
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
        int layerMask = ((-1) - (1 << LayerMask.NameToLayer("Player")) |
                         ((-1) - 1 << LayerMask.NameToLayer("ScrapSpawner")));  // Everything에서 Player, ScrapSpawner 레이어만 제외하고 충돌 체크함
        
        if (inventory.isTwoHandedEquipped)
        {
            Debug.Log("Cannot pick up items while holding a two-handed item.");
            return;
        }

        // 레이캐스트를 실행하여 아이템 픽업 로직을 처리
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        if (Physics.SphereCast(rayOrigin, SphereRadius, rayDirection, out hit, RayLength, layerMask))
        {
            Scrap scrap = hit.transform.GetComponent<Scrap>();
            if (scrap != null)
            {
                getItemMsg.SetActive(true);

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
        }
        else
        {
            getItemMsg.SetActive(false);
        }
    }

    private void HandleItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventory.DropItemFromQuickSlot(currentQuickSlot);
            _soundEmitter.PlayDropItem();
        }
    }

    private int FindSlotIndexForItem(ScrapData item)
    {
        for (int i = 0; i < inventory.scraps.Length; i++)
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

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (hit.transform != null)
        {
            DrawGizmos(Camera.main.transform.position, hit.point, hit.distance);
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
    #endif
}