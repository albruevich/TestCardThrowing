using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private CardsFactory _cardsFactory;

    private Card _currentCard;

    private bool _isTouch;

    private Camera _mainCamera;

    private Plane xyPlane;
    private float lastPlaneZ = 0;
    const float startZ = 5f;

    private void Start()
    {
        _mainCamera = Camera.main;
        _currentCard = _cardsFactory.Get();
        _currentCard.SetPosition(new Vector3(-2.8f, -4.4f, startZ));
    }

    void Update()
    {

#if !UNITY_EDITOR

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
        _isTouch = true;

        if (_currentCard == null)
            return;
       
         _currentCard.SetPosition(WorldPositionOnPlane(position, startZ));
    }

    private void TouchMoved(Vector3 position)
    {
        if (!_isTouch || _currentCard == null)
            return;
       
        _currentCard.SetPosition(WorldPositionOnPlane(position, startZ));
    }

    private void TouchEnded(Vector3 position)
    {
        _isTouch = false;

        if (_currentCard == null)
            return;

        _currentCard.ThrowFromPosition(WorldPositionOnPlane(position, startZ));
    } 

    private Vector3 WorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);

        if (lastPlaneZ != z)
        {
            xyPlane = new Plane(Vector3.forward, new Vector3(0, 0, z));
            lastPlaneZ = z;
        }
      
        float distance;
        xyPlane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }   
}
