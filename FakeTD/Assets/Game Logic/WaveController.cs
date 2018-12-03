using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class WaveController
{
    public bool randomWaveStyle = false;
    // kazdy element listy to jedna fala
    // na fale sklada sie x agentow okreslonego typu
    public List<List<Agent.AgentType>> waves;
    public int currentWave = 0;
    public Agent currentAgent;
    public float interval;
    public float hpSum = 300;
    public float hpWaveInterval = 100;
    public int wavesCount = 10;


    public WaveController(bool randomWaveStyle = false)
    {
        this.randomWaveStyle = randomWaveStyle;
        waves = new List<List<Agent.AgentType>>();

        if (randomWaveStyle)
        {
            for (int i = 0; i < wavesCount; i++)
            {
                waves.Add(MakeAgentsRandom());
            }
        }
    }

    private void MakeAgents()
    {
        // ilos fal w sumie
        for (int i = 0; i < 5; i++)
        {
            var wave = MakeAgentsRandom();

            for (int j = 0; j < 8; j++)
            {

            }
            waves.Add(wave);

        }
    }

    private List<Agent.AgentType> MakeAgentsRandom()
    {
        var wave = new List<Agent.AgentType>();
        int sumOfHP = 0;
        while (sumOfHP <= hpSum)
        {
            var enemy = GetRandomEneny();
            sumOfHP += (int)enemy;
            wave.Add(enemy);
        }
        hpSum += hpWaveInterval;
        wave = wave.OrderBy(enemy => (int)enemy).ToList();
        return wave;
    }

    private Agent.AgentType GetRandomEneny()
    {
        Agent.AgentType agentType;
        var propability = Random.Range(0.00f, 1f);

        if (propability <= 0.4f)
            agentType = Agent.AgentType.Normal;
        else if (propability > 0.4f && propability <= 0.7f)
            agentType = Agent.AgentType.Fast;
        else
            agentType = Agent.AgentType.Tank;

        return agentType;
    }

}




