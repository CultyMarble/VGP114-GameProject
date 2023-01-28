using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : SingletonMonobehaviour<BuildingManager>
{
    [SerializeField] private SpriteRenderer buildPreview;

    [SerializeField] private SO_DefenseBuildingList defenseBuildingList;
    [SerializeField] private SO_ResourceBuildingList resourceBuildingList;

    [HideInInspector] public bool ResourceNodeAvailable = false;

    [HideInInspector] public Vector3 resourceNodePosition;
    [HideInInspector] public Vector3 defenseBuildingPosition;

    private Camera mainCamera;
    private SO_DefenseBuildingType selectedDefenseBuilding;

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

        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 enemySpawnPosition = CultyMarbleHelper.GetMouseToWorldPosition();
            Enemy.Spawn(enemySpawnPosition);
        }
    }
    
    //===========================================================================
    private void TryPlacingBuilding()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (!EventSystem.current.IsPointerOverGameObject())
        { // Check if mouse over UI
            Instantiate(selectedDefenseBuilding.prefab, defenseBuildingPosition, Quaternion.identity);
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