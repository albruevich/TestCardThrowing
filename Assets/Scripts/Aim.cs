using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Aim : MonoBehaviour
{
    [SerializeField]
    private AimType _aimType;   

    [SerializeField]
    private AimPart[] _aimParts;

    public AimFactory OriginFactory { get; set; }

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetMaterial(Material material)
    {
        foreach(AimPart ap in _aimParts)
        {
            ap.Paint(material); 
        }     
    }

    private void OnCollisionEnter(Collision collision)
    {       
        _collider.enabled = false;      

        Explode(index: 0, vertMin: -1, vertMax: -5);
        Explode(index: 1, vertMin: 4, vertMax: 8);        

        StartCoroutine(Destroy());
    }

    private void Explode(int index, int vertMin, int vertMax)
    {
        const float side = 4f;
        const float angle = 10f;

        _aimParts[index].FallApart(
            force:
            new Vector3(Random.Range(-side, side), Random.Range(vertMin, vertMax), Random.Range(-side, side)),
            angularVelocity:
            new Vector3(Random.Range(-angle, angle), Random.Range(-angle, angle), Random.Range(-angle, angle))
            );
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        OriginFactory.Reclaim(this);
    }
}
