using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public float playerHp = 100f;
    public GameObject hp50;
    public GameObject hp30;
    public TMP_Text fatalInjury;
    public TMP_Text death;
    public Image blackout;

    private UIManager _uiManager;

    private bool onfatal = false;

    public float sp = 100f;
    private float currentSp;
    public float spIncreaseSpeed = 10f; // sp 회복량
    private float spRechargeTime = 2f; // sp 회복시간 
    private float currentSpRechargeTime;
    private bool spUsed; // sp 사용여부
    
    public Image spGauge;
    private void Start()
    {
        _uiManager = FindFirstObjectByType<UIManager>();
        currentSp = sp; // currentSp 초기화
    }

    private void Update()
    {
        SPRechargeTime();
        SPRecover();
        UpdateHpUI();

        spGauge.fillAmount = currentSp / sp;
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


    public void TakeDamage(int damage)
    {
        playerHp -= damage;
    }
    
    private void UpdateHpUI()
    {
        //Debug.Log("playerHP :"+playerHp);
        if (playerHp <= 30 && !onfatal)
        {
            hp50.SetActive(false);
            hp30.SetActive(true);
            fatalInjury.enabled = true;
            Invoke("HideFatal",1f);
        }
        else if (playerHp <= 50 && !onfatal)
        {
            hp50.SetActive(true);
        }
        else if (playerHp <= 0)
        {
            StartCoroutine(Fadein());
            death.enabled = true;
            
            
            Invoke("LoadScene",2f);
        }
    }

    private void LoadScene()
    {
        Debug.Log("LoadScene");
        playerHp = 100f;
        SceneManager.LoadScene(1);
    }

    private void HideFatal()
    {
        fatalInjury.enabled = false;
        onfatal = true;
    }

    IEnumerator Fadein()
    {
        Color fadeColor = blackout.color;
        for (int i = 0; i < 100; i++)
        {
            fadeColor.a += 0.02f;
            blackout.color = fadeColor;
            yield return new WaitForSeconds(0.01f);
        }
    }

    /*private void Weight(string info)
    {
        info = sc
        WeightDataInfo(info);
    }*/
}
