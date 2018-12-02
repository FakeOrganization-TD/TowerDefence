using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Agent : MonoBehaviour
{
    public enum AgentType
    {
        Normal = 100,
        Fast= 40,
        Tank = 160
    };

    public static AgentType agentType;



    public static List<Vector2> waypoints;
    Terrain[,] terrainMatrix;
    Vector3 position, oldPosition;
    private int pathIndex = -5;
    public bool isActive;
    public float cashIncome;
    public float damage;
    [SerializeField]
  public  GameObject ActualAgentModel;

    [SerializeField]
    Vector3 size;

    [SerializeField]
    float scale;

    [SerializeField]
   public float speed;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
  public  int healthPoints;

    bool exists;

    // Use this for initialization
    void Start()
    {
        isActive = false;
    }


    public Agent(GameObject model, Vector3 startPosition, int hp) : base()
    {

        this.position = startPosition;
        size = Vector3.one;
        this.healthPoints = hp;
        speed = 5;
        ActualAgentModel = model;
        scale = 1;
        pathIndex = waypoints.Count - 1;
        exists = true;

        // isActive = true;
    }

    // Narazie uzywany ale odradzany
    public void Initalize(GameObject model, Vector3 startPosition, int hp)
     {

        this.position = startPosition;
        size = Vector3.one;
        this.healthPoints = hp;
        speed = 3f; // PREDKOSC
        ActualAgentModel = model;
        scale = 1;
        pathIndex = waypoints.Count - 1;
        exists = true;

    }

    public void Initalize(GameObject model, Vector3 startPosition, AgentType type)
    {
        this.position = startPosition;
        size = Vector3.one;
        exists = true;
        pathIndex = waypoints.Count - 1;
        ActualAgentModel = model;
        switch (type)
        {
            case AgentType.Normal: 
                agentType = AgentType.Normal;
                healthPoints = (int)agentType;
                speed = 3f;
                cashIncome = 4;
                damage = 5;
                break;

            case AgentType.Fast:
                agentType = AgentType.Fast;
                healthPoints = (int)agentType;
                speed = 8f;
                cashIncome = 2.5f;
                damage = 2.5f;
                break;

            case AgentType.Tank:
                agentType = AgentType.Tank;
                healthPoints = (int)agentType;
                speed = 2.5f;
                damage = 10;
                cashIncome = 6;
                break;

        }


       ActualAgentModel = model;
        scale = 1;
        pathIndex = waypoints.Count - 1;
        exists = true;

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
            //Debug.Log("pathIndex Value: "+pathIndex.ToString());
            pathIndex--;
        }
        if( x == waypoints[0].x && y == waypoints[0].y)
        {
            MoneyAndScores.hpNexus -= this.damage;
            GameLogic.agents.Remove(this);
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
            //Debug.Log("Jestem przed move");
            Move();
        }
         
        if(healthPoints <=0 )
        {
            MoneyAndScores.money += this.cashIncome;
            GameLogic.agents.Remove(this);
            Destroy(ActualAgentModel);
            Destroy(this);
            exists = false;
        }
   
	}
}
