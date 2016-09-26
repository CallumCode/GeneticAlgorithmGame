using UnityEngine;
using System.Collections;


[System.Serializable]

public class AgentComponent
{

	public int ID;

	static int range = 20;


	public ArrayList forceList;
	static float forceRange = 100;

	// Use this for initialization
	public AgentComponent()
	{
		ID = Random.Range(-range, range);

		forceList = new ArrayList();

		for (int i = 0; i <  10 ; i++)
		{
			float force = Random.Range(-forceRange, forceRange);
			forceList.Add(force);
		}
	}


}
