using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DefenseBuildingList", fileName = "New Defense Building List")]
public class SO_DefenseBuildingList : ScriptableObject
{
    public List<SO_DefenseBuildingType> ListOfDefenseBuilding;
}