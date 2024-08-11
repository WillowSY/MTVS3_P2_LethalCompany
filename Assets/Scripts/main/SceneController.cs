using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Button startGameButton;
    public Button quitGameButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        //quitGameButton.onClick.AddListener(QuitGame);
    }

    // 게임 씬 전환 함수
    private void StartGame()
    {
        SceneManager.LoadScene("StartField"); // "GameScene"을 실제 게임 씬 이름으로 교체
    }

    // 게임 종료 함수
    /*
    private void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }*/
}
