using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{

    Text numOFGensText;
    public Population popToshow;
    public GameObject mainCanvasObeject;
    public GameObject TextPrefab;

    ArrayList listOfGensCounts;
    

    public Text AddTextToCanvas(string textString, GameObject canvasGameObject , float depth)
    {

        GameObject textObejct = Instantiate(TextPrefab, TextPrefab.transform.position, TextPrefab.transform.rotation) as GameObject;
         


        textObejct.transform.SetParent(canvasGameObject.transform);
        textObejct.name = textString;       

        Text text =  textObejct.GetComponent<Text>();
        text.text = textString;

         

        text.transform.position = text.transform.position - Vector3.up* text.rectTransform.rect.height * depth * 0.25f;

 

        return text;
    }

    // Use this for initialization
    void Start ()
    {
        listOfGensCounts = new ArrayList();

        numOFGensText = AddTextToCanvas("numOFGensText", mainCanvasObeject , 0);
    }

    // Update is called once per frame
    void Update ()
    {
       
       int numOfGens = popToshow.GetNumOfGens();
        numOFGensText.text = "Num of Gens " + numOfGens;
 
         UpdateGenDisplay();
    }


    void UpdateGenDisplay()
    {        
        CreateNewGenTexts();

        int numOfgens = listOfGensCounts.Count;            
        for(int i = 0; i < numOfgens; i++)
        {
           Text genCountText = (Text)listOfGensCounts[i];
           genCountText.text = "Gen " + i + " : Untested : " + popToshow.GetUnTestedCount(i) + " Tested : " + popToshow.GetTestedCount(i);
        }

    }

    void CreateNewGenTexts()
    {

        int numOfGens = popToshow.GetNumOfGens();
        if (listOfGensCounts.Count < numOfGens)
        {
            Text text = AddTextToCanvas("Gen n", mainCanvasObeject, listOfGensCounts.Count + 1);
            listOfGensCounts.Add(text);
        }
    }

}


