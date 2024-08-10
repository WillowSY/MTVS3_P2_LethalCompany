using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanySceneManager : MonoBehaviour
{
    public static CompanySceneManager instance = null;

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
