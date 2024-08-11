using UnityEngine;

public class WebTrigger : MonoBehaviour
{
    public Transform[] websPos;         // 거미줄 위치(local)
    public GameObject webPrefab;        // 설치될 거미줄 프리팹    // FIXME : 거미줄 모델 변경
    public bool isWebTriggerOn = false;
    private GameObject webInstance;
    
    /* 거미줄 초기 생성 */
    void Start()
    {
        foreach(Transform web in websPos)
        {
            webInstance =  Instantiate(webPrefab, web.position , web.rotation);
            webInstance.transform.SetParent(web);
        }
    }
    
    /* 거미줄 충돌 시 거미 순찰 패턴 활성화 */
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("WebTriggerOn");
            //anim.SetBool("isWebTriggerOn", true);
            isWebTriggerOn = true;
            //FIXME : 거미줄 하나만 삭제 되게 변경하기(현재 전체 삭제)
            Destroy(gameObject);
        }
    }
}