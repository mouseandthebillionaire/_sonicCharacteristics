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
    public  GameObject   chimerPrefab;
    private List<Chimer> chimerList = new List<Chimer>();
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
            // stop the last chimer if one is going
            if (chimerList.Capacity > 0 && chimerList.Capacity < 5)
            {
                Debug.Log("there's another chimer going");
            }
            
            // Are we supposed to create a chimer for this click?
            if (chimeClick[currText]){
                CreateChime();
                Debug.Log("ding!");
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
        introText_display.text = introText[currText];
        
        yield return new WaitForSeconds(1);
        
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

    private void CreateChime()
    {
        // Instantiate the chimer
        GameObject c;
        c = Instantiate(chimerPrefab);
        c.transform.position = Input.mousePosition;
        c.transform.parent = GameObject.Find("Chimers").GetComponent<Transform>();
        

        
        // Get this current Chimer script and add it to our list
        chimerList.Add(c.GetComponent<Chimer>());
        
    }

    private void ExitIntro()
    {
        QuestionManager.S.StartQuestions();
        Debug.Log("shut it down");
        this.gameObject.SetActive(false);
    }

}
