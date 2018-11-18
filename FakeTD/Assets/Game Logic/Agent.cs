﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Agent : MonoBehaviour
{
   public enum AgentType
    {
        Normal,
        Fast,
        Tank
    };

    public static AgentType agentType;



     public static List<Vector2> waypoints;
    Terrain[,] terrainMatrix;
    Vector3 position,oldPosition;
    private int pathIndex=-5;
    public  bool isActive;

    [SerializeField]
    GameObject ActualAgentModel;

    [SerializeField]
    Vector3 size;

    [SerializeField]
    float scale;

    [SerializeField]
    float speed;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
    int healthPoints;

    bool exists;

    // Use this for initialization
    void Start ()
    {
        isActive = false;
	}

    
    public Agent(GameObject model, Vector3 startPosition, int hp) : base()
    {
        this.position = startPosition;
        size = Vector3.one;
        this.healthPoints = hp;
        speed = 5f;
        ActualAgentModel = model;
        scale = 1;
        pathIndex = waypoints.Count - 1;
        exists = true;

       // isActive = true;
    }

    public void Move()
    {
        if (pathIndex < 0 || !exists)
            return;

        float step = Time.deltaTime * speed;
            var x = waypoints[pathIndex].x;
            var y = waypoints[pathIndex].y;
            var z = Generator.GeneratedMap[(int) x, (int) y].Height;
        //ActualAgentModel.transform.position = new Vector3(x, z, y);
        Vector3 origin = ActualAgentModel.transform.position;
        Vector3 destination = new Vector3(x, z, y);
        //  Vector3 move = Vector3.MoveTowards(origin, destination, 1f);
        //   ActualAgentModel.transform.Translate(move * Time.deltaTime, Space.World);
        ActualAgentModel.transform.position = Vector3.MoveTowards(origin, destination, step);

        if (pathIndex > 0 && ActualAgentModel.transform.position ==destination)
        {
            Debug.Log("pathIndex Value: "+pathIndex.ToString());
            Debug.Log("ONIEONIE");
            pathIndex--;
        }
        if( x == waypoints[0].x && y == waypoints[0].y)
        {
            Destroy(ActualAgentModel);
            Destroy(this);
            exists = false;
        }
          
       
     
            
    }
     


    //void Destroy()
    //{
       
    //}
    // Update is called once per frame
   public void Update ()
    {
        if(waypoints !=null )
        {
            if(isActive ==false)
            {
                pathIndex = waypoints.Count - 1;
                isActive = true;
            }
            Debug.Log("Jestem przed move");
            Move();
        }
            
   
	}
}
