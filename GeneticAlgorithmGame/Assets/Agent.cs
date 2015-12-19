using UnityEngine;
using System.Collections;

public class Agent 
{

    public ArrayList componentsList;
    public const int numOfComponets = 6;

    int generation;
    Population population;


    // Use this for initialization
    public Agent(Population populationIn, int generationIn)
    {
        Debug.Log("Agent created " + Time.time);
        componentsList = new ArrayList();
        generation = generationIn;
        population = populationIn;

        CreateRandomComponents();
    }

    void CreateRandomComponents()
    {
        for(int i = 0; i < numOfComponets; i++)
        {
            AgentComponent agentComponent  = new AgentComponent();
            componentsList.Add(agentComponent);
        }
    }



    public void  KillSelf()
    {
        // ?
    }


    public void SurivedGen()
    {
        if(population != null)
        {
            population.ReturnTestedAgent(this, generation);
        }
        else
        {
            Debug.Log("Agent::SurivedGen: population is null");
            Debug.Break();
        }
    }

}
