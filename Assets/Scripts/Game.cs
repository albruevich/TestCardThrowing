#define DRAW_GIZMOS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private CardsFactory _cardsFactory;
    [SerializeField]
    private AimFactory _aimFactory;
    [SerializeField]
    private DrawTrajectory _drawTrajectory;

    private Card _currentCard;

    private Trajectory _trajectory;

    private Camera _mainCamera;

    private Plane _xyPlane;
    private float _lastPlaneZ = 0;      

    private void Start()
    {
        _mainCamera = Camera.main;
        CreateAims();
        StartCoroutine(CreateNewCard(0f));        
    }

    private IEnumerator CreateNewCard(float delay)
    {
        yield return new WaitForSeconds(delay);
        _currentCard = _cardsFactory.Get();
    }

    private void CreateAims()
    {
        _aimFactory.CreateAims();
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

        _trajectory = new Trajectory();
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
        _currentCard = null;

        StartCoroutine(CreateNewCard(0.7f));
    } 

    private void CreateNewPosition(Vector3 position)
    {     
        Vector3 pos = WorldPositionOnPlane(position, GameSettings.StartZ + position.y * GameSettings.TrajectorySpeed);        
        _drawTrajectory.AddPoint(pos);
        pos.y = _currentCard.transform.position.y;
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
