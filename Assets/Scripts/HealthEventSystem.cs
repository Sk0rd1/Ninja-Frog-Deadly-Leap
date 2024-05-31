using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEventSystem : MonoBehaviour
{
    public static HealthEventSystem instance;

    public event Action<int> OnDamage;
    public event Action<int> OnHeal;
    public event Action<int> OnCoin;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDamage(int damageAmount)
    {
        OnDamage?.Invoke(damageAmount);
    }

    public void TriggerHeal(int healAmount)
    {
        OnHeal?.Invoke(healAmount);
    }

    public void TriggerCoin(int coinAmount)
    {
        OnCoin?.Invoke(coinAmount);
    }
}
