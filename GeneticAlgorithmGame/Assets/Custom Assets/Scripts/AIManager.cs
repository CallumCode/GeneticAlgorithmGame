using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIManager : Singleton<AIManager>
{

	public UIDisplay UIDisplay;
	public Transform spawnLine;


	public Transform WinLine;
	public GameObject MoveAIPrefab;

	public PopulationManager populationManager;

	float spawnRate = 25.0f;
	float lastSpawnTime = 0;

	float breedRate = 0.05f;
	float lastBreedTime = 0;


	int agentsInTesting = 0;
	int agentLimit = 200;

	float averageDistance = 0;
	float totalAgents = 0;

	protected AIManager() { }


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Time.time > (lastBreedTime + 1.0f / breedRate))
		{
			lastBreedTime = Time.time;
			populationManager.testPop.BreedAllTestedGen();
		}

		bool pastTime = Time.time > (lastSpawnTime + 1.0f / spawnRate);
		bool lowerThanLimit = agentsInTesting < agentLimit;
		if (pastTime && lowerThanLimit)
		{
			lastSpawnTime = Time.time;

			Agent agent = populationManager.GetAgent();

			Random.seed = Random.Range(0, 999999);

			float randomOoffset = Random.Range( -0.5f * spawnLine.transform.localScale.x , 0.5f * spawnLine.transform.localScale.x );

			GameObject moveAIObject = Instantiate(MoveAIPrefab, spawnLine.position + randomOoffset * spawnLine.right, spawnLine.rotation) as GameObject;
			moveAIObject.GetComponent<MoveAI>().SetAgent(agent);
			moveAIObject.transform.parent = transform;
			moveAIObject.name = "MoveAI Gen "+  agent.GetGenNumber();

			agentsInTesting++;

			UIDisplay.UpdateAgentsInTesting( 1 , agent.GetGenNumber(), agentsInTesting);
		}

	}


	public void AgentFinishedTesting(int gen)
	{
		agentsInTesting--;
		UIDisplay.UpdateAgentsInTesting(-1 , gen, agentsInTesting);


		UIDisplay.UpdateAverageDistance(averageDistance);

	}

	public float UpdateAgentAvgDist(Vector3 position)
	{
		//average = average + ((value - average) / nbValues)

		float newDistance = WinLine.position.z - position.z;
		if (newDistance < 0) newDistance = 0;

		totalAgents++;
		averageDistance = averageDistance + ((newDistance - averageDistance) / totalAgents);


		return newDistance;
	}

	public void ChangeAgentTestingLimit( int limit)
	{
		agentLimit = limit; 

	}

	public float GetAvgDist()
	{
		return averageDistance;
	}
}
