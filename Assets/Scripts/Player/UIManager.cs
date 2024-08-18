using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private StatusController _statusController;
    
    public TextMeshProUGUI scanText; // TextMeshProUGUI 컴포넌트
    
    public GameObject hp50;
    public GameObject hp30;
    public Image spGauge;
    
    public TMP_Text fatalInjury;
    public TMP_Text death;
    public Image blackout;
    
    private void Start()
    {
        _statusController = Object.FindFirstObjectByType<StatusController>();
        scanText.text = null;
    }

    public IEnumerator ScanDisplayInfo(string info)
    {
        scanText.text = info;
        yield return new WaitForSeconds(1f);
        scanText.text = null;
    }

    public void UIHp50()
    {
        hp50.SetActive(true);
    }

    public void UIHp30()
    {
        hp50.SetActive(false);
        hp30.SetActive(true);
        fatalInjury.enabled = true;
        StartCoroutine(HideFatal());
    }

    private IEnumerator HideFatal()
    {
        yield return new WaitForSeconds(1.5f);
        fatalInjury.enabled = false;
        _statusController.onFatal = true;
    }
    
    public void UIDead()
    {
        StartCoroutine(Fadein());
        death.enabled = true;
    }
    
    private IEnumerator Fadein()
    {
        Color fadeColor = blackout.color;
        for (int i = 0; i < 100; i++)
        {
            fadeColor.a += 0.02f;
            blackout.color = fadeColor;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        _statusController.playerHp = 100f;
    }
}