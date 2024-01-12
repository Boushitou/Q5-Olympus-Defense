using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static Gate Instance;

    [SerializeField] private int _maxLife = 0;
    private int _life = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _life = _maxLife;
    }

    public void TakeDamage(int damage)
    {
        _life -= damage;
        _life = _life < 0 ? 0 : _life;

        UIManager.Instance.UpdateGateLife(_maxLife, _life);

        if (_life <= 0)
            Death();
    }

    private void Death()
    {
        GameManager.Instance.GameOver(false);
    }

    public int GetLife()
    { return _life; }

    public int GetMaxLife()
    { return _maxLife; }
}
