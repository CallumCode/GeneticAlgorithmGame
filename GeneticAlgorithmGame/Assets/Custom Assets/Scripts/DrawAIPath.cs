using UnityEngine;
using System.Collections;

public class DrawAIPath : MonoBehaviour
{
	LineRenderer lineRenderer;
	// Use this for initialization
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update()
	{

	}


public	void SetMoveAI(MoveAI moveAI)
	{

		if (moveAI)
		{

			Agent agent = moveAI.GetAgent();

			lineRenderer.SetVertexCount(Agent.numOfComponets);

			Vector3 pos = moveAI.startPos;
			Vector3 dir = moveAI.starDir;

			for (int i = 0; i < Agent.numOfComponets; i++)
			{
				lineRenderer.SetPosition(i, pos);

				float time = 1 / MoveAI.updateRate;
				time *= moveAI.speed;
				dir = Quaternion.AngleAxis(agent.GetComponet(i).ID , Vector3.up) * dir;
				pos += dir * time;

			}

			lineRenderer.enabled = true;
		}
		else
		{
			lineRenderer.enabled = false;
		}

	}
}
