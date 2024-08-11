using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public MonsterEnums.MonsterType monsterType;
    public GameObject monsterPrefab;
    public Transform[] monsterTrans;
    private void Start()
    {
        switch (monsterType)
        {
            case MonsterEnums.MonsterType.Spider:
                SpawnSpider();
                break;
            case MonsterEnums.MonsterType.Dog:
                // FIXME : 시간 조건 추가
                SpawnDog();
                break;
            default:
                Debug.LogError("MonsterSpawner : 잘못된 몬스터 타입 입력");
                break;
        }
    }

    private void SpawnSpider()
    {
        foreach (Transform trans in monsterTrans)
        {
            Instantiate(monsterPrefab, trans.position, trans.rotation);
        }
    }

    private void SpawnDog()
    {
        foreach (Transform trans in monsterTrans)
        {
            Instantiate(monsterPrefab, trans.position, trans.rotation);
        }
    }
}
