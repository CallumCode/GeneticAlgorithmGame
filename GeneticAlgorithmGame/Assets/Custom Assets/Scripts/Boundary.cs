using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour
{


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("MoveAI"))
		{
			MoveAI moveAI = other.gameObject.GetComponent<MoveAI>();

			if (moveAI != null)
			{
				moveAI.KillSelf();
			}
		}

 
			if (other.CompareTag("WalkAI"))
			{
				WalkAI walkAI = other.gameObject.GetComponentInParent<WalkAI>();

				if (walkAI != null)
				{
					walkAI.Failed();
				}
			}
	}

}
