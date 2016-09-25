using UnityEngine;
using System.Collections;

[System.Serializable]

public class AgentComponent
{

	public int ID;

	static int range = 20;
	// Use this for initialization
	public AgentComponent()
	{
		ID = Random.Range(-range, range);
	}


}
