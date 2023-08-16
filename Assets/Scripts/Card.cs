using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Card : MonoBehaviour
{
    public CardsFactory OriginFactory { get; set; }

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetPosition(Vector3 position)
    {       
        transform.position = position;
    } 

    public void ThrowWithTrajectory(Trajectory trajectory)
    {
        trajectory.CorrectPointsForPosition(transform.position);
        StartCoroutine(Move(trajectory));
    }

    IEnumerator Move(Trajectory trajectory)
    {
        Vector3 pos = trajectory.UsePoint();
        Vector3 v1 = Vector3.zero, v2 = Vector3.zero;

        while (pos != Vector3.zero)
        {            
            transform.position = pos;            
            yield return null;
            pos = trajectory.UsePoint();

            if (trajectory.WorkCount == 2)
                v1 = pos;
            else if (trajectory.WorkCount == 1)
                v2 = pos;
        }       

        Vector3 impulse = Vector3.ClampMagnitude((v2 - v1) * 200f, 90f);       
        _rigidbody.AddForce(impulse, ForceMode.Impulse);
        _rigidbody.useGravity = true;

        trajectory.ClearWorkPoints();

        yield return new WaitForSeconds(4f);

        Reclaim();
    }

    private void Reclaim()
    {
        OriginFactory.Reclaim(this);
    }
}
