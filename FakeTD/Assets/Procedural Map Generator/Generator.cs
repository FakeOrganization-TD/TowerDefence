using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject dirtPrefab;
    public GameObject normalTerrainPrefab;
    public GameObject trashBinPrefab;
    public GameObject blackHolePrefab;
    public GameObject cristalPrefab;
    public GameObject destroyedTurretprefab;
    public GameObject pathPrefab;
    public float obstaclePropabilityPercent;

    PerlinNoise noise;
    int _minX = 0;
    int _maxX = 64;
    int _minZ = 0;
    int _maxZ = 64;
    int _minY = 0;
    int _maxY = 5;//Max height of mountains15

    void Start()
    {
        noise = new PerlinNoise(Random.Range(1000000, 10000000));
        GenerateMapFromMatrix(GenerateMapMatrix(obstaclePropabilityPercent));
        //Regenerate();
    }

    private Terrain[,] GenerateMapMatrix(float obstaclePropabilityPercent)
    {
        Terrain[,] map = new Terrain[_maxX, _maxZ];
        for (int i = _minX; i < _maxX; i++)
        {//columns (x values)
            for (int k = _minZ; k < _maxZ; k++)
            {
                int columnHeight = 2 + noise.GetNoise(i - _minX, k - _minZ, _maxY - _minY - 2);
                var terrainType = RandomTerrainType(obstaclePropabilityPercent);
                if (terrainType != TerrainType.Normal)
                {
                    map[i, k] = new Terrain(terrainType, columnHeight + 1);//place for special item
                }
                else
                {
                    map[i, k] = new Terrain(terrainType, columnHeight);
                }
            }
        }

        Vector2 start = new Vector2();
        Vector2 end = new Vector2();

        for (int i =0;i < _maxX; i++)
        {
            if (map[i, 0].TerrainType == TerrainType.Normal)
            {
                start = new Vector2(i, 0);
                break;
            }
                
        }
        for (int i = _maxX-1; i >=0 ; i--)
        {
            if (map[i, _maxZ-1].TerrainType == TerrainType.Normal)
            {
                end = new Vector2(i, _maxZ-1);
                break;
            }

        }


        var pathFinder = new Pathfinder(map, _maxX, _maxZ);
        map = pathFinder.SearchPath(start, end);//generateFakePath(map);//todo podmienić
        return map;
    }
    private Terrain[,] generateFakePath(Terrain[,] map)
    {
        for (int i = _minZ; i < _maxZ; i++)
        {
            map[_maxX / 2, i].Height = 1;
            map[_maxX / 2, i].TerrainType = TerrainType.Path;
        }
        return map;
    }

    private void GenerateMapFromMatrix(Terrain[,] map)
    {
        float width = dirtPrefab.transform.lossyScale.x;
        float height = dirtPrefab.transform.lossyScale.y;
        float depth = dirtPrefab.transform.lossyScale.z;

        for (int i = _minX; i < _maxX; i++)
        {//columns (x values)
            for (int k = _minZ; k < _maxZ; k++)
            {
                int columnHeight = map[i, k].Height;
                for (int j = _minY; j < _minY + columnHeight; j++)
                {//rows (y values)
                    if (j == columnHeight - 1) //item on top
                    {
                        switch (map[i, k].TerrainType)
                        {
                            case TerrainType.TrashBin:
                                {
                                    GameObject block = trashBinPrefab;
                                    Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                                }
                                break;
                            case TerrainType.BlackHole:
                                {
                                    GameObject block = blackHolePrefab;
                                    Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                                }
                                break;
                            case TerrainType.DestroyedTurret:
                                {
                                    GameObject block = destroyedTurretprefab;
                                    Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                                }
                                break;
                            case TerrainType.Cristal:
                                {
                                    GameObject block = cristalPrefab;
                                    Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                                }
                                break;
                            case TerrainType.Path:
                                {
                                    GameObject block = pathPrefab;
                                    Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                                }
                                break;
                            default:
                                {
                                    GameObject block = normalTerrainPrefab;
                                    Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                                }
                                break;
                        }
                    }
                    else
                    {
                        GameObject block = normalTerrainPrefab;
                        Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                    }

                }
            }
        }
    }
    private void Regenerate()
    {

        float width = dirtPrefab.transform.lossyScale.x;
        float height = dirtPrefab.transform.lossyScale.y;
        float depth = dirtPrefab.transform.lossyScale.z;

        for (int i = _minX; i < _maxX; i++)
        {//columns (x values)
            for (int k = _minZ; k < _maxZ; k++)
            {
                int columnHeight = 2 + noise.GetNoise(i - _minX, k - _minZ, _maxY - _minY - 2);
                for (int j = _minY; j < _minY + columnHeight; j++)
                {//rows (y values)
                    GameObject block = (j == _minY + columnHeight - 1) ? normalTerrainPrefab : dirtPrefab;
                    Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                }
            }
        }
    }


    private TerrainType RandomTerrainType(float obstaclePropabilityPercent)
    {
        var propability = Random.Range(0.0f, 1.0f);
        if (propability <= obstaclePropabilityPercent)
        {
            if (propability <= obstaclePropabilityPercent / 4)
            {
                return TerrainType.BlackHole;
            }
            else if (propability <= obstaclePropabilityPercent / 3)
            {
                return TerrainType.Cristal;
            }
            else if (propability <= obstaclePropabilityPercent / 2)
            {
                return TerrainType.DestroyedTurret;
            }
            else// (propability > 0.3 && propability <= 0.4)
            {
                return TerrainType.TrashBin;
            }
        }
        else
        {
            return TerrainType.Normal;
        }
    }
}
