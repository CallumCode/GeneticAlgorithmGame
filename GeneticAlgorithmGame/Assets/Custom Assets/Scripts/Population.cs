using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour
{

	public ArrayList untestedPools;
	public ArrayList testedPools;


	public int randomStartSize = 10;
	public int randomRefilSize = 10;
	// Use this for initialization
	void Start()
	{
		untestedPools = new ArrayList();
		testedPools = new ArrayList();
		CreateRandomFirstPool(randomStartSize);


	}

	// Update is called once per frame
	void Update()
	{

	}


	void Breed(ref Agent agentA, ref Agent agentB, int gen)
	{
		CheckGen(untestedPools, gen + 1);
		CheckGen(testedPools, gen + 1);

		////

		SwapBreed(agentA, agentB);
		////
		ArrayList untestGenPool = (ArrayList)untestedPools[gen + 1];

		agentA.Mutate();
		agentA.Mutate();

		agentA.IncrimentGen();
		agentB.IncrimentGen();

		untestGenPool.Add(agentA);
		untestGenPool.Add(agentB);
	}
	void CheckGen(ArrayList genPools, int gen)
	{
		if (untestedPools.Count == gen)
		{
			ArrayList newGeneration = new ArrayList();
			genPools.Add(newGeneration);
		}

		if (untestedPools.Count < gen)
		{
			Debug.Log("pool too smal");
			Debug.Break();
		}

	}
	void SwapBreed(Agent agentA, Agent agentB)
	{

		int numToSwap = (int) ( Agent.numOfComponets * 0.25f);
		for(int i = 0; i < numToSwap; i++)
		{
			int randIndex = UnityEngine.Random.Range(0, Agent.numOfComponets);
			SwapDNA(agentA, agentB, randIndex);
		}
	}

	void SwapDNA(Agent agentA, Agent agentB, int index)
	{
		AgentComponent cachA = (AgentComponent)agentA.componentsList[index];

		agentA.componentsList[index] = agentB.componentsList[index];

		agentB.componentsList[index] = cachA;

	}
	void CreateRandomFirstPool(int howMany)
	{
		CheckGen(untestedPools, 0);
		CheckGen(testedPools, 0);

		ArrayList firstGen = (ArrayList)untestedPools[0];
		for (int i = 0; i < howMany; i++)
		{
			Agent agent = new Agent(this, 0);
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

		int		count = gen.Count;

		if (count > 0)
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

		Agent agent = GetAgent(untestGenLatest);

		return agent;
	}

	public bool TestAgent(ref Agent agent)
	{
		bool kill = true;


		float total = 0;
		int count = agent.componentsList.Count;
		for (int index = 0; index < count; index++)
		{
			AgentComponent agentComponent = (AgentComponent)agent.componentsList[0];

			total += agentComponent.ID;
		}

		float avg = total / count;

		Debug.Log("avg test " + avg);

		if (avg > 60)
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
	public void ReturnTestedAgent(Agent agent, int gen)
	{
		ArrayList testedGen;

		CheckGen(testedPools, gen);

		if (testedPools.Count > gen)
		{
			testedGen = (ArrayList)testedPools[gen];
			testedGen.Add(agent);
		}
		else if (gen == testedPools.Count)
		{
			testedPools.Add(new ArrayList());
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
			ArrayList untestedGen = (ArrayList)untestedPools[genIndex];

			while (testedGen.Count > 1)
			{
				Agent ParentA = (Agent)testedGen[0];
				Agent ParentB = (Agent)testedGen[1];

				Agent ChildA = new Agent(ParentA);
				Agent ChildB = new Agent(ParentB);

				testedGen.Remove(ParentA);
				testedGen.Remove(ParentB);
				untestedGen.Add(ParentA);
				untestedGen.Add(ParentB);

				Breed(ref ChildA, ref ChildB, genIndex); // dont need to be ref any more but will leave for now

			}// end while ran out of tested agents


		}// end loop of all tested gens

		
		AIManager.Instance.ResetAvgDist();
	}


	public int GetNumOfGens()
	{
		int num = untestedPools.Count;
		/*
        if (num != testedPools.Count)
        {
            Debug.Log("test and untested counts not match");            
            Debug.Break();
        }*/

		return num;
	}

	public int GetTestedCount(int gen)
	{
		int count = 0;
		if (testedPools != null && gen < testedPools.Count)
		{

			ArrayList testedGenN = (ArrayList)testedPools[gen];

			if (testedGenN != null)
			{
				count = testedGenN.Count;
			}
		}
		return count;
	}

	public int GetUnTestedCount(int gen)
	{
		int count = -1;
		if (untestedPools != null && gen < untestedPools.Count)
		{

			ArrayList unTestedGenN = (ArrayList)untestedPools[gen];

			if (unTestedGenN != null)
			{
				count = unTestedGenN.Count;
			}

		}
		return count;
	}

}
