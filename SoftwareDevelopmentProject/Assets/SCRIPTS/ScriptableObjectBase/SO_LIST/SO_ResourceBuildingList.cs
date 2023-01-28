using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ResourceBuildingList", fileName = "New Resource Building List")]
public class SO_ResourceBuildingList : ScriptableObject
{
    public List<SO_ResourceBuildingType> ListOfResourceBuilding;
}
