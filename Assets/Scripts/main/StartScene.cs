using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    private float _fadeDuration = 1f;
    private float timer;

    public Image PD;
    public GameObject PDBG;
    public GameObject mainCanvas;

    private void Start()
    {
        // Start 코루틴 실행
        StartCoroutine(Intro());
    }

    private void Update()
    {
        // 타이머 업데이트
        timer += Time.deltaTime;

        // 이미지의 투명도를 서서히 증가 (Fade In)
        if (timer <= _fadeDuration)
        {
            Color color = PD.color;
            color.a = Mathf.Clamp01(timer / _fadeDuration);
            PD.color = color;
        }
    }

    IEnumerator Intro()
    {
        // 이미지가 서서히 나타난 후 3초 대기
        yield return new WaitForSecondsRealtime(4f);

        // 타이머 초기화
        timer = 0f;

        // 서서히 사라지도록 투명도 조정 (Fade Out)
        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;

            Color color = PD.color;
            color.a = Mathf.Clamp01(1f - (timer / _fadeDuration));
            PD.color = color;

            yield return null;
        }

        // 완전히 투명하게 설정
        Color finalColor = PD.color;
        finalColor.a = 0f;
        PD.color = finalColor;

        // 이미지가 완전히 사라진 후 3초 대기
        yield return new WaitForSecondsRealtime(4f);

        // 배경 오브젝트 비활성화 및 메인 캔버스 활성화
        PDBG.SetActive(false);
        mainCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}

