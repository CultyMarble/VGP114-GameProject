using UnityEngine;

[CreateAssetMenu(menuName = "SO/LevelPowerUpList", fileName = "New Level Power Up List")]
public class SO_LevelPowerUpList : ScriptableObject
{
    public System.Collections.Generic.List<SO_LevelPowerUp> ListOfLevelPowerUp;
}