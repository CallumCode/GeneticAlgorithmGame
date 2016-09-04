using UnityEngine;
using System.Collections;

[System.Serializable]

public class AgentComponent
{

	public int ID;

	// Use this for initialization
	public AgentComponent()
	{
		ID = Random.Range(0, 100);
	}


}
