using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{

    public Generator gene;
    private GameLogic gameLogic;
    private GameObject button;
    // Use this for initialization
    Terrain[,] terrainMatrix;
    Vector2 mapStartPosition,mapEndPosition;
    TowerBuilder towerBuilder;
    public void Start()
    {
        gameObject.AddComponent<GameLogic>();
        gameLogic = GameObject.FindObjectOfType<GameLogic>();
        gameObject.AddComponent<TowerBuilder>();

        Play();
    }
    public void Play()
    {
       
        // button = GameObject.Find("BtnNewGame");
        // ukrywa guzik
        // [ Stary Button]
        //    button.transform.localScale = new Vector3(0, 0, 0);

        
        try
        {
          //  button.gameObject.SetActive(false);
            //Debug.Log("Jestem!");
            terrainMatrix = gene.RunGenerationProcedure(out mapStartPosition,out mapEndPosition);
            gameLogic.pathTiles = new System.Collections.Generic.List<Vector2>(gene.pathTilesList);
          

            
            gameLogic.Initalize(gene,terrainMatrix, mapStartPosition, mapEndPosition);
            

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

