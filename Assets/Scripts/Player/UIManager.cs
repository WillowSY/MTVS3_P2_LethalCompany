using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scanText; // TextMeshProUGUI 컴포넌트
    public TextMeshProUGUI weightText;
    private void Start()
    {
        scanText.text = null;
        scanText.text = null;
    }

    public IEnumerator ScanDisplayInfo(string info)
    {
        scanText.text = info;
        yield return new WaitForSeconds(1f);
        scanText.text = null;
    }

    public void WeightDisplayInfo(string info)
    {
        weightText.text = info;
    }
    /*public void DisplayInfo(string info)
    {
        scanText.text = info;
    }*/
}