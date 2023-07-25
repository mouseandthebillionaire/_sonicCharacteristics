using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponseManager : MonoBehaviour
{
	public static ResponseManager S;

	private TextAsset    responses_asset;
	public  List<string> responses = new List<string>();

	public  Image fadePanel;
	private float textFadeSpeed = 1f;

	// Objects to Display the Text
	public TMPro.TMP_Text response_display;

	// Start is called before the first frame update
	void Start()
	{
		S = this;
		LoadText();
	}

	// Update is called once per frame
	public void Respond()
	{
		StartCoroutine(RespondControl());
	}

	private IEnumerator RespondControl()
	{
		// set text transparency
		response_display.color = new Color(1f, 1f, 1f, 1f);
		// load the text
		int r = Random.Range(0, responses.Count);
		response_display.text = responses[r];
		// fade it in
		while (fadePanel.color.a > 0.0f)
		{
			fadePanel.color = new Color(
				fadePanel.color.r,
				fadePanel.color.g,
				fadePanel.color.b,
				fadePanel.color.a - (Time.deltaTime * textFadeSpeed));
			yield return null;
		}

		// Wait a few seconds for them to read it
		yield return new WaitForSeconds(2f);
		// fade it out
		while (fadePanel.color.a < 1.0f)
		{
			fadePanel.color = new Color(
				fadePanel.color.r,
				fadePanel.color.g,
				fadePanel.color.b,
				fadePanel.color.a + (Time.deltaTime * textFadeSpeed));
			yield return null;
		}
		
		// set text transparency
		response_display.color = new Color(1f, 1f, 1f, 0f);

		// delete that response so we don't use it again
		responses.RemoveAt(r);
		// and then load the next Question
		QuestionManager.S.NewQuestion();
	}

	private void LoadText()
	{
		responses_asset = Resources.Load("responseText") as TextAsset;
		string[] responseList = responses_asset.text.Split('\n');
		for (int i = 0; i < responseList.Length; i++)
		{
			responses.Add(responseList[i]);
		}
	}
}