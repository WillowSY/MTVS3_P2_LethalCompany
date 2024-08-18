using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    private UIManager _uiManager;
    
    public float playerHp = 100f;
    public float sp = 100f;
    private float currentSp;
    public float spIncreaseSpeed = 10f; // sp 회복량 
    private float spRechargeTime = 2f; // sp 회복시간 
    private float currentSpRechargeTime;
    private bool spUsed; // sp 사용여부
    
    

    public bool onFatal;
    
    private void Start()
    {
        _uiManager = FindFirstObjectByType<UIManager>();
        currentSp = sp; // currentSp 초기화
    }

    private void Update()
    {
        PlayerHPUpdata();
        SPRechargeTime();
        SPRecover();
        _uiManager.spGauge.fillAmount = currentSp / sp;
    }

    public void TakeDamage(int damage)
    {
        playerHp -= damage;
        Debug.Log("데미지 입음");
    }

    private void PlayerHPUpdata()
    {
        if (playerHp <= 0)
        {
            _uiManager.UIDead();
            StartCoroutine(Dead());
            playerHp = 100f;
        }
        else if (playerHp <= 30 && !onFatal) 
        {
            _uiManager.UIHp30() ;
        }
        else if (playerHp <= 50 && !onFatal) 
        {
            _uiManager.UIHp50();
        }
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScreen");
    }
    
    public void DecreaseStamina(float amount)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - amount > 19.2f)
        {
            currentSp -= amount;
        }
        else
        {
            currentSp = 19.2f;
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


    
    

    /*private void Weight(string info)
    {
        info = sc
        WeightDataInfo(info);
    }*/
}
