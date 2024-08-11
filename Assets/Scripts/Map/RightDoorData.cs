using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;



public class R_DoorVectorData
{
    public float x;
    public float y;
    public float z;
}

public class RightDoorData : MonoBehaviour
{

    public GameObject onFieldObject;
    public static bool _isR_DoorPosition = false; 

    
    private void Start()
    {
        if (_isR_DoorPosition)
        {
            LoadPosition(); //CompanyEntranceController에 선언해둔 값으로 true로 변동시 해당 값을 받아옴
        }
    }

    public void SavePosition() //ship의 위치 데이터를 json으로 저장 하는 메소드
    {
        Vector3 position = onFieldObject.transform.localPosition;

        R_DoorVectorData data = new R_DoorVectorData
        {
            x = position.x,
            y = position.y,
            z = position.z
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/R_DoorVector.json", json);

        Debug.Log("오른문 위치 정보 저장" + json);
    }

    public void LoadPosition() // ship의 위치 데이터를 json으로 부터 반환받는 메소드
    {
        string path = Application.persistentDataPath + "/R_DoorVector.json";

        string json = File.ReadAllText(path);

        R_DoorVectorData data = JsonUtility.FromJson<R_DoorVectorData>(json);

        Vector3 position = new Vector3(data.x, data.y, data.z);
        
        onFieldObject.transform.localPosition = position;
        
        Debug.Log("오른문 위치 복원");
    }
}

