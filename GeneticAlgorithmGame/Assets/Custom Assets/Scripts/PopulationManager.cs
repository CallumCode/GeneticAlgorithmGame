using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PopulationManager : MonoBehaviour
{


	public Population testPop;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			testPop.BreedAllTestedGen();
		}
	}

	public Agent GetAgent()
	{
		return testPop.GetLatestNonTestedAgent();
	}


	public void Save()
	{
		Debug.Log("Save");
			
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/PopulationData.dat" );

		PopulationData data = new PopulationData(testPop.testedPools, testPop.untestedPools);

		bf.Serialize(file, data);
		file.Close();

	}

	public void Load()
	{
		Debug.Log("Load");

		if(File.Exists(Application.persistentDataPath + "/PopulationData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/PopulationData.dat", FileMode.Open);
			PopulationData data = (PopulationData)bf.Deserialize(file);

			ArrayList untestedGenerations = new ArrayList();
			foreach (ArrayList gen in data.untestedPools)
			{
				ArrayList newGen = new ArrayList();
				foreach (AgentData agentData in gen)
				{
					Agent agent = new Agent(agentData, testPop );
					newGen.Add(agent);
				}

				untestedGenerations.Add(newGen);
			}


			ArrayList testedGenerations = new ArrayList();
			foreach (ArrayList gen in data.testedPools)
			{
				ArrayList newGen = new ArrayList();
				foreach (AgentData agentData in gen)
				{
					Agent agent = new Agent(agentData, testPop);
					newGen.Add(agent);
				}

				testedGenerations.Add(newGen);

			}


			file.Close();

			testPop.testedPools = new ArrayList(testedGenerations);
			testPop.untestedPools = new ArrayList(untestedGenerations);
		}
	}
	 
}

[Serializable]
class PopulationData
{
 	public ArrayList untestedPools;
	public ArrayList testedPools;

	public PopulationData(ArrayList tested, ArrayList untested)
	{
		testedPools = new ArrayList();
		untestedPools = new ArrayList();
		foreach (ArrayList gen in tested)
		{

			ArrayList newGen = new ArrayList();
			foreach(Agent agent in gen)
			{
				AgentData newAgent = new AgentData(agent);
				newGen.Add(newAgent);
			}
			
			testedPools.Add(newGen);
		}


		foreach (ArrayList gen in untested)
		{

			ArrayList newGen = new ArrayList();
			foreach (Agent agent in gen)
			{
				AgentData newAgent = new AgentData(agent);
				newGen.Add(newAgent);
			}

			untestedPools.Add(newGen);
		}


	}
}


[Serializable]
public class AgentData
{
	public int generation;

	public ArrayList components;
	public AgentData(Agent agent)
	{
		generation = agent.GetGenNumber();

		components = new ArrayList(agent.componentsList);
 	}
}