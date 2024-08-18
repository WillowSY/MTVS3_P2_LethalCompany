using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text text_Time;
    public TMP_Text text_DailyCycle;
    public CanvasGroup canvasGroup;
    public SkyboxController skyboxController;

    private int inGameTime_Minutes;
    private int timer = 360;
    private int inGameTime_StartHour = 08;
    private int inGameTime_Hours;
    private float inGameTime;
    
    public Image imageComponent;
    public Sprite morning;
    public Sprite afternoon;
    public Sprite dinner;
    public Sprite night;

    private string newColor_Code;
    private Color color = new Color32(255,135,0,255);

    private void Start()
    {
        inGameTime = inGameTime_StartHour * 3600f;
        StartCoroutine(CanvasGroupStart());
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    private void Update()
    {
        inGameTime += Time.deltaTime * timer;

        inGameTime_Hours = (int)(inGameTime / 3600f);
        inGameTime_Minutes = (int)((inGameTime % 3600) / 60f);

        text_Time.color = color;
        text_Time.text = $"{inGameTime_Hours:D2}:{inGameTime_Minutes:D2}";
        skyboxController.StartBlending();
        ChangeSprite(morning);
        if (inGameTime_Hours >= 12)
        {
            text_DailyCycle.color = color;
            text_DailyCycle.text = "오후";
            ChangeSprite(afternoon);
                
            if (inGameTime_Hours >= 18)
            {
                text_DailyCycle.text = "저녁";
                ChangeSprite(dinner);
                if (inGameTime_Hours >= 22)
                {
                    text_DailyCycle.text = "밤";
                    ChangeSprite(night);
                    if (inGameTime_Hours >= 24)
                    {
                        inGameTime_Hours = 0;
                        inGameTime_Minutes = 0;
                    }
                }
            }
        } 
    }
    
    private void ChangeSprite(Sprite newSprite)
    {
        imageComponent.sprite = newSprite;
    }

    IEnumerator CanvasGroupStart()
    {
        yield return new WaitForSeconds(8);
        canvasGroup.alpha = 1;
    }
}
