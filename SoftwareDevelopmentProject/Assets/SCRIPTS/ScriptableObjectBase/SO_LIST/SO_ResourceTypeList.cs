using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ResourceTypeList", fileName = "New List")]
public class SO_ResourceTypeList : ScriptableObject
{
    public List<SO_ResourceType> ListOfResourceType;
}