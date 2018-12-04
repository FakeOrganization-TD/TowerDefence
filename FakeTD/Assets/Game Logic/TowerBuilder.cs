using Assets.Game_Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TowerBuilder : MonoBehaviour
{

    public  enum ChosenTower
    {
        Basic=15,
        CannonTower=10,
        Sniper= 30,
        Fast= 20,
        None
    }
    
    

    float MousePosX;
    float MousePosY;
    float MousePosZ;
    public GameObject Basic;
    public GameObject Fast;
    public GameObject Sniper;
    public GameObject CannonTower;
    static GameObject chosenTowerModel ;
    public GameObject MainCamera;
    public static Terrain[,] terrainMatrix;
    string towerTag = "";
    // ChosenTower chosenTower;

    //public TowerBuilder(GameObject basic, GameObject cannonTower, GameObject sniper, GameObject fast, ChosenTower chosenTower)
    //{
    //    Basic = basic;
    //    CannonTower = cannonTower;
    //    Sniper = sniper;
    //    Fast = fast;
    //    this.chosenTower = chosenTower;
    //}


    public  void ChoseTower( string strChosenTower)
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        TowerChoser.isSelected = true;
        TowerChoser.ChosenTower=(ChosenTower)Enum.Parse(typeof(ChosenTower), strChosenTower, true);
        
        
        switch (TowerChoser.ChosenTower)
        {
            case ChosenTower.Basic:
                Debug.Log("IT CANONS !");
                towerTag="BasicTowerTag";

                break;

            case ChosenTower.CannonTower:
                break;

            case ChosenTower.Fast:
                towerTag = "FastTowerTag";
                break;

            case ChosenTower.Sniper:
                break;

            case ChosenTower.None:
                Destroy(chosenTowerModel);
                break;

        }
    }


    public void AttachTowerToTerrain()
    {
     
        // RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Tower tower;
        // TUTAJ WARNKI KASY
        // Achtung Bitte 
        if (Physics.Raycast(ray, out hit)) //check if the ray hit something
        {
            hitPosition = hit.point; //use this position for what you want to do
            //Debug.Log(hit.point);
        }

        
        if (Input.GetMouseButtonDown(0) && TowerChoser.ChosenTower != ChosenTower.None && towerTag!="")
        {
            // hitPosition.x += 0.5f;
            //  hitPosition.z -= 0.5f;
            if (MoneyAndScores.money < (float) TowerChoser.ChosenTower)
                return;
            
            for (int i = 0; i < terrainMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < terrainMatrix.GetLength(1); j++)
                {
                    Rect current = new Rect(i, j, 1, 1);
                    if (terrainMatrix[i, j].TerrainType == TerrainType.Normal && current.Contains(new Vector2(hitPosition.x + 0.5f, hitPosition.z + 0.5f))
                       /* && GameLogic.generator._maxX >= i && GameLogic.generator._maxZ >= j*/)
                    {
                        ////////////////////////////////////////////////

                       /////////////////////////////////////////////////
                        hitPosition.y = terrainMatrix[i, j].Height;  // ew. dodać ++
                        chosenTowerModel = Instantiate(GameObject.FindGameObjectWithTag(towerTag));
                        chosenTowerModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        chosenTowerModel.transform.position = new Vector3(
                            i,
                            terrainMatrix[i, j].Height,
                            j - 0.5f);

                        tower = gameObject.AddComponent<Tower>();
                        tower.Initialize(chosenTowerModel, chosenTowerModel.transform.position,
                 //   Tower.TowerType.Basic);
                      TowerChoser.ChosenTower);
                        // tower = new Tower(chosenTowerModel, chosenTowerModel.transform.position,
                        //Tower.TowerType.Basic);
                        GameLogic.towers.Add(tower);
                        MoneyAndScores.money -= (float) TowerChoser.ChosenTower;
                        terrainMatrix[i, j].TerrainType = TerrainType.Turrent;
                     //   towerTag = "";
                      //  GameLogic.towers.Add(new Tower(chosenTowerModel, chosenTowerModel.transform.position,
                    //   Tower.TowerType.Basic));    // (Tower.TowerType) Enum.Parse(typeof(Tower.TowerType), TowerChoser.ChosenTower.ToString())));
                    }
                }
            }

        }
    }

    public void ChooseTowerFromUI()
    {

        if (Camera.main == null)
            return;
        var v3 = Input.mousePosition;

        //   Vector3 T;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        //   T = Camera.main.ViewportToWorldPoint(Input.mousePosition);
        //    Debug.Log(v3);


        if (TowerChoser.ChosenTower != ChosenTower.None)
        {
            switch (TowerChoser.ChosenTower)
            {
                case ChosenTower.Basic:
                    if (TowerChoser.isSelected)
                    {
                      
                        //   chosenTowerModel = Instantiate(GameObject.FindGameObjectWithTag("BasicTowerTag"));
                        TowerChoser.isSelected = false;
                    }
                    // chosenTowerModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    //  chosenTowerModel.transform.position = v3;

                    break;

                case ChosenTower.CannonTower:
                    break;

                case ChosenTower.Fast:
                    break;

                case ChosenTower.Sniper:
                    break;

                case ChosenTower.None:
                    break;
            }
        }
    }
    // Use this for initialization
     void Awake ()
    {
       //chosenTower = ChosenTower.None;
	}

    // Update is called once per frame
   void Update ()
    {
        AttachTowerToTerrain();
  //      ChooseTowerFromUI();
	}
}
