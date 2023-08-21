using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [SerializeField, Range(30, 50)] public int MaxPointsCount = 40;
    [SerializeField, Range(0.01f, 0.03f)] public float TrajectorySpeed = 0.02f;
    [SerializeField, Range(0.02f, 0.08f)] public float VerticalTrajectorySpeed = 0.05f;
    [SerializeField, Range(3f, 8f)] public float StartZ = 5;
    [SerializeField] public Vector3 StartCardPosition = new Vector3(0f, -4.4f, 5f);
}; 