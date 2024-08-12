using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange = 100f; // 스캔 범위
    public string[] detectableTags = { "Item", "Enemy" }; // 감지 가능한 태그 목록
    public string weaponTag = "Weapon"; // 무기 태그

    private UIManager _uiManager;

    void Start()
    {
        // UIManager 스크립트 참조 가져오기
        _uiManager = Object.FindFirstObjectByType<UIManager>();
    }

    void Update()
    {
        // 우클릭 시 주변 스캔
        if (Input.GetMouseButtonDown(1))
        {
            ScanSurroundings();
        }
    }

    // 주변 스캔
    private void ScanSurroundings()
    {
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, scanRange);
        
        GameObject closestDetectableObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null && IsDetectableTag(hit.collider.tag) && hit.collider.tag != weaponTag)
            {
                float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDetectableObject = hit.collider.gameObject;
                }
            }
        }

        if (closestDetectableObject != null)
        {
            DisplayInfo(closestDetectableObject);
        }
        else
        {
            Debug.Log("No detectable objects found");
        }
    }

    // 감지 가능한 태그인지 확인
    private bool IsDetectableTag(string tag)
    {
        foreach (string detectableTag in detectableTags)
        {
            if (tag == detectableTag)
            {
                return true;
            }
        }
        return false;
    }

    // 정보 표시
    private void DisplayInfo(GameObject scannedObject)
    {
        string info = "";
        if (scannedObject.CompareTag("Item"))
        {
            info = "아이템 발견: <br>" + scannedObject.name;
        }
        else if (scannedObject.CompareTag("Enemy"))
        {
            info = "적 발견: <br>" + scannedObject.name;
        }
        _uiManager.DisplayInfo(info);
    }
}
