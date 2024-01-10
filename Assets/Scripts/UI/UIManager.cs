using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _constructionMenu;
    [SerializeField] private Image _gateLifeImageSlidder;

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

    public void UpdateGateLife(int maxLife, int life)
    {
        _gateLifeImageSlidder.fillAmount = (float)life / maxLife;
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
