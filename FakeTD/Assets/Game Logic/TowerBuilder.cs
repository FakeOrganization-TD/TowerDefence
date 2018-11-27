using Assets.Game_Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TowerBuilder : MonoBehaviour
{

    public  enum ChosenTower
    {
        Basic,
        CannonTower,
        Sniper,
        Fast,
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
               

                break;

            case ChosenTower.CannonTower:
                break;

            case ChosenTower.Fast:
                break;

            case ChosenTower.Sniper:
                break;

            case ChosenTower.None:
                Destroy(chosenTowerModel);
                break;

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
        //MousePosX = Input.mousePosition.x;
        //MousePosY = Input.mousePosition.y;
        //  MousePosZ = Input.mousePosition.z;
    
        if (Camera.main == null)
            return;
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);

      
        if (TowerChoser.ChosenTower != ChosenTower.None)
        {
            switch (TowerChoser.ChosenTower)
            {
                case ChosenTower.Basic:
                    if (TowerChoser.isSelected)
                    {
                        chosenTowerModel = Instantiate(GameObject.FindGameObjectWithTag("BasicTowerTag"));
                        TowerChoser.isSelected = false;
                    }
                    chosenTowerModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    chosenTowerModel.transform.position = v3;
                    
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
}
