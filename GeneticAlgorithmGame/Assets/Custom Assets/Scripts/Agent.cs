using UnityEngine;
using System.Collections;
using System;

public class Agent
{

	public ArrayList componentsList;
	public static int numOfComponets = 30;

	int generation;
	Population population;


	// Use this for initialization
	public Agent(AgentData agentIN , Population populationIn)
	{
		componentsList = new ArrayList(agentIN.components);
		generation = agentIN.generation;
		population = populationIn;
	}

public Agent(Population populationIn, int generationIn)
	{
	//	Debug.Log("Agent created " + Time.time);
		componentsList = new ArrayList();
		generation = generationIn;
		population = populationIn;

		CreateRandomComponents();
	}

	void CreateRandomComponents()
	{
		UnityEngine.Random.seed = UnityEngine.Random.Range(1, 1000);
		for (int i = 0; i < numOfComponets; i++)
		{
			AgentComponent agentComponent = new AgentComponent();
			componentsList.Add(agentComponent);
		}
	}

	public void KillSelf()
	{
		// ?
	}


	public void SurivedGen()
	{
		if (population != null)
		{
			population.ReturnTestedAgent(this, generation);
		}
		else
		{
			Debug.Log("Agent::SurivedGen: population is null");
			Debug.Break();
		}
	}

	public void IncrimentGen()
	{
		generation++;
	}


	public AgentComponent GetComponet(int index)
	{
		AgentComponent component = null;


		if (componentsList != null && componentsList.Count > index)
		{
			component = (AgentComponent)componentsList[index];
		}

		return component;
	}

	public int GetGenNumber()
	{
		return generation;
	}
}

