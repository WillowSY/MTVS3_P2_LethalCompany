using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // 메뉴 UI 패널
    public Button resumeButton;
    public Button quitButton;

    private bool _isMenuActive = false; // 메뉴 활성화 상태

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(ExitGame);
        
        menuPanel.SetActive(false);
    }
    
    void Update()
    {
        // ESC 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isMenuActive = !_isMenuActive;
            menuPanel.SetActive(_isMenuActive);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 게임 일시 정지
            if (_isMenuActive)
            {
                Time.timeScale = 0f; // 게임 일시 정지
            }
        }
    }
    
    private void ResumeGame()
    {
        _isMenuActive = false;
        menuPanel.SetActive(false);
        Time.timeScale = 1f; // 게임 재개
        Debug.Log("Resume");
    }
    
    private void ExitGame()
    {
        Time.timeScale = 1f; // 게임이 재개된 상태로 돌아갑니다.
        Debug.Log("Exit");
        SceneManager.LoadScene("MainScreen");
    }
}

