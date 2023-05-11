using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UIElements;

public class Introduction : MonoBehaviour
{
    public bool skipIntro;
    
    // Manage the Text
    private TextAsset    introText_asset;
    public  List<string> introText  = new List<string>();
    private List<bool>   chimeClick = new List<bool>();
    private int          currText;
    public  float        textFadeSpeed = 1f;

    // Chimer stuff
    private bool         clickable;


    // Objects to Display the Text
    public TMPro.TMP_Text introText_display;

    // Start is called before the first frame update
    void Start()
    {
        // Are we skipping all of this for testing reasons?
        if (skipIntro) ExitIntro();

        else
        {
            GetText();
            StartCoroutine(UpdateText());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (clickable)
        {
            // Are we supposed to create a chimer for this click?
            if (chimeClick[currText]){
                AudioManager.S.CreateChime();
            }
            StartCoroutine(UpdateText());
        }
    }

    private void GetText()
    {
        introText_asset = Resources.Load("introductionText") as TextAsset;
        string[] introTextCollection = introText_asset.text.Split('\n');
        for (int i = 0; i < introTextCollection.Length; i++)
        {
            string[] temp = introTextCollection[i].Split(';');
            introText.Add(temp[0]);
            chimeClick.Add(Convert.ToBoolean(temp[1]));
        }
        currText = -1;
    }

    private IEnumerator UpdateText()
    {
        // don't let them click during the transitions
        clickable = false;

        // Fade out the text
        while (introText_display.color.a > 0.0f)
        {
            introText_display.color = new Color(
                introText_display.color.r,
                introText_display.color.g,
                introText_display.color.b,
                introText_display.color.a - (Time.deltaTime * textFadeSpeed));
            yield return null;
        }
        
        // are we at the end of the introduction?
        if (currText+1 >= introText.Count)
        {
            // Then let's load the questions and exit the intro
            ExitIntro();
            yield break;
        }
        // Change the text
        currText += 1;
        string newText = introText[currText];
        newText = newText.Replace("-", "\n"); // needed to force new line
        introText_display.SetText(newText);
        
        yield return new WaitForSeconds(1);
        // Do something to refresh the text so the newLines work correctly
        
        // Fade in the New Text
        while (introText_display.color.a < 1.0f)
        {
            introText_display.color = new Color(
                introText_display.color.r,
                introText_display.color.g,
                introText_display.color.b,
                introText_display.color.a + (Time.deltaTime * textFadeSpeed));
            yield return null;
        }
        
        // Only accept a click once all this folderol has happened
        clickable = true;

    }

    private void ExitIntro()
    {
        QuestionManager.S.StartQuestions();
        this.gameObject.SetActive(false);
    }

}
