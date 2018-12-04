using System;
using UnityEngine;

public class Tower : MonoBehaviour
{


    TowerBuilder.ChosenTower type;
    // public bool isPlaced=false;
    public float angle = 0;
    public float damage;
    public float range;
    public float burstDamage;
    public float reload;
    public float cooldown;
    public float rotation;
    public float cost;

    public static int BasicTowerDamage = 50;
    public static int FastTowerDamage = 30;
    public static int CannonTowerDamage = 100;
    public static int SniperTowerDamage = 60;

    public static int SniperTowerSpeed = 30;
    public static int CannonTowerSpeed = 7;
    public static int BasicTowerSpeed = 12;
    public static int FastTowerSpeed = 20;

    //MOdel wierzy
    public GameObject model;
    public Agent target;
    public Missle missle;
    public Missle.MissleType missleType;
    // namierza
    public void PinPoint()
    {

    }
    // strzelanie
    public void Shoot()
    {


        if (model == null || target == null)
        {
            return;
        }

        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            //missle = gameObject.AddComponent<Missle>();
            //missle.Initalize(
            //   Instantiate(GameObject.FindGameObjectWithTag("BasicMissleTag"), model.transform.position, Quaternion.identity), // klon sfery,ktora ma byc pociskiem
            //   model, // model wierzy
            //   target, // ref. do agenta
            //   missleType,(int)damage,12); // typ pocisku 
            //cooldown = reload;

            missle = gameObject.AddComponent<Missle>();
            missle.Initalize(
               Instantiate(GameObject.FindGameObjectWithTag("BasicMissleTag"), model.transform.position, Quaternion.identity), // klon sfery,ktora ma byc pociskiem
               model, // model wierzy
               target, // ref. do agenta
               missleType); // typ pocisku 
            cooldown = reload;
        }
    }
    // obracanie wierza
    public void Rotate()
    {
        if (target != null)
        {
            float numerator, denominator, height, sinus;
            height = target.ActualAgentModel.transform.position.y - model.transform.position.y;
            numerator = target.ActualAgentModel.transform.position.x - model.transform.position.x;
          
            denominator = (float) Math.Sqrt(Math.Pow(height, 2) + Math.Pow(numerator, 2));
            sinus = numerator / denominator;
            angle = (float) Math.Sin(sinus);



            ////  model.transform.Rotate(0, angle * ( 100 ), 0);
            //if (czyUj)
            //    angle *= -1;
            ////model.transform.Rotate(new Vector3(0, 1, 0), angle);
            ////model.transform.LookAt(target.ActualAgentModel.transform);
            ////model.transform.rotation = Quaternion.Euler(0, model.transform.rotation.y, 0);

            //var lookPos = target.transform.position - model.transform.position;
            //lookPos.y = 0;
            //var rotation = Quaternion.LookRotation(-lookPos);
            //model.transform.rotation = Quaternion.Slerp(model.transform.rotation, rotation, Time.deltaTime * 1);
            model.transform.LookAt(new Vector3(
                target.ActualAgentModel.transform.position.x,
                target.ActualAgentModel.transform.position.y,
                target.ActualAgentModel.transform.position.z));
            //target.ActualAgentModel.transform.position.z));


        }
    }

   

    

    public void Initialize(GameObject model, Vector3 Position, TowerBuilder.ChosenTower towerType)
    {
        //  this.isPlaced = true;
        this.model = model;
        //  this.position = Position;
        target = null;
        switch (towerType)
        {
            case TowerBuilder.ChosenTower.Basic:
                type = TowerBuilder.ChosenTower.Basic;
                missleType = Missle.MissleType.Basic;
                
                range = 3;
                burstDamage = 0;
                reload = 1.2f;
                cost = (int) type;
                break;

            case TowerBuilder.ChosenTower.CannonTower:
            
                type = TowerBuilder.ChosenTower.CannonTower;
                missleType = Missle.MissleType.Cannon;
                range = 5;
                burstDamage = 20;
                reload = 2f;
                cost = (int) type;
                break;

            case TowerBuilder.ChosenTower.Fast:
                type = TowerBuilder.ChosenTower.Fast;
                missleType = Missle.MissleType.Fast;
               
                range = 4;
                burstDamage = 0;
                reload = 0.5f;
                cost = (int) type;
                break;

            case TowerBuilder.ChosenTower.Sniper:
                type = TowerBuilder.ChosenTower.Sniper;
                missleType = Missle.MissleType.Sniper;
          
                range = 10;
                burstDamage = 5;
                reload = 1.5f;
                cost = (int) type;
                break;
        }
    }

    private void Update()
    {
        Rotate();
        Shoot();
    }
}
