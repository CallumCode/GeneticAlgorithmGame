using UnityEngine;
using System.Collections;

public class Agent 
{


    public ArrayList componentsList;

    public const int numOfComponets = 6;

    // Use this for initialization
    public Agent()
    {
        componentsList = new ArrayList();

    }

    void CreateRandomComponents()
    {
        for(int i = 0; i < numOfComponets; i++)
        {
            AgentComponent agentComponent  = new AgentComponent();
            componentsList.Add(agentComponent);
        }
    }

	 
}
