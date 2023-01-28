using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : SingletonMonobehaviour<PlayerHealth>
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private float healthPercentage;

    public event EventHandler<OnPlayerHealthChangedEventArgs> OnPlayerHealthChanged;
    public class OnPlayerHealthChangedEventArgs : EventArgs { public float healthPercentageEvent; }

    //======================================================================
    protected override void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthPercentage();
    }

    //======================================================================
    public void DecreaseCurrentHealth(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthPercentage();

        // Call Event
        OnPlayerHealthChanged?.Invoke(this, new OnPlayerHealthChangedEventArgs { healthPercentageEvent = healthPercentage });
    }

    public void IncreaseCurrentHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthPercentage();

        // Call Event
        OnPlayerHealthChanged?.Invoke(this, new OnPlayerHealthChangedEventArgs { healthPercentageEvent = healthPercentage });
    }

    private void UpdateHealthPercentage()
    {
        healthPercentage = (currentHealth / maxHealth) * 100.0f;
    }
}
