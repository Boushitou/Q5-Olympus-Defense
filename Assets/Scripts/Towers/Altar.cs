using System.Collections;
using UnityEngine;

public class Altar : Tower
{
    private int _faith = 10;
    private int _waitTime = 10;

    private void Start()
    {
        WaitGainFaith();
    }

    private void WaitGainFaith()
    {
        StartCoroutine(GainFaith());
    }

    private IEnumerator GainFaith()
    {
        yield return new WaitForSeconds(_waitTime);
        ConstructionManager.Instance.AddFaith(_faith);

        WaitGainFaith();
    }

    public override void Attack(Transform enemy)
    {
        
    }

    public override void InitializeValue()
    {
        _maxHealth = 50;
        _cost = 50;
    }
}
