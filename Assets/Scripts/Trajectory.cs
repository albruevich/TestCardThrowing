using System.Collections.Generic;
using UnityEngine;

public class Trajectory 
{  
    private Queue<Vector3> _points = new Queue<Vector3>();
    private Queue<Vector3> _workPpoints = new Queue<Vector3>();

    public Vector3[] Points => _points.ToArray();

    public int Count => _points.Count;
    public int WorkCount => _workPpoints.Count;

    public void AddPoint(Vector3 vector)
    {
        _points.Enqueue(vector);

        if (_points.Count > GameConfig.Instance.MaxPointsCount)
            _points.Dequeue();       
    }

    public Vector3 UsePoint()
    {     
        if(_workPpoints.Count > 0)
            return _workPpoints.Dequeue();
        return Vector3.zero;
    }    
  
    public void CorrectPointsForPosition(Vector3 position)
    {
        if (_points.Count == 0)
            return;

        _workPpoints.Clear();

        Vector3[] array = _points.ToArray();
        Vector3 differ = array[0] - position;

        int i = 0;
        foreach (Vector3 v in array)
        {
            _workPpoints.Enqueue(new Vector3(v.x - differ.x,
                position.y + i * GameConfig.Instance.VerticalTrajectorySpeed, v.z - differ.z));
            i++;
        }
    }

    public void Clear()
    {
        _points.Clear();       
    }

    public void ClearWorkPoints()
    {
        _workPpoints.Clear();
    }
}
