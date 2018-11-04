using UnityEngine;
using System;
public class PlayGame : MonoBehaviour
{

    public Generator gene;
    // Use this for initialization
    Terrain[,] terrainMatrix;
    Vector2 mapStartPosition;
    public void Play()
    {
        
        try
        {
            Debug.Log("Jestem!");
            terrainMatrix = gene.RunGenerationProcedure(mapStartPosition);
        }
         catch (Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("Null reference Generatore adore\nOLE!");
        }
        
        

    }

}

