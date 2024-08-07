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
    public GameObject shipPrefab;
    public Vector3 rotationOffset = new Vector3(0, 180, 0);
    public static bool _isShipPosition = false;

    
    private void Start()
    {
        if (_isShipPosition)
        {
            LoadPosition();
        }
    }

    public void SavePosition()
    {
        Vector3 position = onFieldObject.transform.position;

        ShipVectorData data = new ShipVectorData
        {
            x = position.x,
            y = position.y,
            z = position.z
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/ShipVector.json", json);

        Debug.Log("배 위치 정보 저장" + json);
    }

    public void LoadPosition()
    {
        string path = Application.persistentDataPath + "/ShipVector.json";

        string json = File.ReadAllText(path);

        ShipVectorData data = JsonUtility.FromJson<ShipVectorData>(json);

        Vector3 position = new Vector3(data.x, data.y, data.z);


        //onFieldObject = Instantiate(shipPrefab, position, Quaternion.identity);
        onFieldObject.transform.position = position;
        //onFieldObject.transform.Rotate(rotationOffset);
        Debug.Log("배 위치 복원");
    }
}
