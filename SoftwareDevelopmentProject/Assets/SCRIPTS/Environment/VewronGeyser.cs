using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VewronGeyser : MonoBehaviour
{
    public bool isAvailable = true;

    //===========================================================================
    private void OnMouseOver()
    {
        if (isAvailable)
        {
            BuildingManager.Instance.ResourceNodeAvailable = true;
            BuildingManager.Instance.resourceNodePosition = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
