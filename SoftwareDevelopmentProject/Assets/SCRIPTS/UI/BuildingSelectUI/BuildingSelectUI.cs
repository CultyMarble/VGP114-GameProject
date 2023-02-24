using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectUI : MonoBehaviour
{
    [SerializeField] private SO_DefenseBuildingList defenseBuildingList;
    [SerializeField] private Transform buttonTemplate;

    [Header("Button offsets:")]
    [SerializeField] private Vector2 offSetPosition;
    [SerializeField] private float offsetEachNode = -100.0f;

    private Dictionary<SO_DefenseBuildingType, Transform> resourceTypeTransformDictionary;

    //======================================================================

    private void Awake()
    {
        // Set resourceTemplate in hierachy to inactive
        buttonTemplate.gameObject.SetActive(false);
        resourceTypeTransformDictionary = new Dictionary<SO_DefenseBuildingType, Transform>();
    }

    private void Start()
    {
        CreateButtonUI();
    }

    //===========================================================================

    private void CreateButtonUI()
    {
        int _index = 0;
        foreach (SO_DefenseBuildingType defenseBuilding in defenseBuildingList.ListOfDefenseBuilding)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);

            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offSetPosition.x + (offsetEachNode * _index), offSetPosition.y);
            buttonTransform.Find("image_icon").GetComponent<Image>().sprite = defenseBuilding.buildingSprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetSelectedDefenseBuilding(defenseBuilding);
            });

            buttonTransform.gameObject.SetActive(true);

            resourceTypeTransformDictionary[defenseBuilding] = buttonTransform;
            _index++;
        }
    }
}
