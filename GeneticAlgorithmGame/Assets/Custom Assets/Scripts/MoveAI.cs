using UnityEngine;

public class MoveAI : MonoBehaviour
{
	public float speed = 20;

	CharacterController characterController;

	Agent agent;


	float lastUpdate = 0;
	public static float updateRate = 0.5f;
	int index = 0;


	public Vector3 startPos;
	public Vector3 starDir;

	// Use this for initialization
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		startPos = transform.position;
		starDir = transform.forward;
 	}

	// Update is called once per frame
	void Update()
	{
		characterController.SimpleMove(transform.forward * speed);

		UpdateAngle();
	}


	public void SetAgent(Agent agentIn)
	{
		agent = agentIn;
	}


	void UpdateAngle()
	{
		bool killSelf = false;

		if (Time.time > (lastUpdate + 1 / updateRate))
		{

			lastUpdate = Time.time;

			if (agent != null)
			{
				AgentComponent agentComponent = agent.GetComponet(index);
				if (agentComponent != null)
				{
					/// we will just use the as a angle for basic 
					float angle = agentComponent.ID;


					transform.Rotate(Vector3.up * angle);
					index++;
				}
				else
				{
					killSelf = true;
				}

			}
			else
			{
				killSelf = true;
			}

			if (killSelf)
			{
				KillSelf();
			}

		}

	}


	public void KillSelf()
	{
		AIManager.Instance.AgentFinishedTesting(agent.GetGenNumber());
		if (AIManager.Instance.UpdateAgentAvgDist(transform.position) > AIManager.Instance.GetAvgDist())
		{
			agent.KillSelf();
			Destroy(gameObject);
		}
		else
		{
			agent.SurivedGen();
			Destroy(gameObject);
		}
		
	}

	public void Win()
	{
		AIManager.Instance.AgentFinishedTesting(agent.GetGenNumber());
		AIManager.Instance.UpdateAgentAvgDist(transform.position);

		agent.SurivedGen();
		Destroy(gameObject);
	}

	public Agent GetAgent()
	{
		return agent;
	}
}