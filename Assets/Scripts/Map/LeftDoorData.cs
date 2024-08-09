using System.IO;
using UnityEngine;

public class L_DoorVectorData
{
    public float x;
    public float y;
    public float z;
}

public class LeftDoorData : MonoBehaviour
{

    public GameObject onFieldObject;
    public static bool _isL_DoorPosition = false; 

    
    private void Start()
    {
        if (_isL_DoorPosition)
        {
            LoadPosition(); //CompanyEntranceController에 선언해둔 값으로 true로 변동시 해당 값을 받아옴
        }
    }

    public void SavePosition() //ship의 위치 데이터를 json으로 저장 하는 메소드
    {
        Vector3 position = onFieldObject.transform.localPosition;

        L_DoorVectorData data = new L_DoorVectorData
        {
            x = position.x,
            y = position.y,
            z = position.z
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/L_DoorVector.json", json);

        Debug.Log("왼쪽문 위치 정보 저장" + json);
    }

    public void LoadPosition() // ship의 위치 데이터를 json으로 부터 반환받는 메소드
    {
        string path = Application.persistentDataPath + "/L_DoorVector.json";

        string json = File.ReadAllText(path);

        L_DoorVectorData data = JsonUtility.FromJson<L_DoorVectorData>(json);

        Vector3 position = new Vector3(data.x, data.y, data.z);
        
        onFieldObject.transform.localPosition = position;
        
        Debug.Log("왼쪽문 위치 복원");
    }
}
