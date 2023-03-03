using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler<OnHealthChangedEvenArgs> OnHealthChanged;
    public event EventHandler OnDestroy;
    public class OnHealthChangedEvenArgs
    {
        public float currentHealth;
    }

    [SerializeField] private float maxHealth;
    private float currentHealth;
    private float healthPercentage;

    //======================================================================
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ReduceCurrentHealth(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Call Event
        OnHealthChanged?.Invoke(this, new OnHealthChangedEvenArgs { currentHealth = currentHealth});

        if (currentHealth <= 0)
        {
            SoundEffectManager.Instance.PlaySound(SoundEffectManager.EnumSound.BuildingDestroyed);
            OnDestroy?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth * 100;
    }

    public void SetMaxHealth(float newMaxHealthAmount, bool updateCurrentHealth = false)
    {
        maxHealth = newMaxHealthAmount;

        if (updateCurrentHealth)
            currentHealth = maxHealth;
    }
}