using UnityEngine;
using UnityEngine.UI;

public class ConstructionButton : MonoBehaviour
{
    [SerializeField] Tower _tower;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (_tower != null)
        {
            _button.interactable = _tower.GetCost() <= ConstructionManager.Instance.GetFaith();
        }
    }
}
