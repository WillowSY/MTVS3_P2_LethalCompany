using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public float playerHp = 100f;
    public GameObject hp50;
    public GameObject hp30;

    public float sp = 100f;
    private float currentSp;
    public float spIncreaseSpeed = 10f; // sp 회복량
    private float spRechargeTime = 2f; // sp 회복시간 
    private float currentSpRechargeTime;
    private bool spUsed; // sp 사용여부
    
    public Image spGauge;
    private void Start()
    {
        currentSp = sp; // currentSp 초기화
    }

    private void Update()
    {
        SPRechargeTime();
        SPRecover();
        UpdateHp();

        spGauge.fillAmount = currentSp / sp;
    }

    public void DecreaseStamina(float amount)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - amount > 0)
        {
            currentSp -= amount;
        }
        else
        {
            currentSp = 0;
        }
    }

    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
            {
                currentSpRechargeTime += Time.deltaTime;
            }
            else
            {
                spUsed = false;
            }
        }
    }

    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed * Time.deltaTime;
        }
    }

    public float GetCurrentSP()
    {
        return currentSp;
    }


    public void TakeDamage(int damage)
    {
        playerHp -= damage;
    }
    private void UpdateHp()
    {
        //Debug.Log("playerHP :"+playerHp);
        if (playerHp < 51)
        {
            hp50.SetActive(true);
            if (playerHp < 31)
            {
                hp50.SetActive(false);
                hp30.SetActive(true);
                if (playerHp <= 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}
