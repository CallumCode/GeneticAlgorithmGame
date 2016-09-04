using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour
{


	public Transform spawnLine;

	public GameObject MoveAIPrefab;

	public PopulationManager populationManager;

	float spawnRate = 3;
	float lastSpawnTime = 0;

	float breedRate = 0.01f;
	float lastBreedTime = 0;

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


		if (Time.time > (lastSpawnTime + 1.0f / spawnRate))
		{
			lastSpawnTime = Time.time;

			Agent agent = populationManager.GetAgent();

			float randomOoffset = (0.5f - Random.value) * spawnLine.transform.localScale.x;

			GameObject moveAIObject = Instantiate(MoveAIPrefab, spawnLine.position + randomOoffset * spawnLine.right, spawnLine.rotation) as GameObject;
			moveAIObject.GetComponent<MoveAI>().SetAgent(agent);

		}

	}
}
