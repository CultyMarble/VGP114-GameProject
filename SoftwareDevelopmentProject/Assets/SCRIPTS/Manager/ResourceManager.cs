using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingletonMonobehaviour<ResourceManager>
{
    [SerializeField] SO_ResourceTypeList resourceTypeList;

    private Dictionary<SO_ResourceType, int> resourceAmountDictionary;

    public event EventHandler OnResourceAmountChanged;

    //===========================================================================
    protected override void Awake()
    {
        Singleton();

        resourceAmountDictionary = new Dictionary<SO_ResourceType, int>();

        foreach (SO_ResourceType resourceType in resourceTypeList.ListOfResourceType)
        {
            // Set amount of each currency type equals 0
            resourceAmountDictionary[resourceType] = 0;
        }
    }

    public void AddResource(SO_ResourceType currencyType, int amount)
    {
        resourceAmountDictionary[currencyType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(SO_ResourceType resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
}