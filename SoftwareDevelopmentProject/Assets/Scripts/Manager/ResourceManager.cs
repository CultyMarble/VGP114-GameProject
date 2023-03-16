using System;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Gas,
    Metal,
}

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

    public void AddResource(CurrencyType currencyType, int amount)
    {
        if (currencyType == CurrencyType.Gas)
        {
            resourceAmountDictionary[resourceTypeList.ListOfResourceType[0]] += amount;
        }
        else if (currencyType == CurrencyType.Metal)
        {
            resourceAmountDictionary[resourceTypeList.ListOfResourceType[1]] += amount;
        }

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(CurrencyType currencyType)
    {
        if (currencyType == CurrencyType.Gas)
        {
            return resourceAmountDictionary[resourceTypeList.ListOfResourceType[0]];
        }
        else if (currencyType == CurrencyType.Metal)
        {
            return resourceAmountDictionary[resourceTypeList.ListOfResourceType[1]];
        }

        return 0;
    }
}