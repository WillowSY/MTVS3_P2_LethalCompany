using UnityEngine;
public class PlayerRaycast : MonoBehaviour
{
    // 퀵슬롯들을 저장할 배열
    [SerializeField]
    private GameObject[] quickSlots;

    // 현재 장착된 슬롯들을 저장할 배열
    [SerializeField]
    private GameObject[] currentSlots;
    
    // 플레이어 인벤토리 참조
    public Inventory inventory;
    
    // 현재 선택된 퀵슬롯 인덱스
    public int currentQuickSlot = 0;
    
    // 레이캐스트 결과를 저장할 변수
    private RaycastHit hit;
    
    // 레이의 길이와 구의 반지름 상수
    private const float RayLength = 2.0f;
    private const float SphereRadius = 1f;
    
    // UI 요소들
    [SerializeField] private GameObject getItemMsg;
    [SerializeField] private GameObject twoHandedMsg;
    
    void Update()
    {
        // 인벤토리가 할당되지 않은 경우 함수 종료
        if (inventory == null) return;
        
        // 레이캐스트를 통해 아이템과의 충돌 감지
        PerformRaycast();

        // 퀵슬롯 선택 처리
        HandleQuickSlotSelection();
        
        // 아이템 픽업 처리
        HandleItemPickup();
        
        // 아이템 드롭 처리
        HandleItemDrop();
        
        // UI 업데이트
        UpdateUI();
    }

    // 레이캐스트를 통해 아이템과의 충돌 감지
    private void PerformRaycast()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        if (Physics.SphereCast(rayOrigin, SphereRadius, rayDirection, out hit, RayLength))
        {
            // 히트된 오브젝트에 Scrap 컴포넌트가 있는지 확인
            Scrap scrap = hit.transform.GetComponent<Scrap>();
            if (scrap != null)
            {
                getItemMsg.SetActive(true);
            }
        }
        else
        {
            getItemMsg.SetActive(false);
        }
    }
    
    // 에디터에서 레이와 히트 지점을 시각적으로 표시하기 위한 함수
    private void OnDrawGizmos()
    {
        // 레이의 시작점과 방향 설정
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        // 구 레이캐스트를 실행하여 충돌이 감지되면 Gizmo를 그림
        if (Physics.SphereCast(rayOrigin, SphereRadius, rayDirection, out hit, RayLength))
        {
            DrawGizmos(rayOrigin, hit.point, hit.distance);
        }
    }

    // Gizmo를 그리는 함수
    private void DrawGizmos(Vector3 origin, Vector3 hitPoint, float distance)
    {
        // 레이의 경로를 빨간색으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, Camera.main.transform.forward * distance);

        // 히트 지점까지의 경로를 따라 녹색의 와이어 구를 표시
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin + Camera.main.transform.forward * distance, SphereRadius);

        // 히트 지점에 파란색 구를 표시
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(hitPoint, SphereRadius * 0.25f);
    }

    // 퀵슬롯 선택을 처리하는 함수
    void HandleQuickSlotSelection()
    {
        // 두 손 아이템이 장착된 상태에서는 퀵슬롯을 변경할 수 없음
        if (inventory.isTwoHandedEquipped)
        {
            Debug.Log("Cannot change quick slots while holding a two-handed item.");
            return;
        }
        
        // 숫자 키(1~4)를 눌러 퀵슬롯을 선택
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

    // 아이템을 픽업하는 함수
    private void HandleItemPickup()
    {
        // 두 손 아이템이 장착된 상태에서는 아이템을 주울 수 없음
        if (inventory.isTwoHandedEquipped)
        {
            Debug.Log("Cannot pick up items while holding a two-handed item.");
            return;
        }
        
        // 'E' 키를 눌러 아이템을 픽업
        if (Input.GetKeyDown(KeyCode.E) && hit.transform != null)
        {
            // 히트된 오브젝트에서 Scrap 컴포넌트를 찾음
            Scrap scrap = hit.transform.GetComponent<Scrap>();
            if (scrap != null)
            {
                ScrapData scrapData = scrap.scrap;
                
                // ScrapData가 유효한 경우, 아이템을 퀵슬롯에 추가
                if (scrapData != null)
                {
                    inventory.AddItemToQuickSlot(currentQuickSlot, scrapData);
                    
                    // 아이템이 추가된 슬롯을 찾아 선택
                    currentQuickSlot = FindSlotIndexForItem(scrapData);
                    inventory.SelectQuickSlot(currentQuickSlot);
                    
                    // 히트된 오브젝트 삭제
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

    // 아이템을 드롭하는 함수
    void HandleItemDrop()
    {
        // 'G' 키를 눌러 현재 선택된 퀵슬롯의 아이템을 드롭
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventory.DropItemFromQuickSlot(currentQuickSlot);
        }
    }

    // 주어진 아이템이 있는 슬롯의 인덱스를 찾는 함수
    int FindSlotIndexForItem(ScrapData item)
    {
        // 인벤토리의 퀵슬롯에서 아이템을 찾아 해당 인덱스를 반환
        for (int i = 0; i < inventory.scraps.Length; i++)
        {
            if (inventory.scraps[i] == item)
                return i;
        }
        return 0;
    }
    
    // UI 업데이트 함수
    private void UpdateUI()
    {
        // 양손 아이템을 들고 있을 때 UI 업데이트
        if (inventory.isTwoHandedEquipped)
        {
            twoHandedMsg.SetActive(true);
        }
        else
        {
            twoHandedMsg.SetActive(false);
        }
    }
}