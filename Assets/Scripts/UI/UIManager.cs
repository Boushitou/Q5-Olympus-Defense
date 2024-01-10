using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _constructionMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _constructionMenu.SetActive(false);
    }

    public void OpenConstructionMenu()
    {
        _constructionMenu.SetActive(true);
    }

    public void CloseConstructionMenu()
    {
        _constructionMenu.SetActive(false);
    }
}
