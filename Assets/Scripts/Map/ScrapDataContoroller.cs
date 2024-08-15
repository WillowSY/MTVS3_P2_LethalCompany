using System.IO;
using System.Net;
using UnityEngine;


public class ScrapVectorData
{
    public float x;
    public float y;
    public float z;
}
public class ScrapDataContoroller : MonoBehaviour
{
    public GameObject scrabObject;

    public static bool _isScrapPosition = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_isScrapPosition)
        {
            LoadPosition();
        }
    }

    // Update is called once per frame
    public void SavePosition()
    {
        Vector3 position = scrabObject.transform.position;

        ScrapVectorData data = new ScrapVectorData()
        {
            x = position.x,
            y = position.y,
            z = position.z
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/ScrapVector.json", json);
        Debug.Log("scrap위치 데이터 저장" + json);
    }

    public void LoadPosition()
    {
        string path = Application.persistentDataPath + "/ScrapVector.json";
        string json = File.ReadAllText(path);
        ScrapVectorData data = JsonUtility.FromJson<ScrapVectorData>(json);
        Vector3 position = new Vector3(data.x, data.y, data.z);

        Instantiate(scrabObject, position, Quaternion.identity);

    }
}
