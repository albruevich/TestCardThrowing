using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AimFactory : GameObjectFactory
{    
    [SerializeField]
    private Aim _cylinderPrefab;   
    [SerializeField]
    private Aim _boxPrefab;
    [SerializeField]
    private Aim _spherePrefab;

    [SerializeField]
    private Material[] _materials;

    private List<Aim> _aims;

    public void Reclaim(Aim aim)
    {
        _aims.Remove(aim);
        Destroy(aim.gameObject);

        if(_aims.Count == 0)
        {
            CreateAims();
        }
    }

    public void CreateAims()
    {
        _aims = new List<Aim>();       

        for (int i = 0; i < Random.Range(4, 7); i++)
        {
            Aim aim = GetRandomAim();
            _aims.Add(aim);
        }

        SetPositionsToAims();
    }

    private void SetPositionsToAims()
    {
        List<Vector2Int> cells = new List<Vector2Int>();
        for(int x = -10; x < 10; x += 5)
        {
            for (int y = 25; y < 40; y += 5)
            {
                cells.Add(new Vector2Int(x, y));
            }
        }

        foreach(Aim aim in _aims)
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
        switch((AimType)Random.Range(0, AimType.GetNames(typeof(AimType)).Length))      
        {
            case AimType.Cylinder: prefab = _cylinderPrefab; break;          
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
    Box,
    Sphere
}
