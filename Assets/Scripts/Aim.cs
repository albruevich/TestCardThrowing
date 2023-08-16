using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Aim : MonoBehaviour
{
    [SerializeField]
    private AimType _aimType;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    public AimFactory OriginFactory { get; set; }
   

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetMaterial(Material material)
    {        
        _meshRenderer.material = material;
    }
}
