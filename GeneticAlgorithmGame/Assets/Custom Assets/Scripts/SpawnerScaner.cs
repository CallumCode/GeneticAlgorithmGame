using UnityEngine;
using System.Collections;

public class SpawnerScaner : MonoBehaviour
{

	public float moveSpeed = 0.1f;

	bool blocked = false;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		blocked = false;

		transform.Translate(transform.right * moveSpeed);
	}


	public void SetDir(float dir)
	{
		moveSpeed = Mathf.Abs(moveSpeed) * dir;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("WalkAI"))
		{
			blocked = true;
		}
	}

	public bool IsBlocked()
	{
		return blocked;
	}

	public Vector3 GetPos()
	{
		return transform.position;
	}
}