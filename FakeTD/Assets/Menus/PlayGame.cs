using UnityEngine;
using System;
public class PlayGame : MonoBehaviour
{

    public Generator gene;
    private GameLogic gameLogic;
    private GameObject button;
    // Use this for initialization
    Terrain[,] terrainMatrix;
    Vector2 mapStartPosition,mapEndPosition;

    public void Start()
    {
        gameObject.AddComponent<GameLogic>();
        gameLogic = GameObject.FindObjectOfType<GameLogic>();
        button = GameObject.Find("BtnNewGame");
    }
    public void Play()
    {

        // ukrywa guzik
        button.transform.localScale = new Vector3(0, 0, 0);
        
        try
        {
          //  button.gameObject.SetActive(false);
            Debug.Log("Jestem!");
            terrainMatrix = gene.RunGenerationProcedure(out mapStartPosition,out mapEndPosition);
            gameLogic.pathTiles = new System.Collections.Generic.List<Vector2>(gene.pathTilesList);
          

            
            gameLogic.Initalize(terrainMatrix, mapStartPosition, mapEndPosition);

        }
         catch (Exception ex)
        {
            Debug.LogException(ex);
         
        }           

    }
    public void Update()
    {
       // gameLogic.Update();
    }
}

