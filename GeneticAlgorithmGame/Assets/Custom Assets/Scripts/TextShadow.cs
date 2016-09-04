using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextShadow : MonoBehaviour
{
	Text text;
	Text shadow;

	RectTransform rectTransform;
	RectTransform rectTrainsformText;
	// Use this for initialization
	void Start()
	{
		text = GetComponentInParent<Text>();
		shadow = GetComponent<Text>();

		rectTransform = GetComponent<RectTransform>();
		rectTrainsformText = GetComponentInParent<RectTransform>();


	}

	// Update is called once per frame
	void Update()
	{
		if(text && shadow)
		{
			shadow.text = text.text;

			rectTransform.position = rectTrainsformText.position + Vector3.right * 0.1f + Vector3.up * 0.1f;

		}

	}
}
