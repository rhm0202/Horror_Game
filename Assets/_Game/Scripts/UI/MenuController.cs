using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("엔딩 씬 전용")]
    [SerializeField] GameObject clearScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TMP_Text timeText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (clearScreen == null || gameOverScreen == null) return;

        bool isGameOver = GameManager.LastState == GameManager.GameState.GameOver;
        clearScreen.SetActive(!isGameOver);
        gameOverScreen.SetActive(isGameOver);

        if (!isGameOver && timeText != null)
            timeText.text = GameManager.GetFormattedTime();
    }

    // 버튼 콜백
    public void OnStartGame()   => GameManager.Instance.StartGame();
    public void OnRestart()     => GameManager.Instance.StartGame();
    public void OnMainMenu()    => GameManager.Instance.GoToMainMenu();
    public void OnQuit()        => Application.Quit();
}
