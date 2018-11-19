using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour {

   public enum ChosenTower
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
    GameObject chosenTowerModel;
    
    ChosenTower chosenTower;
    
    //public TowerBuilder(GameObject basic, GameObject cannonTower, GameObject sniper, GameObject fast, ChosenTower chosenTower)
    //{
    //    Basic = basic;
    //    CannonTower = cannonTower;
    //    Sniper = sniper;
    //    Fast = fast;
    //    this.chosenTower = chosenTower;
    //}

    
   public void ChoseTower( string chosenTower)
    {
        this.chosenTower=(ChosenTower)Enum.Parse(typeof(ChosenTower), chosenTower, true);

        switch (this.chosenTower)
        {
            case ChosenTower.Basic: Debug.Log("IT CANONS !");
                chosenTowerModel = Instantiate( GameObject.FindGameObjectWithTag("BasicTowerTag"));

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
    void Start ()
    {
        chosenTower = ChosenTower.None;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MousePosX = Input.mousePosition.x;
        MousePosY = Input.mousePosition.y;
        MousePosZ = Input.mousePosition.z;
        
        if (chosenTower != ChosenTower.None)
        {
            switch (chosenTower)
            {
                case ChosenTower.Basic:
                    chosenTowerModel.transform.position = new Vector3(MousePosX, MousePosY, MousePosZ);  

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
