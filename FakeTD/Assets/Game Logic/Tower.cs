using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Game_Logic
{
    class Tower : MonoBehaviour
    {
        enum TowerType
        {
            Basic,
            CannonTower,
            Sniper,
            Fast
        }

        public float damage;
        public float range;
        public float burstDamage;
        public float speed;
        public float rotation;
        public Vector3 targetPoint;
        public Vector3 position;
        public GameObject model;

        // namierza
        public void PinPoint()
        {

        }
        // strzela
        public void Shoot()
        {

        }
        Tower(GameObject model, Vector3 Position, TowerType towerType)
        {
            this.model = model;
            this.position =Position;

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
    }
}
