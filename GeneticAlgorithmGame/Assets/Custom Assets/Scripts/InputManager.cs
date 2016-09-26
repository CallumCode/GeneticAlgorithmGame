using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{

	public UIAgentDisplay UIAgentDisplay;
	public DrawAIPath DrawAIPath;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
			//	Debug.Log(hit.collider.tag);

				if(hit.collider.CompareTag("MoveAI") )
				{
					MoveAI moveAI  = hit.collider.GetComponent<MoveAI>();

					UIAgentDisplay.SetAgent(moveAI.GetAgent());
					DrawAIPath.SetMoveAI(moveAI);

				}
				else
				{
					UIAgentDisplay.SetAgent(null);
					DrawAIPath.SetMoveAI(null);

				}

			}
			else
			{
				UIAgentDisplay.SetAgent(null);
				DrawAIPath.SetMoveAI(null);

			}
		}

	}
}
