using UnityEngine;

[CreateAssetMenu(fileName = "New Level Info", menuName = "Level Info")]
public class LevelInfo : ScriptableObject
{
    public int levelNumber;
    public int packages;
    public int customers;
    public int boosts;
    public Vector3 carSpawnPosition;
    public Quaternion carSpawnRotation;
    public Vector3[] packageSpawnPositions;
    public Vector3[] customerSpawnPositions;
    public Vector3[] boostSpawnPositions;
}