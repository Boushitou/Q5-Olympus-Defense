using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Image _fill;

    public void UpdateLife(int maxLife, int life)
    { _fill.fillAmount = (float)life / maxLife; }
}
