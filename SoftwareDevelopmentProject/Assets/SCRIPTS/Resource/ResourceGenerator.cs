using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private SO_ResourceBuildingType resourceBuilding;
    private float timerMax;
    private float timeCounter;

    private void Awake()
    {
        resourceBuilding = GetComponent<ResourceBuildingPref>().buildingPreference;
        timerMax = resourceBuilding.resourceGeneratorData.timerMax;
    }

    private void Update()
    {
        timeCounter -= Time.deltaTime;
        if (timeCounter <= 0)
        {
            timeCounter += timerMax;
            ResourceManager.Instance.AddResource(resourceBuilding.resourceGeneratorData.currencyType, 1);
        }
    }
}
