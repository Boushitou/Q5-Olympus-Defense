using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PauseGame()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void GameOver(bool win)
    {
        PauseGame();

        UIManager.Instance.GameOver(win);
        
        Debug.Log("game over");
    }
}
