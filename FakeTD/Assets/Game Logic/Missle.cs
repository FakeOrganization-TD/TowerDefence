﻿using UnityEngine;

public class Missle : MonoBehaviour
{
    public enum MissleType
    {
        Basic,
    }

    public GameObject MissleModel;
    public Agent TargetAgent;
    public GameObject Mothertower;
    public float speed;
    public int damage;
    Vector3 origin;
    Vector3 destination;
    public void Initalize(GameObject model,GameObject Mothertower,Agent Target, MissleType type)
    {
        this.TargetAgent = Target;
        this.MissleModel = model;
        this.Mothertower = Mothertower;
        origin = Mothertower.transform.position;
        
        switch (type)
        {
            case MissleType.Basic:
                damage = 20;
                speed = 1f;
                break;
        }

      
    }
    public void HitTarget()
    {
       // if()
        float step = Time.deltaTime * speed;

        if (TargetAgent == null|| TargetAgent.ActualAgentModel ==null)
        {
            Destroy(MissleModel);
            Destroy(this);
            return;
        }
           
        destination = TargetAgent.ActualAgentModel.transform.position;
        MissleModel.transform.position = Vector3.MoveTowards(origin, destination, step);

        if(MissleModel.transform.position == destination)
        {
            TargetAgent.healthPoints -= damage;
            Destroy(MissleModel);
            Destroy(this);

        }
       
    }
  public void Update()
    {
        HitTarget();
    }

}

