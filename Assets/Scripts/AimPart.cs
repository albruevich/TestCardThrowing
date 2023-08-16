using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class AimPart : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();

        _rigidbody.useGravity = false;
        _collider.enabled = false;
    }

    public void Paint(Material material)
    {
        _meshRenderer.material = material;
    }

    public void FallApart(Vector3 force, Vector3 angularVelocity)
    {
        _rigidbody.useGravity = true;
        _collider.enabled = true;
        _rigidbody.AddForce(force, ForceMode.Impulse);
        _rigidbody.angularVelocity = angularVelocity;
    }
}
