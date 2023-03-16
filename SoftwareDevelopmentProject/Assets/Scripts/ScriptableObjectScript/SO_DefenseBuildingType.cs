using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Defense Building", fileName = "New Defense Building")]
public class SO_DefenseBuildingType : ScriptableObject
{
    public string buildingName;
    public Transform prefab;
    public Sprite buildingSprite;
    public float buildingMaxHealth;
}
