using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class WaveController : MonoBehaviour
  {
    public  bool randomWaveStyle = false;
    // kazdy element listy to jedna fala
    // na fale sklada sie x agentow okreslonego typu
    public  List<List<Agent>> waves;
    public  int currentWave;
    public  Agent currentAgent;
    public  float interval;
    public float hpSum;

    public WaveController(bool randomWaveStyle=false)
    {
        this.randomWaveStyle = randomWaveStyle;
        waves = new List<List<Agent>>();
    }
   
    private void MakeAgents()
    {
        // ilos fal w sumie
        for (int i = 0; i < 5; i++)
        {
            var tempList = new List<Agent>();

            for (int j = 0; j < 8; j++)
            {
                

            }
            waves.Add(tempList);
           

        }
    }
    
    private void MakeAgentsRandom()
    {

    }

  }



                                 
                                 