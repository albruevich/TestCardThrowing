//#define DRAW_GIZMOS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private CardsFactory _cardsFactory;
    [SerializeField]
    private DrawTrajectory _drawTrajectory;

    private Card _currentCard;

    private Trajectory _trajectory;

    private Camera _mainCamera;

    private Plane _xyPlane;
    private float _lastPlaneZ = 0;

    const float _startZ = 5f;
    static readonly Vector3 _startCardPosition = new Vector3(0f, -4.4f, _startZ);   

    private void Start()
    {
        _mainCamera = Camera.main;
        _currentCard = _cardsFactory.Get();
        _currentCard.SetPosition(_startCardPosition);
    }

    void Update()
    {

#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR

        if(Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];

            switch(touch.phase)
            {
                case TouchPhase.Began: TouchBegan(touch.position); break;
                case TouchPhase.Moved: TouchMoved(touch.position); break;
                case TouchPhase.Ended: TouchEnded(touch.position); break;
                default: break;
            }
        }       
#else

        if (Input.GetMouseButtonDown(0))
        {
            TouchBegan(Input.mousePosition);            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TouchEnded(Input.mousePosition);           
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            TouchMoved(Input.mousePosition);
        }
#endif
    }

    private void TouchBegan(Vector3 position)
    {              
        if (_currentCard == null)
            return;

        _trajectory = new Trajectory(this);
        CreateNewPosition(position);        
    }

    private void TouchMoved(Vector3 position)
    {
        if (_trajectory == null || _currentCard == null)
            return;

        CreateNewPosition(position);
    }

    private void TouchEnded(Vector3 position)
    {
        if (_trajectory == null || _currentCard == null)
            return;     

        _currentCard.ThrowWithTrajectory(_trajectory);

        _drawTrajectory.Clear();
        _trajectory.Clear();
        _trajectory = null;
    } 

    private void CreateNewPosition(Vector3 position)
    {
        Vector3 pos = WorldPositionOnPlane(position, _startZ + position.y * 0.01f);        
        _drawTrajectory.AddPoint(pos);
        pos.y = 0;
        _trajectory.AddPoint(pos);
    }

    private Vector3 WorldPositionOnPlane(Vector3 screenPosition, float z)
    {      
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);

        if (_lastPlaneZ != z)
        {
            _xyPlane = new Plane(Vector3.forward, new Vector3(0, 0, z));
            _lastPlaneZ = z;
        }
      
        float distance;
        _xyPlane.Raycast(ray, out distance);      
        return ray.GetPoint(distance);
    }

#if DRAW_GIZMOS
    private void OnDrawGizmos()
    {
        if(_trajectory != null)
        {
            foreach (Vector3 p in _trajectory.Points)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(p, 1);
            }
        }    
    }
#endif
}
