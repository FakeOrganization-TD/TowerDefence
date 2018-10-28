﻿using System.Collections;
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

    PerlinNoise noise;
    int _minX = 0;
    int _maxX = 64;
    int _minZ = 0;
    int _maxZ = 64;
    int _minY = 0;
    int _maxY = 20;

    void Start()
    {
        noise = new PerlinNoise(Random.Range(1000000, 10000000));
        GenerateMapFromMatrix(GenerateMapMatrix());
        //Regenerate();
    }

    private Terrain[,] GenerateMapMatrix()
    {
        Terrain[,] map = new Terrain[_maxX, _maxZ];
        for (int i = _minX; i < _maxX; i++)
        {//columns (x values)
            for (int k = _minZ; k < _maxZ; k++)
            {
                int columnHeight = 2 + noise.GetNoise(i - _minX, k - _minZ, _maxY - _minY - 2);
                //rows (y values)
                var terrainType = RandomTerrainType();
                if (terrainType == TerrainType.Path)
                {
                    map[i, k] = new Terrain(terrainType, 1);
                }
                else
                {
                    map[i, k] = new Terrain(terrainType, columnHeight);
                }
            }
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
                                //TODO zmienić na inny prefab ze ścieżką
                                GameObject block = normalTerrainPrefab;
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


    private TerrainType RandomTerrainType()
    {
        var propability = Random.Range(0.0f, 1.0f);
        if (propability <= 0.1)
        {
            return TerrainType.BlackHole;
        }
        else if (propability > 0.1 && propability <= 0.2)
        {
            return TerrainType.Cristal;
        }
        else if (propability > 0.2 && propability <= 0.3)
        {
            return TerrainType.DestroyedTurret;
        }
        else if (propability > 0.3 && propability <= 0.4)
        {
            return TerrainType.TrashBin;
        }
        else
        {
            return TerrainType.Normal;
        }
    }
}