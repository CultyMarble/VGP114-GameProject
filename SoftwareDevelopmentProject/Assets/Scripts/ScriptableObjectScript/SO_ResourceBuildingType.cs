using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Resource Building", fileName = "New Resource Building")]
public class SO_ResourceBuildingType : ScriptableObject
{
    public string buildingName;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
}
