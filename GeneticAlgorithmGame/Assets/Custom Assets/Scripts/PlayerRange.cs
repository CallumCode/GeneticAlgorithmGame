using UnityEngine;
using System.Collections;

public class PlayerRange : MonoBehaviour
{


	void OnTriggerEnter(Collider other)
	{
		MoveAI moveAI = other.gameObject.GetComponent<MoveAI>();

		if (moveAI != null)
		{
			moveAI.KillSelf();
		}

	}

}