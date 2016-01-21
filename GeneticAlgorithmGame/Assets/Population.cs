﻿using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour
{

    ArrayList untestedPools;
    ArrayList testedPools;


    public int randomStartSize = 10;
    public int randomRefilSize = 10;
    // Use this for initialization
    void Start ()
    {
        untestedPools = new ArrayList();
        testedPools = new ArrayList();
        CreateRandomFirstPool(randomStartSize);


    }

    // Update is called once per frame
    void Update ()
    {
	
	}
     
    
    void Breed(ref Agent agentA , ref Agent agentB , int gen)
    {
        CheckGen( untestedPools , gen + 1);
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
    void CreateRandomFirstPool(int howMany)
    {
        CheckGen(untestedPools, 0);
        ArrayList firstGen = (ArrayList)untestedPools[0];
        for(int i = 0; i < howMany; i ++)
        {
            Agent agent = new Agent(this , 0);
            firstGen.Add(agent);
        }
    }
      /// <summary>
    /// loops through each untested gen pool looking for latest one that isn ot empty
    /// </summary>
    /// <returns>non emmpty latest untested gen pool</returns>
    ArrayList GetNewestUnTestedGen()
    {
        ArrayList gen = null;


        int numOfGens = untestedPools.Count;

        if (numOfGens > 0)
        {
            for (int genIndex = numOfGens - 1; genIndex >= 0; genIndex--) // work backwords check from latested gen
            {
                ArrayList genUntested = (ArrayList)untestedPools[genIndex];

                if (genUntested.Count > 0)
                {
                    gen = genUntested;
                    break; // breaking when finding non empty gen;
                }
            }
        }
        else
        {
            Debug.Log("NO UNTested Gens");
            Debug.Break();
        }
        

        if (gen == null)
        {
            CreateRandomFirstPool(randomRefilSize);
            gen = (ArrayList)untestedPools[0];
        }         

         return gen;
    }
    /// <summary>
    ///    removes agent from gen and returns it
    /// </summary>
    /// <param name="gen"></param>
    /// <returns>Returns agent last in generation </returns>
    Agent GetAgent(ArrayList gen)
    {
        Agent agent = null;

        int count = gen.Count;

        if(count > 0)
        {
            agent = (Agent)gen[count - 1];
            gen.RemoveAt(count - 1);
        }
        else
        {
            Debug.Log("GetLatestAgent:  Gen is Empty ");
            Debug.Break();
        }


        return agent;
    }
    public Agent GetLatestNonTestedAgent()
    {

        ArrayList untestGenLatest = GetNewestUnTestedGen();

        Agent agent =  GetAgent(untestGenLatest);

        return agent;
    }

    public bool TestAgent(ref Agent agent)
    {
        bool kill = true;


        float total = 0;
        int count = agent.componentsList.Count;
        for(int index = 0; index < count; index++)
        {
            AgentComponent agentComponent = (AgentComponent)agent.componentsList[0];

            total += agentComponent.ID;
        }
        
        float avg = total / count;

        Debug.Log("avg test " + avg);

        if(avg > 60)
        {
            kill = false;
            agent.SurivedGen();
        }
            else
        {
            agent.KillSelf();
        }

        agent = null;
        return kill;
    }
    public void ReturnTestedAgent(Agent agent , int gen)
    {
        ArrayList testedGen;

        if (testedPools.Count > gen  )
        {
            testedGen = (ArrayList)testedPools[gen];
            testedGen.Add(agent);
        }
        else if(gen == testedPools.Count)
        {
            testedPools.Add( new ArrayList() );
            testedGen = (ArrayList)testedPools[gen];
            testedGen.Add(agent);
        }
        else
        {
            
            Debug.Log("Population::ReturnTestedAgent gen too high");
        }
      }


    /// <summary>
    /// loop through test gen and breed them
    /// 
    /// 
    /// /// </summary>
    public void BreedAllTestedGen()
    {
        int numOfTestedGen = testedPools.Count;

        for (int genIndex = 0; genIndex < numOfTestedGen; genIndex++)
        {
            ArrayList testedGen = (ArrayList)testedPools[genIndex];

            while (testedGen.Count > 1)
            {
                Agent A = (Agent)testedGen[0];
                Agent B = (Agent)testedGen[1];

                testedGen.Remove(A);
                testedGen.Remove(B);

                Breed(ref  A, ref  B, genIndex);
                
            }// end while ran out of tested agents


        }// end loop of all tested gens

    }
}


