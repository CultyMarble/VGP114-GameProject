using UnityEngine;

[CreateAssetMenu(menuName = "SO/PowerUp", fileName = "New Power Up")]
public class SO_LevelPowerUp : ScriptableObject
{
    public string powerName;
    public string discription;

    public int attackPower;
    public int moveMovementSpeed;
    public float delayWaveTime;
}