using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SO_DefenseBuildingType defenseBuildingType;
    private HealthSystem buildingHealth;

    //======================================================================
    private void Awake()
    {
        buildingHealth = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        buildingHealth.OnHealthChanged += Building_OnBuildingHealthChangedHandler;
        buildingHealth.OnDestroy += Building_OnBuildingDestroyHandler;

        buildingHealth.SetMaxHealth(defenseBuildingType.buildingMaxHealth, true);
    }

    private void OnDestroy()
    {
        buildingHealth.OnHealthChanged -= Building_OnBuildingHealthChangedHandler;
        buildingHealth.OnDestroy -= Building_OnBuildingDestroyHandler;
    }

    //======================================================================
    private void Building_OnBuildingHealthChangedHandler(object sender, HealthSystem.OnHealthChangedEvenArgs e)
    {
        return;
    }

    private void Building_OnBuildingDestroyHandler(object sender, System.EventArgs e)
    {
        Destroy(this.gameObject);
    }
}
