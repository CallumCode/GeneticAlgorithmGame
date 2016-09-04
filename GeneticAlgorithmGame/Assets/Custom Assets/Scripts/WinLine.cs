using UnityEngine;
using System.Collections;

public class WinLine : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//transform.Translate(transform.forward * Time.deltaTime * 0.01f);
	}

	void OnTriggerEnter(Collider other)
	{

		MoveAI moveAI = other.gameObject.GetComponent<MoveAI>();

		if (moveAI != null)
		{
			moveAI.Win();
		}

	}

}
