using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AimFactory : GameObjectFactory
{
    
    [SerializeField]
    private Aim _cylinderPrefab;
    [SerializeField]
    private Aim _caplulePrefab;
    [SerializeField]
    private Aim _boxPrefab;
    [SerializeField]
    private Aim _spherePrefab;

    [SerializeField]
    private Material[] _materials;

    public void Reclaim(Aim aim)
    {
        Destroy(aim.gameObject);
    }

    public void CreateAims()
    {
        List<Aim> aims = new List<Aim>();

        for(int i = 0; i < Random.Range(4, 7); i++)
        {
            Aim aim = GetRandomAim();
            aims.Add(aim);
        }

        SetPositionsToAims(aims);
    }

    private void SetPositionsToAims(List<Aim> aims)
    {
        List<Vector2Int> cells = new List<Vector2Int>();
        for(int x = -10; x < 10; x += 5)
        {
            for (int y = 25; y < 40; y += 5)
            {
                cells.Add(new Vector2Int(x, y));
            }
        }

        foreach(Aim aim in aims)
        {
            int index = Random.Range(0, cells.Count);
            Vector2Int v = cells[index];
            aim.SetPosition(new Vector3(v.x, -2.5f, v.y));
            cells.RemoveAt(index);
        }
    }

    private Aim GetRandomAim()
    {
        Aim prefab = null;
        switch((AimType)Random.Range(0, 4))
        {
            case AimType.Cylinder: prefab = _cylinderPrefab; break;
            case AimType.Capsule: prefab = _caplulePrefab; break;
            case AimType.Box: prefab = _boxPrefab; break;
            case AimType.Sphere: prefab = _spherePrefab; break;
        }

        Aim aim = CreateGameObjectInstance(prefab);
        aim.OriginFactory = this;
        aim.SetMaterial(_materials[Random.Range(0, _materials.Length)]);
        return aim;
    }
}

public enum AimType
{
    Cylinder,
    Capsule,
    Box,
    Sphere
}
