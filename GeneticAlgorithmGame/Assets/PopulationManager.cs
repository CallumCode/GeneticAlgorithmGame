using UnityEngine;
using System.Collections;

public class PopulationManager : MonoBehaviour
{


    public Population testPop;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GetAgent();
        }

    }

    void GetAgent()
    {

       Agent agentA = testPop.GetLatestNonTestedAgent();

      bool kill =  testPop.TestAgent(ref agentA);       

        if(kill == true)
        {
            Debug.Log("Agent failed test ");
        }
        else
        {
            Debug.Log("Agent passed test ");
        }

     }
}
