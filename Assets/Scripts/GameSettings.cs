using UnityEngine;

public struct GameSettings 
{
    public static readonly int MaxPointsCount = 40;
    public static readonly float TrajectorySpeed = 0.02f;
    public static readonly float VerticalTrajectorySpeed = 0.05f;
    public static readonly float StartZ = 5f;
    public static readonly Vector3 StartCardPosition = new Vector3(0f, -4.4f, StartZ);
}
