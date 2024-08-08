using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private float _fadeDuration = 1f;
    private float timer;
    public CanvasGroup PD;
    public GameObject PDBG;

    void Update()
    {
        timer += Time.deltaTime;
        PD.alpha = timer / _fadeDuration;
        if (timer > _fadeDuration)
        {
            Starting();
        }
    }

    void Starting()
    {
        
        
    }
    
    IEnumerator 
}

