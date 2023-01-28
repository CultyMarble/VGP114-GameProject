using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTile : MonoBehaviour
{
    private void OnMouseOver()
    {
        BuildingManager.Instance.defenseBuildingPosition = this.transform.position;
    }
}