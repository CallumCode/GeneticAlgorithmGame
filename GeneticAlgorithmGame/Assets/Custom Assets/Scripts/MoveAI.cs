using UnityEngine;

public class MoveAI : MonoBehaviour
{
	public float speed = 5;

	CharacterController characterController;

	Agent agent;


	float lastUpdate = 0;
	float updateRate = 0.1f;
	int index = 0;
	// Use this for initialization
	void Start()
	{
		characterController = GetComponent<CharacterController>();
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
		agent.KillSelf();
		Destroy(gameObject);
	}

	public void Win()
	{
		agent.SurivedGen();
		Destroy(gameObject);
	}


}