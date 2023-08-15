using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardsFactory OriginFactory { get; set; }

    public void SetPosition(Vector3 position)
    {       
        transform.position = position;
    } 

    public void ThrowWithTrajectory(Trajectory trajectory)
    {

    }
}
