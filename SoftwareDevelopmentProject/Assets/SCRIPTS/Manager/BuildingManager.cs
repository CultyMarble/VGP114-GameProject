using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : SingletonMonobehaviour<BuildingManager>
{
    public event EventHandler OnPlaceBuilding;

    [HideInInspector] public bool ResourceNodeAvailable = false;
    [HideInInspector] public Vector3 resourceNodePosition;
    [HideInInspector] public Vector3 defenseBuildingPosition;
    [HideInInspector] public int TowerCounter = 0;

    [SerializeField] private SpriteRenderer buildPreview;
    [SerializeField] private SO_DefenseBuildingList defenseBuildingList;
    [SerializeField] private SO_ResourceBuildingList resourceBuildingList;
    [SerializeField] private int maxinumTower;

    private Camera mainCamera;
    [HideInInspector] public SO_DefenseBuildingType selectedDefenseBuilding;

    //======================================================================
    protected override void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        mainCamera = Camera.main;

        HideBuildingPreview();
    }

    private void Update()
    {
        TryPlacingBuilding();
        UpdateBuidlingPreviewPosition();

        if (Input.GetMouseButtonDown(1) ||
            ResourceManager.Instance.GetResourceAmount(CurrencyType.Metal) < 100)
        {
            selectedDefenseBuilding = null;
            HideBuildingPreview();
        }
    }
    
    //===========================================================================
    private void TryPlacingBuilding()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (TowerCounter == maxinumTower)
            return;

        if (!EventSystem.current.IsPointerOverGameObject() && selectedDefenseBuilding != null)
        { // Check if mouse over UI
            ResourceManager.Instance.AddResource(CurrencyType.Metal, -100);

            Instantiate(selectedDefenseBuilding.prefab, defenseBuildingPosition, Quaternion.identity);
            TowerCounter++;

            OnPlaceBuilding?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetSelectedDefenseBuilding(SO_DefenseBuildingType defenseBuilding = null)
    {
        if (defenseBuilding == null)
        {
            HideBuildingPreview();
            return;
        }

        selectedDefenseBuilding = defenseBuilding;
        ShowBuildingPreview(selectedDefenseBuilding.buildingSprite);
    }

    private void ShowBuildingPreview(Sprite spriteToShow)
    {
        buildPreview.sprite = spriteToShow;
        buildPreview.gameObject.SetActive(true);
    }

    private void HideBuildingPreview()
    {
        buildPreview.gameObject.SetActive(false);
    }

    private void UpdateBuidlingPreviewPosition()
    {
        if (buildPreview.gameObject.activeInHierarchy == false)
            return;

        buildPreview.transform.position = defenseBuildingPosition;
    }
}