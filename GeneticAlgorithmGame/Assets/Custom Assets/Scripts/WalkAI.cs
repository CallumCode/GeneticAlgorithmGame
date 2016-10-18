using UnityEngine;
using System.Collections;

public class WalkAI : MonoBehaviour
{
	Agent Agent;

	public WalkController WalkController;

	public GameObject Body;

	float lastUpdate = 0;
	public static float updateRate = 2.0f;
	int index = 0;

	ArrayList ForceList = null;

	int walkCycles = 0;
	int minWalkCycles = 5;

	bool ending = false;
	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		WalkController.DoForceTick(ForceList);


		if(walkCycles >= minWalkCycles)
		{

			float avgDist =  AIManager.Instance.GetAvgDist();
			float currentDistance = AIManager.Instance.UpdateAgentAvgDist(Body.transform.position);

			if( currentDistance < avgDist)
			{
				Succeed(false);
			}
			else
			{
				Failed(false);
			}

			return;
		}


		if (Time.time > (lastUpdate + 1 / updateRate))
		{

			lastUpdate = Time.time;

			if (Agent != null)
			{
				AgentComponent agentComponent = Agent.GetComponet(index);
				if (agentComponent != null)
				{
					ForceList = agentComponent.forceList;

					index++;
				}

				if (index >= Agent.numOfComponets)
				{
					index = 0;
					walkCycles++;
				}

			}
		}
	}

	

	void DestroySelf(bool recordDist)
	{
		if (recordDist) AIManager.Instance.UpdateAgentAvgDist(transform.position);
		AIManager.Instance.AgentFinishedTesting(Agent.GetGenNumber());
		Destroy(gameObject);
	}


	////////////////////////////////////////////////////////////////
	public void SetAgent(Agent AgentIn)
	{
		Agent = AgentIn;
	}


	public Agent GetAgent()
	{
		return Agent;
	}

	public void Succeed(bool recordDist = true)
	{
		if (ending == false)
		{
			ending = true;
			Agent.SurivedGen();
			DestroySelf(recordDist);
		}
	}

	public void Failed(bool recordDist = true)
	{
		if (ending == false)
		{
			ending = true;
			DestroySelf(recordDist);
		}
	}
}
