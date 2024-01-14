using UnityEngine;
using UnityEngine.UI;

public class ConstructionButton : MonoBehaviour
{
    [SerializeField] private int _cost = 1;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        UpdateInteractable();
    }

    public void UpdateInteractable()
    {
        _button.interactable = _cost <= ConstructionManager.Instance.GetFaith();
    }
}
