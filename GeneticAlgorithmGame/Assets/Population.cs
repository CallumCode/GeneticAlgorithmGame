using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour
{

    ArrayList untestedPools;
    ArrayList testedPools;

	// Use this for initialization
	void Start ()
    {
        untestedPools = new ArrayList();
        testedPools = new ArrayList();

        CreateFirstPool(10);

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
     
    
    void Breed(Agent agentA , Agent  agentB , int gen)
    {
        CheckGen( untestedPools , gen);
        ////
 
        SwapDNA(agentA, agentB, 0);
        ////
        ArrayList untestGenPool = ( ArrayList ) untestedPools[gen + 1 ];

        untestGenPool.Add(agentA);
        untestGenPool.Add(agentB);
    }

    void CheckGen(ArrayList genPools , int gen)
    {
        if (untestedPools.Count == gen)
        {
            ArrayList newGeneration = new ArrayList();
            genPools.Add(newGeneration);
        }

        if (untestedPools.Count < gen )
        {
            Debug.Log("pool too smal");
            Debug.Break();
        }
    
}


    void SwapBreed(Agent agentA, Agent agentB)
    {
        // TODO decide how many to swap

        int randIndex = Random.Range(0, Agent.numOfComponets);
        SwapDNA(agentA, agentB, randIndex);
    }

    void SwapDNA( Agent agentA, Agent agentB, int index)
    {
        AgentComponent cachA = (AgentComponent) agentA.componentsList[index];

        agentA.componentsList[index] = agentB.componentsList[index];

        agentB.componentsList[index] = cachA;

    }



    void CreateFirstPool(int howMany)
    {
        CheckGen(untestedPools, 0);
        for(int i = 0; i < howMany; i ++)
        {
            Agent agent = new Agent();
            untestedPools.Add(agent);
        }
    }

}


