using UnityEngine;

[CreateAssetMenu(fileName = "Runner_Level0", menuName = "ScriptableObjects/RunnerLevelDataSO", order = 1)]
public class RunnerLevelData : ScriptableObject
{
    [SerializeField] public int obstaclesToLevelUp;
    [SerializeField] public float minTimeBetweenObstacles;
    [SerializeField] public float maxTimeBetweenObstacles;
    [SerializeField] public float obstaclesSpeed;
    [SerializeField] public GameObject[] availableObstacleTypes;
}