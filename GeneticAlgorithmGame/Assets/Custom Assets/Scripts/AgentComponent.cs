using UnityEngine;
using System.Collections;
using System;

[System.Serializable]

public class AgentComponent
{

	public int ID;

	static int range = 20;


	public ArrayList forceList;
	static float forceRange = 100;
	int numOfForces = 10;

	float muateRange = 0.5f;

	// Use this for initialization
	public AgentComponent()
	{
		ID = UnityEngine.Random.Range(-range, range);

		forceList = new ArrayList();

		for (int i = 0; i <  10 ; i++)
		{
			float force = UnityEngine.Random.Range(-forceRange, forceRange);
			forceList.Add(force);
		}
	}

	public AgentComponent(AgentComponent AgentComponentToCopy)
	{

		ID = AgentComponentToCopy.ID;
		forceList = new ArrayList();

		for (int i = 0; i < numOfForces; i++)
		{
			float force = (float)AgentComponentToCopy.forceList[i];
			forceList.Add(force);
		}

	}

	 public void Mutate()
	{

		int forceCount = forceList.Count;
		float mutation = 0;
		float newForce = 0;
		float oldForce = 0;

		for (int i = 0; i < forceCount;	i++)
		{
			mutation = UnityEngine.Random.Range(-muateRange , muateRange);
			oldForce = (float)forceList[i];
			newForce = mutation + oldForce;
			newForce = Mathf.Clamp(newForce , - forceRange, forceRange);
			forceList[i] = newForce;
 		}

	}
}
