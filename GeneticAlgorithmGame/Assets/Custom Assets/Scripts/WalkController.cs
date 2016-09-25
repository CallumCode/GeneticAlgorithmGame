using UnityEngine;
using System.Collections;

public class WalkController : MonoBehaviour
{

	public Rigidbody Body;
	public Rigidbody LegHighLeft;
	public Rigidbody LegHighRight;

	public Rigidbody LegLowLeft;
	public Rigidbody LegLowRight;


	public Rigidbody FootLeft;
	public Rigidbody FootRight;


	public Rigidbody ArmLeft;
	public Rigidbody ArmRight;


	public Rigidbody WristLeft;
	public Rigidbody WristRight;

	public float amount = 1;

	enum ForceIndex
	{
		Right_BodyToArm = 0,
		Right_ArmToWrist,
		Right_BodyToLeg,
		Right_LegToLowLeg,
		Right_LowLegToFoot,

		Left_BodyToArm,
		Left_ArmToWrist,
		Left_BodyToLeg,
		Left_LegToLowLeg,
		Left_LowLegToFoot,
		count,
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKey(KeyCode.Space))
		{

			ArrayList tempForceList = new ArrayList();

			for (int i = 0; i < (int)ForceIndex.count; i++)
			{
				float force = Random.Range(-250, 250);
				tempForceList.Add(force);
			}

			DoForceTick(tempForceList);
		}

	}


	void DoAllForceChecks()
	{
		CheckForceMove(Body, LegHighLeft, amount, KeyCode.Q);
		CheckForceMove(Body, LegHighLeft, -amount, KeyCode.W);

		CheckForceMove(LegHighLeft, LegLowLeft, amount, KeyCode.A);
		CheckForceMove(LegHighLeft, LegLowLeft, -amount, KeyCode.S);

		CheckForceMove(LegLowLeft, FootLeft, amount, KeyCode.Z);
		CheckForceMove(LegLowLeft, FootLeft, -amount, KeyCode.X);

		CheckForceMove(Body, ArmLeft, amount, KeyCode.E);
		CheckForceMove(Body, ArmLeft, -amount, KeyCode.R);


		CheckForceMove(ArmLeft, WristLeft, amount, KeyCode.D);
		CheckForceMove(ArmLeft, WristLeft, -amount, KeyCode.F);

		//

		CheckForceMove(Body, LegHighRight, amount, KeyCode.Y);
		CheckForceMove(Body, LegHighRight, -amount, KeyCode.U);

		CheckForceMove(LegHighRight, LegLowRight, amount, KeyCode.H);
		CheckForceMove(LegHighRight, LegLowRight, -amount, KeyCode.J);

		CheckForceMove(LegLowRight, FootRight, amount, KeyCode.N);
		CheckForceMove(LegLowRight, FootRight, -amount, KeyCode.M);

		CheckForceMove(Body, ArmRight, amount, KeyCode.I);
		CheckForceMove(Body, ArmRight, -amount, KeyCode.O);

		CheckForceMove(ArmRight, WristRight, amount, KeyCode.K);
		CheckForceMove(ArmRight, WristRight, -amount, KeyCode.L);
	}

	void CheckForceMove(Rigidbody rigidBdy1, Rigidbody rigidBdy2, float force, KeyCode input)
	{
		if (Input.GetKey(input))
		{
			DoForceMove(rigidBdy1, rigidBdy2, force);
		}
	}


	void DoForceMove(Rigidbody rigidBdy1, Rigidbody rigidBdy2, float force)
	{
		Vector3 forceVector = rigidBdy1.transform.forward * force;
		rigidBdy1.AddForce(forceVector);

		Color colour = force > 0 ? Color.red : Color.blue;
		Debug.DrawLine(rigidBdy1.transform.position, rigidBdy1.transform.position + forceVector * 0.01f, colour, 1);

		force *= -1;
		forceVector = rigidBdy2.transform.forward * force;
		rigidBdy2.AddForce(forceVector);

		colour = force > 0 ? Color.red : Color.blue;
		Debug.DrawLine(rigidBdy2.transform.position, rigidBdy2.transform.position + forceVector * 0.01f, colour, 1);
	}


	void DoForceTick(ArrayList forceList)
	{
		if (forceList != null && (int)ForceIndex.count == forceList.Count)

		{
			DoForceMove(Body, LegHighLeft, (float)forceList[(int)ForceIndex.Left_BodyToLeg]);
			DoForceMove(LegHighLeft, LegLowLeft, (float)forceList[(int)ForceIndex.Left_LegToLowLeg]);
			DoForceMove(LegLowLeft, FootLeft, (float)forceList[(int)ForceIndex.Left_LowLegToFoot]);

			DoForceMove(Body, ArmLeft, (float)forceList[(int)ForceIndex.Left_BodyToArm]);
			DoForceMove(ArmLeft, WristLeft, (float)forceList[(int)ForceIndex.Left_ArmToWrist]);

			DoForceMove(Body, LegHighRight, (float)forceList[(int)ForceIndex.Right_BodyToLeg]);
			DoForceMove(LegHighRight, LegLowRight, (float)forceList[(int)ForceIndex.Right_LegToLowLeg]);
			DoForceMove(LegLowRight, FootRight, (float)forceList[(int)ForceIndex.Right_LowLegToFoot]);

			DoForceMove(Body, ArmRight, (float)forceList[(int)ForceIndex.Right_BodyToArm]);
			DoForceMove(ArmRight, WristRight, (float)forceList[(int)ForceIndex.Right_ArmToWrist]);
		}
	}


}