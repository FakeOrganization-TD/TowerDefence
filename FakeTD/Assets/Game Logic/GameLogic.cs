using System.Collections.Generic;
using UnityEngine;

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

    
    List<pathField> pathFields;

    Agent agent;
    // Use this for initialization
    void Start()
    {
       
    }

    private void Awake()
    {
        pathFields = new List<pathField>();

    }
    
    
    // To ma w teorii dać liste kafelek ścieżki (path) 
    public void Initialize(Terrain[,] terrain, Vector2 startPoint,Vector2 endPoint)
    {
        mapStartPosition = startPoint;
        mapEndPosition = endPoint;
       
        // x- kolumna y - rząd 
        int x = (int) mapStartPosition.x;
        int y = (int) mapStartPosition.y;
        pathFields.Add(new pathField(new Vector2(x, y), terrain[x, y])); 
        do
        {
            if (!(y+1 > terrain.GetLength(1)) && terrain[x,y+1].TerrainType == TerrainType.Path && terrain[x, y + 1] != pathFields[pathFields.Count - 1].terrain) // Sprawdzamy sąsiadujące elementy tablicy
            {
                pathFields.Add(new pathField(new Vector2(x, y + 1), terrain[x, y + 1]));
                y++;
            }
            else if (!(y-1 < 0) && terrain[x,y-1].TerrainType == TerrainType.Path && terrain[x,y-1]!=pathFields[pathFields.Count-1].terrain)
            {
                pathFields.Add(new pathField(new Vector2(x, y - 1), terrain[x, y - 1]));
                y--;

            }
            else if (!( x - 1 < 0 ) && terrain[x - 1, y ].TerrainType == TerrainType.Path && terrain[x - 1, y ] != pathFields[pathFields.Count - 1].terrain)
            {
                pathFields.Add(new pathField(new Vector2(x-1, y), terrain[x-1, y ]));
                x--;

            }
            else if (!( x + 1 < 0 ) && terrain[x + 1, y].TerrainType == TerrainType.Path && terrain[x + 1, y] != pathFields[pathFields.Count - 1].terrain)
            {
                pathFields.Add(new pathField(new Vector2(x + 1, y), terrain[x + 1, y]));
                x++;

            }

        }
        while (x != mapEndPosition.x && y !=mapEndPosition.y); // jeśli są równe to ma przerwać 

        Agent.waypoints = pathFields;

        // TUTAJ SKOŃCZYLIŚMY
        //agent = Agent.GetAgent(Agent.normalAgentModel,new Vector3())
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
