using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
   public enum AgentType
    {
        Normal,
        Fast,
        Tank
    };

    public static AgentType agentType;

    static public List<Vector3> waypoints;
    
    Terrain[,] terrainMatrix;
    Vector3 position,oldPosition;

    [SerializeField]
    static GameObject normalAgentModel;

    [SerializeField]
    static GameObject fastAgentModel;

    [SerializeField]
    static GameObject tankAgentModel;

    [SerializeField]
    Vector3 size;

    [SerializeField]
    float scale=1;

    [SerializeField]
    Vector3 speed;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
    int healthPoints;

    // Use this for initialization
    void Start ()
    {
	    	
	}



    
    public Agent(GameObject model, Vector3 startPosition, int hp)
    {
        this.position = startPosition;
        size = Vector3.one;
        this.healthPoints = hp;
        speed = Vector3.one;

    }

    

 static Agent GetAgent(GameObject model, Vector3 startPosition,AgentType type)
    {

        switch (type)
        {
            case AgentType.Normal:
                return new Agent(normalAgentModel, startPosition, 100);

            case AgentType.Fast:
                return new Agent(fastAgentModel, startPosition, 40);

            case AgentType.Tank:
                return new Agent(tankAgentModel, startPosition, 150);

            default: return null; // A się ktoś zdziwi HEHE pozdro dla kumatych

        }
    }

    void Destroy()
    {
        
    }
    // Update is called once per frame
    void Update ()
    {
		
	}
}
