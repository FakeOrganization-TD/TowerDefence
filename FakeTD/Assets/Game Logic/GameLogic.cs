using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
/// ///////////////////////////////////////

public class pathField
{
    public Vector2 index;
    public Terrain terrain;

    public pathField(Vector2 ind, Terrain ter)
    {
        /// Index elementu path w tablicy Terrain[,] 
        index = ind;
        terrain = ter;
    }
}               

/// ////////////////////////////////////

public class GameLogic : MonoBehaviour
{

    [SerializeField]
    readonly Terrain[,] terrainMatrix;

    [SerializeField]
    readonly int interval;

    [SerializeField]
    static Vector2 mapStartPosition;

    [SerializeField]
    static Vector2 mapEndPosition;

    public List<Vector2> pathTiles;
    Agent agent;

    List<Agent> agents;
     
    // Use this for initialization
    void Start()
    {
        agents = new List<Agent>();
        
    }

    
        // Argumenty wyrzucić do konstruktora
        // zmienić nazwę na spawnAgent
        // Wrzucić do update i ustawić przed nim licznik czasu,
        // aby moby tworzyły się co określony interwał.
    public void Initalize(Terrain[,] terrain,Vector2 startPoint,Vector2 endPoint)
    {
        Agent.waypoints = new List<Vector2>(pathTiles);

        //agent = new Agent(GameObject.Find("Enemy"))
        float timeLeft = 10f;

        #region Debug Code
        GameObject enemy = GameObject.Find("Normal_Enemy");

        for (int i = 0; i < 5; i++)
        {

                agents.Add(new Agent(
               Instantiate(enemy, new Vector3(startPoint.x, 1, startPoint.y), Quaternion.identity), startPoint, 100
                ));
    
            }
        GameObject.DestroyObject(enemy);
        



        #endregion

    }

    // To ma w teorii dać liste kafelek ścieżki (path) 

    // Update is called once per frame
  public  void Update()
    {
        foreach (Agent agent in agents)
        {

            agent.Update();
        }
    }
}
