using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIManager : Singleton<AIManager>
{

	public UIDisplay UIDisplay;
	public Transform spawnLine;


	public Transform WinLine;
	public GameObject MoveAIPrefab;

	public GameObject WalkAIPrefab;

	public PopulationManager populationManager;

	public SpawnerScaner spawnerScaner;

	float spawnRate = 5.0f;
	float lastSpawnTime = 0;

	float breedTime = 20.0f;
	float lastBreedTime = 0;


	 int agentsInTesting = 0;
	int agentLimit = 1;

	ArrayList pastDistances;

	float averageDistance = 100000;
	//float totalAgents = 0;

	protected AIManager() { }


	// Use this for initialization
	void Start()
	{
		pastDistances = new ArrayList();
	}

	// Update is called once per frame
	void Update()
	{

		if (Time.time > (lastBreedTime + breedTime))
		{
			lastBreedTime = Time.time;
			populationManager.testPop.BreedAllTestedGen();
		}

		bool pastTime = Time.time > (lastSpawnTime + 1.0f / spawnRate);
		bool lowerThanLimit = agentsInTesting < Mathf.Abs(agentLimit);
		if (pastTime && lowerThanLimit && !spawnerScaner.IsBlocked())
		{
			lastSpawnTime = Time.time;

			Agent agent = populationManager.GetAgent();

			Random.seed = Random.Range(0, 999999);

			//SpawnMoveAI(agent);

			SpawnWalkAI(agent);

			agentsInTesting++;

			UIDisplay.UpdateAgentsInTesting( 1 , agent.GetGenNumber(), agentsInTesting);
		}

	}

	void SpawnMoveAI(Agent Agent)
	{
		float randomOoffset = Random.Range(-0.5f * spawnLine.transform.localScale.x, 0.5f * spawnLine.transform.localScale.x);

		GameObject moveAIObject = Instantiate(MoveAIPrefab, spawnLine.position + randomOoffset * spawnLine.right, spawnLine.rotation) as GameObject;
		moveAIObject.GetComponent<MoveAI>().SetAgent(Agent);
		moveAIObject.transform.parent = transform;
		moveAIObject.name = "MoveAI Gen " + Agent.GetGenNumber();
	}

	void SpawnWalkAI(Agent Agent)
	{

		GameObject walkAIObject = Instantiate(WalkAIPrefab, spawnerScaner.GetPos() , spawnLine.rotation) as GameObject;
		walkAIObject.GetComponent<WalkAI>().SetAgent(Agent);
		walkAIObject.transform.parent = transform;
		walkAIObject.name = "WalkAI Gen " + Agent.GetGenNumber();
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
		/*
		float newDistance = WinLine.position.z - position.z;
		if (newDistance < 0) newDistance = 0;

		averageDistance = averageDistance + ((newDistance - averageDistance) / totalAgents);
		*/

		float newDistance = WinLine.position.z - position.z;
		if (newDistance < 0) newDistance = 0;


		pastDistances.Add(newDistance);
		pastDistances.Sort();

		float count = pastDistances.Count;
		int mid = (int)(count / 2.0);

		averageDistance = (float) pastDistances[mid];

		UIDisplay.UpdateAverageDistance(averageDistance);

		
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

	public void ResetAvgDist()
	{
		averageDistance = 0;
		pastDistances.Clear();
	}
}
