using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Playing, GameOver, Clear }
    public GameState State { get; private set; }

    public static GameState LastState { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame() => SceneManager.LoadScene("Game");
    public void GoToMainMenu() => SceneManager.LoadScene("MainMenu");

    public void SetState(GameState state)
    {
        State = state;
        LastState = state;
        switch (state)
        {
            case GameState.GameOver:
            case GameState.Clear:
                SceneManager.LoadScene("Ending");
                break;
        }
    }
}
