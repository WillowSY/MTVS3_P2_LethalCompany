using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scanText; // TextMeshProUGUI 컴포넌트

    private void Start()
    {
        scanText.text = null;
    }

    public IEnumerator DisplayInfo(string info)
    {
        scanText.text = info;
        yield return new WaitForSeconds(1f);
        scanText.text = null;
    }
    /*public void DisplayInfo(string info)
    {
        scanText.text = info;
    }*/
}