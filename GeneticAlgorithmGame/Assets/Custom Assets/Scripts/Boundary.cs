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

		MoveAI moveAI = other.gameObject.GetComponent<MoveAI>();

		if (moveAI != null)
		{
			moveAI.KillSelf();
		}

	}

}
