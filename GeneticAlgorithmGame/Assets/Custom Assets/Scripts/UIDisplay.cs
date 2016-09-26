using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{

	public Text agentsInTesting;
	Text numOFGensText;
	public Population popToshow;
	public GameObject mainCanvasObeject;
	public GameObject TextPrefab;

	public Slider limitSlider;
	public Text limitText;
	public Text avgDistText;

	ArrayList listOfGensCounts;

	ArrayList listOfGenCountsInTesting;

	public Text AddTextToCanvas(string textString, GameObject canvasGameObject, float depth)
	{

		GameObject textObejct = Instantiate(TextPrefab, TextPrefab.transform.position, TextPrefab.transform.rotation) as GameObject;



		textObejct.transform.SetParent(canvasGameObject.transform);
		textObejct.name = textString;

		Text text = textObejct.GetComponent<Text>();
		Text shadow = textObejct.transform.GetChild(0).GetComponent<Text>();
		text.text = textString;
		shadow.text =  textString;

		text.transform.position = text.transform.position - Vector3.up * text.rectTransform.rect.height * depth * 1;



		return text;
	}

	// Use this for initialization
	void Start()
	{
		listOfGensCounts = new ArrayList();
		listOfGenCountsInTesting = new ArrayList();

		numOFGensText = AddTextToCanvas("numOFGensText", mainCanvasObeject, 0);
	}

	// Update is called once per frame
	void Update()
	{

		int numOfGens = popToshow.GetNumOfGens();
		numOFGensText.text = "Num of Gens " + numOfGens;
		Text shadow = numOFGensText.transform.GetChild(0).GetComponent<Text>();
		shadow.text = numOFGensText.text;
		UpdateGenDisplay();
	}


	void UpdateGenDisplay()
	{
		CreateNewGenTexts();

		int numOfgens = listOfGensCounts.Count;
		for (int i = 0; i < numOfgens; i++)
		{

			int agentsInTesting = 0;
			if (listOfGenCountsInTesting.Count > i) agentsInTesting = (int) listOfGenCountsInTesting[i];

			if(agentsInTesting < 0)
			{
				Debug.Break();
			}
			else
			{
				Text genCountText = (Text)listOfGensCounts[i];
				genCountText.text = "Gen " + i + " : Untested : " + popToshow.GetUnTestedCount(i) + " Tested : " + popToshow.GetTestedCount(i) + " Testing " + agentsInTesting;
				Text shadow = genCountText.transform.GetChild(0).GetComponent<Text>();
				shadow.text = genCountText.text;
			}
		}
	}

	void CreateNewGenTexts()
	{

		int numOfGens = popToshow.GetNumOfGens();
		if (listOfGensCounts.Count < numOfGens)
		{
			Text text = AddTextToCanvas("Gen " + listOfGensCounts.Count, mainCanvasObeject, listOfGensCounts.Count + 1);
			listOfGensCounts.Add(text);
		}
	}



	public void UpdateAgentsInTesting(int number , int gen , int total)
	{
		agentsInTesting.text = "Total Agents In Testing: " + total;
		
		bool added = false;
		while (!added)
		{
			if (listOfGenCountsInTesting.Count > gen)
			{
				int lastCount = (int)listOfGenCountsInTesting[gen];
				lastCount += number;

				if(lastCount < 0)
				{
					Debug.Break();
					lastCount = 0;
				}

				listOfGenCountsInTesting[gen] = lastCount;
				added = true;
			}
			else
			{
				Debug.Log("Testing New Gen " + gen);
				listOfGenCountsInTesting.Add(0);
			}
		}
		
		UpdateGenDisplay();
	}


	public void ChangeLimitSlider()
	{
		int limit = ((int)limitSlider.value);
		AIManager.Instance.ChangeAgentTestingLimit(limit);
		limitText.text = "Limit " + limit;
	}

	public void UpdateAverageDistance(float distance)
	{
		avgDistText.text =  "Avg distance: " + distance;
	}
}


