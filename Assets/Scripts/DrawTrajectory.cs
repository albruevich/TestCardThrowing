using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawTrajectory : MonoBehaviour
{
    private LineRenderer _lineRenderer;   

    private Queue<Vector3> _points = new Queue<Vector3>();

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Clear();
    }

    private void UpdateLine(Vector3[] points)
    {
        if (points == null)
            return;
      
        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
    }

    public void AddPoint(Vector3 vector)
    {
        _points.Enqueue(vector);

        if (_points.Count > GameSettings.MaxPointsCount)
            _points.Dequeue();

        UpdateLine(_points.ToArray());
    }

    public void Clear()
    {
        _points.Clear();
        _lineRenderer.positionCount = 0;
        _lineRenderer.SetPositions(_points.ToArray());
    }
}
