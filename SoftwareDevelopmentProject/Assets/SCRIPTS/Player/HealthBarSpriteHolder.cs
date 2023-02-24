using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSpriteHolder : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    private SpriteRenderer healthBar_sr;

    //===========================================================================
    private void Awake()
    {
        healthBar_sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerHealth.Instance.OnPlayerHealthChanged += HealthBarSpriteHolder_OnPlayerHealthChangedHandler;
    }

    private void OnDestroy()
    {
        PlayerHealth.Instance.OnPlayerHealthChanged -= HealthBarSpriteHolder_OnPlayerHealthChangedHandler;
    }

    //===========================================================================
    private void HealthBarSpriteHolder_OnPlayerHealthChangedHandler(object sender, PlayerHealth.OnPlayerHealthChangedEventArgs e)
    {
        float _healthPercentage = e.healthPercentageEvent;

        if (_healthPercentage <= 0.0f)
            UpdateHealthSprite(0);
        else if (_healthPercentage <= 10.0f)
            UpdateHealthSprite(1);
        else if (_healthPercentage <= 20.0f)
            UpdateHealthSprite(2);
        else if (_healthPercentage <= 30.0f)
            UpdateHealthSprite(3);
        else if (_healthPercentage <= 40.0f)
            UpdateHealthSprite(4);
        else if (_healthPercentage <= 50.0f)
            UpdateHealthSprite(5);
        else if (_healthPercentage <= 60.0f)
            UpdateHealthSprite(6);
        else if (_healthPercentage <= 70.0f)
            UpdateHealthSprite(7);
        else if (_healthPercentage <= 80.0f)
            UpdateHealthSprite(8);
        else if (_healthPercentage <= 90.0f)
            UpdateHealthSprite(9);
        else if (_healthPercentage <= 100.0f)
            UpdateHealthSprite(10);
    }

    private void UpdateHealthSprite(int spriteIndex)
    {
        healthBar_sr.sprite = sprites[spriteIndex];
    }
}
