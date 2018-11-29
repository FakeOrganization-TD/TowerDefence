using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

   public class Tower :MonoBehaviour
    {
      public  enum TowerType
        {
            Basic,
            CannonTower,
            Sniper,
            Fast
        }
       // public bool isPlaced=false;
        public float angle=0;
        public float damage;
        public float range;
        public float burstDamage;
        public float speed;
        public float rotation;
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
        if (model == null||target==null)
            return;
         missle = gameObject.AddComponent<Missle>();
         missle.Initalize( 
            Instantiate(GameObject.FindGameObjectWithTag("BasicMissleTag"),model.transform.position,Quaternion.identity), // klon sfery,ktora ma byc pociskiem
            model, // model wierzy
            target, // ref. do agenta
            missleType); // typ pocisku 
             
        }
    // obracanie wierza
   public void Rotate()
    {
        if(target!=null)
        {
            float numerator,denominator,height,sinus;
            height = target.ActualAgentModel.transform.position.y - model.transform.position.y;
            numerator = target.ActualAgentModel.transform.position.x - model.transform.position.x;

            denominator = (float)Math.Sqrt(Math.Pow(height, 2) + Math.Pow(numerator, 2));
            sinus = numerator / denominator;
            angle =  (float)Math.Sin(sinus);



            //  model.transform.Rotate(0, angle * ( 100 ), 0);
            model.transform.LookAt(new Vector3(target.ActualAgentModel.transform.position.x,
                0,target.ActualAgentModel.transform.position.z));

           
        }
    }

     public Tower(GameObject model, Vector3 Position, TowerType towerType) :base()
        {
            this.model = model;
          //  this.position =Position;
        this.target = null;
            switch (towerType)
            {
                case TowerType.Basic:
                    damage = 35;
                    range = 3;
                    burstDamage = 0;
                    speed = 1;
                    break;

                case TowerType.CannonTower:
                    damage = 65;
                    range = 5;
                    burstDamage = 20;
                    speed = 0.5f;
                    break;

                case TowerType.Fast:
                    damage = 30;
                    range = 4;
                    burstDamage = 0;
                    speed = 1.8f;
                    break;

                case TowerType.Sniper:
                    damage = 40;
                    range = 10;
                    burstDamage = 5;
                    speed = 0.8f;
                    break;
            }

        }

    public void Initialize(GameObject model, Vector3 Position, TowerType towerType)
    {
      //  this.isPlaced = true;
        this.model = model;
      //  this.position = Position;
        this.target = null;
        switch (towerType)
        {
            case TowerType.Basic:
                damage = 35;
                range = 5f;
                burstDamage = 0;
                speed = 1;
                missleType = Missle.MissleType.Basic;
                break;

            case TowerType.CannonTower:
                damage = 65;
                range = 5;
                burstDamage = 20;
                speed = 0.5f;
                break;

            case TowerType.Fast:
                damage = 30;
                range = 4;
                burstDamage = 0;
                speed = 1.8f;
                break;

            case TowerType.Sniper:
                damage = 40;
                range = 10;
                burstDamage = 5;
                speed = 0.8f;
                break;
        }
    }

    private void Update()
    {
        Rotate();
        Shoot();
    }
}
