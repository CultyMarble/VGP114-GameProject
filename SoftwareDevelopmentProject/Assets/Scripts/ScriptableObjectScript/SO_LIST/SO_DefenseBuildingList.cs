using UnityEngine;

[CreateAssetMenu(menuName = "SO/DefenseBuildingList", fileName = "New Defense Building List")]
public class SO_DefenseBuildingList : ScriptableObject
{
    public System.Collections.Generic.List<SO_DefenseBuildingType> ListOfDefenseBuilding;
}