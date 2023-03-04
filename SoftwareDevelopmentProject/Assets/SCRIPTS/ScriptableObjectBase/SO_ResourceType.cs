using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ResourceType", fileName = "Resource Name")]
public class SO_ResourceType : ScriptableObject
{
    public string resourceName = string.Empty;
    public Sprite sprite;
}
