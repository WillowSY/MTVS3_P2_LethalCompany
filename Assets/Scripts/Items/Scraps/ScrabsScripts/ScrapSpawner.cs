using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrapSpawner : MonoBehaviour
{
    public BoxCollider boxCollider;
    public List<ScrapData> scrapsList;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        SpawnScraps();
    }

    private void SpawnScraps()
    {
        Vector3 randomPosition = GetRandomPositionInBoxCollider(boxCollider);
        int randomIndex = Random.Range(0, scrapsList.Count);
        ScrapData scrapData = scrapsList[randomIndex];
        Instantiate(scrapData.ScrapPrefab, randomPosition, Quaternion.identity); // 지정해 둔 randomPosition에 prefab을 생성
        WatchScrapsInfo(scrapData);
    }
    private void WatchScrapsInfo(ScrapData data)
    {
        Debug.Log("물건 이름 ::" + data.ScrapName);
        Debug.Log("물건 가격 ::" + data.ScrapPrice);
        Debug.Log("물건 무게 ::" + data.ScrapWeight);
    }

    Vector3 GetRandomPositionInBoxCollider(BoxCollider collider) 
    {
        Vector3 center = collider.center;
        Vector3 size = collider.size; // 콜라이더의 중심과 크기를 각각의 변수에 저장

        Vector3 randomPosition = new Vector3(
            Random.Range(center.x - size.x / 2, center.x + size.x / 2),
            Random.Range(center.y - size.y / 2, center.y + size.y / 2),
            Random.Range(center.z - size.z / 2, center.z + size.z / 2)
        );
        return collider.transform.TransformPoint(randomPosition); // randomPosition을 collider의 월드 좌표값으로 리턴
    }
}
