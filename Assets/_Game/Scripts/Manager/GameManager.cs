using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Playing, GameOver, Clear }
    public GameState State { get; private set; }

    public static GameState LastState { get; private set; }
    public static float EscapeTime { get; private set; }

    float startTime;
    bool timing = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        EscapeTime = 0f;
        startTime = Time.realtimeSinceStartup;
        timing = true;
    }

    void Update()
    {
        if (timing)
            EscapeTime = Time.realtimeSinceStartup - startTime;
    }

    public static string GetFormattedTime()
    {
        int minutes = (int)(EscapeTime / 60);
        int seconds = (int)(EscapeTime % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    public void StartGame() => SceneManager.LoadScene("Game");
    public void GoToMainMenu() => SceneManager.LoadScene("MainMenu");

    public void SetState(GameState state)
    {
        State = state;
        LastState = state;
        timing = false;
        switch (state)
        {
            case GameState.GameOver:
            case GameState.Clear:
                SceneManager.LoadScene("Ending");
                break;
        }
    }
}
