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
    const float interval = 1f;

    [SerializeField]
    static Vector2 mapStartPosition;

    [SerializeField]
    static Vector2 mapEndPosition;

    Vector2 startPoint;
    public List<Vector2> pathTiles;
    Agent agent;

    List<Agent> agents;
    
    const int maxNumberOfMobs = 10;
    int NumberOfMobs;
    float timeLeft = 1.0f;
    bool initalised = false;
    // Use this for initialization
    void Start()
    {
        agents = new List<Agent>();
        NumberOfMobs = 0;
    }


    // Wyczyścić to niezbędnego minimum 
    public void Initalize(Terrain[,] terrain,Vector2 startPoint,Vector2 endPoint)
    {
        Agent.waypoints = new List<Vector2>(pathTiles);
        Agent.waypoints.Add(startPoint);
        Agent.waypoints.Add(endPoint);        
        Agent.waypoints.Reverse();

        this.startPoint = endPoint;

        


        //agent = new Agent(GameObject.Find("Enemy"))
        initalised = true;
   

    }

    // To ma w teorii dać liste kafelek ścieżki (path) 

    // Update is called once per frame
  public  void Update()
    {
        #region agentSpawner 
        if (!initalised)
            return;

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0 && NumberOfMobs < maxNumberOfMobs)
            {
            
            GameObject enemy = GameObject.Find("Normal_Enemy");

          
                agents.Add(new Agent(
               Instantiate(enemy, new Vector3(startPoint.x, 1, startPoint.y), Quaternion.identity), startPoint, 100
                ));

            NumberOfMobs++;

            timeLeft = interval;

            }
        #endregion

        if (agents != null)
        foreach (Agent agent in agents)
        {
            
            agent.Update();
        }
    }
}
