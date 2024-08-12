using System;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class ShipVectorData
{
    public float x;
    public float y;
    public float z;
}

public class ShipDataController : MonoBehaviour
{
    public GameObject onFieldObject;
    public static bool _isShipPosition = false; 

    
    private void Start()
    {
        if (_isShipPosition)
        {
            LoadPosition(); //CompanyEntranceController에 선언해둔 값으로 true로 변동시 해당 값을 받아옴
        }
    }

    public void SavePosition() //ship의 위치 데이터를 json으로 저장 하는 메소드
    {
        Vector3 position = onFieldObject.transform.position;

        ShipVectorData data = new ShipVectorData
        {
            x = position.x,
            y = position.y,
            z = position.z
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/ShipVector1.json", json);

        Debug.Log("배 위치 정보 저장" + json);
    }

    public void LoadPosition() // ship의 위치 데이터를 json으로 부터 반환받는 메소드
    {
        string path = Application.persistentDataPath + "/ShipVector1.json";

        string json = File.ReadAllText(path);

        ShipVectorData data = JsonUtility.FromJson<ShipVectorData>(json);

        Vector3 position = new Vector3(data.x, data.y, data.z);
        
        onFieldObject.transform.position = position;
        
        Debug.Log("배 위치 복원");
    }
}
