using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusButtons : MonoBehaviour
{
    public static MenusButtons Instance;

    [SerializeField] private GameObject _menuChooseLevel;
    [SerializeField] private GameObject _menuPause;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ButtonPlay()
    {
        _menuChooseLevel.SetActive(true);
    }

    public void ButtonCross()
    {
        _menuChooseLevel.SetActive(false);
    }

    public void ButtonMap1()
    {
        SceneManager.LoadScene("Map1");
    }

    public void ButtonMap2()
    {
        SceneManager.LoadScene("Map2");
    }

    public void ButtonMap3()
    {
        SceneManager.LoadScene("Map3");
    }

    public void ButtonMapEndless()
    {
        SceneManager.LoadScene("MapEndless");
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        _menuPause.SetActive(!_menuPause.activeSelf);
        GameManager.Instance.PauseGame();
    }

    public void ButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
