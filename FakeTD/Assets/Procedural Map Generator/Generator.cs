using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator  : MonoBehaviour{



    public GameObject dirtPrefab;
    public GameObject grassPrefab;
    PerlinNoise noise;

    int minX = 0;
    int maxX = 32;
    int minZ = 0;
    int maxZ = 64;
    int minY = 0;
    int maxY = 20;

    void Start()
    {
        noise = new PerlinNoise(Random.Range(1000000, 10000000));
        Regenerate();
    }
    private void Regenerate()
    {

        float width = dirtPrefab.transform.lossyScale.x;
        float height = dirtPrefab.transform.lossyScale.y;
        float depth = dirtPrefab.transform.lossyScale.z;

        for (int i = minX; i < maxX; i++)
        {//columns (x values)
            for (int k = minZ; k < maxZ; k++)
            {
                int columnHeight = 2 + noise.GetNoise(i - minX, k - minZ, maxY - minY - 2);
                for (int j = minY; j < minY + columnHeight; j++)
                {//rows (y values)
                    GameObject block = (j == minY + columnHeight - 1) ? grassPrefab : dirtPrefab;
                    Instantiate(block, new Vector3(i * width, j * height, k * depth), Quaternion.identity);
                }
            }
        }
    }
}
