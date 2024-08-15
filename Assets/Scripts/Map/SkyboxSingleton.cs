using UnityEngine;

using UnityEngine;

public class SkyboxSingleton : MonoBehaviour
{
    public static SkyboxSingleton instance = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

