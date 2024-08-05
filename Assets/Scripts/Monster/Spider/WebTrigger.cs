using UnityEngine;

public class WebTrigger : MonoBehaviour
{
    public Transform[] websPos;         // 거미줄 위치(local)
    public GameObject webPrefab;        // 설치될 거미줄 프리팹    
    public MonsterPattern monster;      // Spider 스크립트 참조
    
    private GameObject webInstance;
    
    /*
     * 거미줄 초기 생성
     */
    void Start()
    {
        foreach(Transform web in websPos)
        {
            webInstance =  Instantiate(webPrefab, web.position , web.rotation);
            webInstance.transform.SetParent(web);
        }
    }
    
    /*
     * 거미줄 충돌 시 거미 순찰 패턴 활성화
     */
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monster.curPattern.ExitPattern();
            monster.curPattern = monster.patterns[(int)PatternNumber.Pattern.Patrol];
            monster.curPattern.EnterPattern(Vector3.zero);
            Destroy(gameObject);
            Debug.Log("WebTriggerOn");
        }
    }
}
