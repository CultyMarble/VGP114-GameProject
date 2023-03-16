using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private SO_ResourceTypeList resourceTypeList;
    [SerializeField] private Transform resourceTemplate;
    [SerializeField] private float offsetPosition = -32.0f;
    [SerializeField] private float offsetEachNode = -64.0f;

    private Dictionary<SO_ResourceType, Transform> resourceTypeTransformDictionary;


    //======================================================================
    private void Awake()
    {
        // Set resourceTemplate in hierachy to inactive
        resourceTemplate.gameObject.SetActive(false);
        resourceTypeTransformDictionary = new Dictionary<SO_ResourceType, Transform>();
    }

    private void Start()
    {
        // Sub to OnResourceAmountChanged event
        ResourceManager.Instance.OnResourceAmountChanged += ResourceUI_ResourceAmountChangedHandler; ;

        CreateResourceUI();
        UpdateResourceAmount();
    }

    //===========================================================================
    private void CreateResourceUI()
    {
        int _index = 0;
        foreach (SO_ResourceType resourceType in resourceTypeList.ListOfResourceType)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);

            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetPosition + (offsetEachNode * _index), offsetPosition);
            resourceTransform.Find("image_icon").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTransform.gameObject.SetActive(true);

            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            _index++;
        }
    }

    private void UpdateResourceAmount()
    {
        foreach (SO_ResourceType resourceType in resourceTypeList.ListOfResourceType)
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];

            if (resourceType.resourceName == "Vewron")
            {
                resourceTransform.Find("text_amount").GetComponent<TextMeshProUGUI>().
                    SetText(ResourceManager.Instance.GetResourceAmount(CurrencyType.Gas).ToString());
            }
            else if (resourceType.resourceName == "Gear")
            {
                resourceTransform.Find("text_amount").GetComponent<TextMeshProUGUI>().
                    SetText(ResourceManager.Instance.GetResourceAmount(CurrencyType.Metal).ToString());
            }

        }
    }

    private void ResourceUI_ResourceAmountChangedHandler(object sender, EventArgs e)
    {
        UpdateResourceAmount();
    }
}
