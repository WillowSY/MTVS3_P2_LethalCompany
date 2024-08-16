using TMPro;
using UnityEngine;

public class SalaryCanvasController : MonoBehaviour
{
    public TMP_Text mainText;
    public TMP_Text totalText;

    private Color color = new Color32(220, 45, 45, 255);

    private string newColor_code;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mainText.color = color;
        mainText.text = "급여를 받았습니다!";

        totalText.color = color;
        totalText.text = "총합 : 165$";
    }
}
