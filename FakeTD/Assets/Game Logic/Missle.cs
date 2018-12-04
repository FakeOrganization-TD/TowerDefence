using UnityEngine;

public class Missle : MonoBehaviour
{
    public enum MissleType
    {
        Basic,
        Fast,
        Cannon,
        Sniper
    }

    public GameObject MissleModel;
    public Agent TargetAgent;
    public GameObject Mothertower;
    public float speed;
    public int damage;
    Vector3 origin;
    Vector3 currentPosition;
    Vector3 destination;

    public void Initalize(GameObject model, GameObject Mothertower, Agent Target, MissleType type)
    {
        this.TargetAgent = Target;
        this.MissleModel = model;
        this.Mothertower = Mothertower;
        origin = Mothertower.transform.position;
        currentPosition = origin;
        switch (type)
        {
            case MissleType.Basic:
                damage = Tower.BasicTowerDamage;
                // MissleModel.transform.localScale
                speed = Tower.BasicTowerSpeed; // szybkosc ataku: szybkosc agenta + offset 
                break;
            case MissleType.Fast:
                damage = Tower.FastTowerDamage;
                speed = Tower.FastTowerSpeed;
                break;
            case MissleType.Cannon:
                damage = Tower.CannonTowerDamage;
                speed = Tower.CannonTowerSpeed;
                break;
            case MissleType.Sniper:
                damage = Tower.SniperTowerDamage;
                speed = Tower.SniperTowerSpeed;
                break;
        }


    }







    // Funkcja,ktora jest odpowiedzialna za trafienie i poruszanie sie pocisku funkcja troche tylko zmieniona z agenta ( Move()  )
    public void HitTarget()
    {
       //Obliczanie "kroku" tj co jaka wartosc pocisk bedzie sie poruszac 
        float step = Time.deltaTime * speed;

        // Warunek konieczny, poniewaz agenci sa niszczeni po dotarciu do konca mapy. Bez tego sypie NullReference
        if (TargetAgent == null|| TargetAgent.ActualAgentModel == null)
        {
            Destroy(MissleModel);
            Destroy(this);
            return;
        }
           
        destination = TargetAgent.ActualAgentModel.transform.position;
        MissleModel.transform.position = Vector3.MoveTowards(currentPosition, destination, step);
        currentPosition = MissleModel.transform.position;

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

