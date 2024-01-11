using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _constructionMenu;
    [SerializeField] private Image _gateLifeImageSlidder;
    [SerializeField] private GameObject _killWavesStatut;

    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private TextMeshProUGUI _textGameOverMenu;
    [SerializeField] private TextMeshProUGUI _textGateLife;
    [SerializeField] private TextMeshProUGUI _textKills;

    private TextMeshProUGUI _wavesTxt;
    private TextMeshProUGUI _killTxt;

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

        _wavesTxt = _killWavesStatut.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _killTxt = _killWavesStatut.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateGateLife(int maxLife, int life)
    {
        _gateLifeImageSlidder.fillAmount = (float)life / maxLife;
    }

    public void UpdateWavesTxt(int currentWave, int totalWave)
    {
        _wavesTxt.text = "Waves : " + currentWave + " / " + totalWave;
    }

    public void UpdateKillTxt(int totalKilled)
    {
        _killTxt.text = "Kill : " + totalKilled;
    }

    public void OpenConstructionMenu()
    {
        _constructionMenu.SetActive(!_constructionMenu.activeSelf);
    }

    public void CloseConstructionMenu()
    {
        _constructionMenu.SetActive(false);
    }

    public void GameOver(bool win)
    {
        _gameOverMenu.SetActive(true);
        _textGameOverMenu.text = win ? "Victory" : "Defeat";
        _textGateLife.text += Gate.Instance.GetLife() + " / " + Gate.Instance.GetMaxLife();
    }
}
