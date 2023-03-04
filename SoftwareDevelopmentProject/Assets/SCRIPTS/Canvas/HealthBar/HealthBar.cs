using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem buildingHealth;
    [SerializeField] private Transform healthBar;

    //===========================================================================
    private void Start()
    {
        buildingHealth.OnHealthChanged += BuildingHealth_OnBuildingHealthChanged;

        UpdateHealthBarScale();
        UpdateHealthBarVisibility();
    }

    private void OnDestroy()
    {
        buildingHealth.OnHealthChanged -= BuildingHealth_OnBuildingHealthChanged;
    }

    //===========================================================================
    private void BuildingHealth_OnBuildingHealthChanged(object sender, HealthSystem.OnHealthChangedEvenArgs e)
    {
        UpdateHealthBarScale();
        UpdateHealthBarVisibility();
    }

    private void UpdateHealthBarScale()
    {
        float targetScale = buildingHealth.GetHealthPercentage() * 0.01f;
        healthBar.localScale = new Vector3(targetScale, 1.0f, 1.0f);
    }

    private void UpdateHealthBarVisibility()
    {
        if (Mathf.Approximately(buildingHealth.GetHealthPercentage(), 100.0f))
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
