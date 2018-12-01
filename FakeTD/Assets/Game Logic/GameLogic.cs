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
    static int DebugTargetCounter = 0;
    [SerializeField]
    readonly Terrain[,] terrainMatrix;

    [SerializeField]
    float interval = 0.5f; // odstepy pomiedzy agentami 

    [SerializeField]
    static Vector2 mapStartPosition;

    [SerializeField]
    static Vector2 mapEndPosition;

    Vector2 startPoint;
    public List<Vector2> pathTiles;


    public static List<Agent> agents = new List<Agent>();
    public static List<Tower> towers = new List<Tower>();
    const int maxNumberOfMobs = 8;
    int NumberOfMobs;
    float timeLeft = 1.0f;
    bool initalised = false;
    private WaveController wavesController;


    // Use this for initialization
    void Start()
    {
        // towers = new List<Tower>();

        NumberOfMobs = 0;
    }


    // Wyczyścić to niezbędnego minimum 
    public void Initalize(Terrain[,] terrain, Vector2 startPoint, Vector2 endPoint)
    {
        Agent.waypoints = new List<Vector2>(pathTiles);
        Agent.waypoints.Add(startPoint);
        Agent.waypoints.Add(endPoint);
        Agent.waypoints.Reverse();

        this.startPoint = endPoint;

        TowerBuilder.terrainMatrix = terrain;


        //agent = new Agent(GameObject.Find("Enemy"))
        initalised = true;
        wavesController = new WaveController(true);//todo ustawiać parametr z menu


    }

    public bool PointInsideSphere(Vector3 point, Vector3 center, float radius)
    {
        return Vector3.Distance(point, center) < radius;
    }

    public bool PointInsideCircle(Vector2 point, Vector2 center, float radius)
    {
        return Vector2.Distance(point, center) < radius;
    }

    // Funkcja za pomoca ktorej wierze obieraja agentow na cel 
    public void ChooseTargetMob()
    {
        if (towers.Count <= 0)
            return;
        if (agents.Count <= 0)
            return;

        foreach (Tower tower in towers)
        {
            if (tower.target == null)
            {
                foreach (Agent agent in agents)
                {
                    // czy agent jest w polu razenia wierzy 
                    if (PointInsideCircle(new Vector2(agent.ActualAgentModel.transform.position.x, agent.ActualAgentModel.transform.position.z)
                                        , new Vector2(tower.model.transform.position.x, tower.model.transform.position.z), tower.range))
                    {
                        tower.target = agent;
                        break;
                    }

                }
            }
            else if (tower.target != null)
            {
                if (!PointInsideCircle(new Vector2(tower.target.ActualAgentModel.transform.position.x,
                    tower.target.ActualAgentModel.transform.position.z),
                    new Vector2(tower.model.transform.position.x, tower.model.transform.position.z), tower.range))
                {
                    tower.target = null;
                }

            }

        }

    }


    // To ma w teorii dać liste kafelek ścieżki (path) 

    // Update is called once per frame
    public void Update()
    {
        if (!initalised)
            return;
     
        #region agentSpawner 
        

        timeLeft -= Time.deltaTime;



        if (wavesController.currentWave >= wavesController.wavesCount)
        {
            WaveTextManager.Message = "No waves left";
            return; //todo koniec fal, wygranko
        }
        if (wavesController.waves[wavesController.currentWave].Count == 0 &&
            agents.Count == 0 &&
            wavesController.currentWave<wavesController.wavesCount)
        {
            wavesController.currentWave++;
            //todo kończy się aktualna fala. Zrobić przerwę czasową albo coś
        }
       
        if (timeLeft < 0 && wavesController.waves[wavesController.currentWave].Count > 0)
        {
            GameObject enemy = GameObject.Find("FastMob");

            

            WaveTextManager.Message = "Wave " + (wavesController.currentWave + 1);
            var Agent = gameObject.AddComponent<Agent>();
            Agent.Initalize(
              Instantiate(enemy, new Vector3(startPoint.x, 1, startPoint.y),
              Quaternion.identity),
              startPoint,
              wavesController.waves[wavesController.currentWave][0]);

            agents.Add(Agent);
            if (wavesController.waves[wavesController.currentWave].Count > 0)
                wavesController.waves[wavesController.currentWave].RemoveAt(0);

            NumberOfMobs++;

            timeLeft = interval;

        }
        #endregion

        Debug.Log("Current wave "+wavesController.currentWave);
        Debug.Log("Count in wave" + wavesController.waves[wavesController.currentWave].Count);

        ChooseTargetMob();
        //Debug.Log("Enemys in list " + agents.Count);

    }
}
