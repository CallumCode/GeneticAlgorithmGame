using UnityEngine;
using System.Collections;

public class SpawnerScaner : MonoBehaviour
{

	public float moveSpeed = 0.1f;

	int counter;

	public bool isEmpty = true ;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		transform.Translate(transform.right * moveSpeed);
	}

	void LateUpdate()
	{

	}


	public void SetDir(float dir)
	{
		moveSpeed = Mathf.Abs(moveSpeed) * dir;
	}



	public bool IsBlocked()
	{
		return  !isEmpty;
	}

	public Vector3 GetPos()
	{
		return transform.position;
	}
 


	void FixedUpdate()
	{
		isEmpty = true;
	}

	void OnTriggerStay(Collider other)
	{
		isEmpty = false;
	}

}