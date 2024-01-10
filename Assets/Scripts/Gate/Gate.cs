using UnityEngine;

public class Gate : MonoBehaviour
{
    public static Gate Instance;

    [SerializeField] private int _life = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void TakeDamage(int damage)
    {
        _life -= damage;

        if (_life <= 0)
            Death();
    }

    private void Death()
    {
        GameManager.Instance.GameOver();
    }

    public int GetLife()
    { return _life; }
}
