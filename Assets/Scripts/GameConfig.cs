using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameConfig : MonoBehaviour
{
    private static GameConfig _instance;
    public static GameConfig Instance => _instance;

    [SerializeField] private GameSettings _gameSettings;

    public int MaxPointsCount => _gameSettings.MaxPointsCount;
    public float TrajectorySpeed => _gameSettings.TrajectorySpeed;
    public float VerticalTrajectorySpeed => _gameSettings.VerticalTrajectorySpeed;
    public float StartZ => _gameSettings.StartZ;
    public Vector3 StartCardPosition => _gameSettings.StartCardPosition;

    public void Init()
    {
        _instance = this;

        DontDestroyOnLoad(this);
    }  
}