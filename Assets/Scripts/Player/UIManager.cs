using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI infoText; // TextMeshProUGUI 컴포넌트

    public void DisplayInfo(string info)
    {
        infoText.text = info;
    }
}