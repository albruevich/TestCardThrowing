using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory 
{    
    const int _maxPointsCount = 40;

    private Queue<Vector3> _points = new Queue<Vector3>();

    public Vector3[] Points => _points.ToArray();   


    private Game _game;

    public Trajectory(Game game)
    {
        _game = game;
    }

    public void AddPoint(Vector3 vector)
    {
        _points.Enqueue(vector);

        if (_points.Count > _maxPointsCount)
            _points.Dequeue();       
    }

    public Vector3 UsePoint()
    {
        return _points.Dequeue();
    }    
   

    public void Clear()
    {
        _points.Clear();       
    }
}
