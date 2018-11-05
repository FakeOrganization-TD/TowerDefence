using UnityEngine;
using System;
public class PlayGame : MonoBehaviour
{

    public Generator gene;
    private GameLogic gameLogic;
    // Use this for initialization
    Terrain[,] terrainMatrix;
    Vector2 mapStartPosition,mapEndPosition;
    public void Play()
    {
        
        try
        {
            Debug.Log("Jestem!");
            terrainMatrix = gene.RunGenerationProcedure(mapStartPosition,mapEndPosition);
            gameLogic.Initialize(terrainMatrix, mapStartPosition, mapEndPosition);

        }
         catch (Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("Null reference Generatore adore\nOLE!");
        }
        
        

    }

}

