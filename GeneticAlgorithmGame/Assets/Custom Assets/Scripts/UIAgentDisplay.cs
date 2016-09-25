using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAgentDisplay : MonoBehaviour
{
	public Text agentDisplay;

	Agent agentToDsiplay = null;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}


	public void SetAgent(Agent agent)
	{
		agentToDsiplay = agent;
		UpdateDisplay();
	}


	void UpdateDisplay()
	{
		if (agentToDsiplay != null)
		{
			string componentsDesc = "Componets: ";
			for (int i = 0; i < Agent.numOfComponets; i++)
			{
				AgentComponent agentComponent = agentToDsiplay.GetComponet(i);
				componentsDesc += "\n" + agentComponent.ID;
			}

			agentDisplay.text = componentsDesc;
		}
		else
		{
			agentDisplay.text = "";
		}

	}
	
}
