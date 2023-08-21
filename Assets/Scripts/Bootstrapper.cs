using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Game _game;
    [SerializeField] private DrawTrajectory _drawTrajectory;

    private void Awake()
    {
        _gameConfig.Init();
        _game.Init();
        _drawTrajectory.Init();
    }
}
