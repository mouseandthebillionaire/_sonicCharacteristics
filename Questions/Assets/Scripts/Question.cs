using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Question: MonoBehaviour {
    [Header("What question do we want to ask them")]
    public string question;
    
    [Header("Are the responses random?")]
    // otherwise we can link them to the specific answer given
    public bool randomResponse;

    [Header("these can be edited for greater immersion")]
    public string[] responseText = new string[]
    {
        "really?",
        "I'm sorry to hear that.",
        "yeah?",
        "oh, yeah? Huh.",
        "well, okay then!"
    };

    private Image fadePanel;
    private float textFadeSpeed = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        fadePanel = GameObject.Find("FadePanel").GetComponent<Image>();
        
        // Fade in the New Text
        while (fadePanel.color.a > 0.0f)
        {
            fadePanel.color = new Color(
                fadePanel.color.r,
                fadePanel.color.g,
                fadePanel.color.b,
                fadePanel.color.a - (Time.deltaTime * textFadeSpeed));
            yield return null;
        }

        //fadePanel.gameObject.SetActive(false);
    }
    
    

    // Effect Chooser
    // Not actually using this right now, but let's save it for later
    // public enum Effects
    // {
    //     LeadInstrument = 0,
    //     DelayWetMix = 1,
    //     DelayTime = 2,
    //     Distortion = 3,
    //     Pitch = 4
    // };
    //
    // public Effects effect = new Effects();
    
    public void SubmitAnswer()
    {
        int questionValue;
        if (GetComponentInChildren<Slider>() != null)
        {
            questionValue = (int) GetComponentInChildren<Slider>().value;
        }
        else
        {
            questionValue = 99;
        }
        StartCoroutine(ProcessAnswer(questionValue));
    }

    private IEnumerator ProcessAnswer(int value)
    {
        int v = value;
        
        GlobalVariables.S.AddAnswer(v);
        
        AudioManager.S.UpdateSoundtrack();

        // Take a second to read the snark
        // yield return new WaitForSeconds(2);

        // And fade out
        StartCoroutine(FadeOut());
        yield return null;
    }

    private IEnumerator Response(int responseValue)
    {
        TMPro.TMP_Text snark = GameObject.Find("Response").GetComponent<TMP_Text>();
        snark.text = responseText[responseValue];
        yield return null;
    }
    
    private IEnumerator FadeOut()
    {
        fadePanel = GameObject.Find("FadePanel").GetComponent<Image>();
        //fadePanel.gameObject.SetActive(true);
        
        // Fade in the New Text
        while (fadePanel.color.a < 1.0f)
        {
            fadePanel.color = new Color(
                fadePanel.color.r,
                fadePanel.color.g,
                fadePanel.color.b,
                fadePanel.color.a + (Time.deltaTime * textFadeSpeed));
            yield return null;
        }
        
        // Hide the question when all this is finished
        this.gameObject.SetActive(false);
        
        // And tell the Question Manager to load the next question
        QuestionManager.S.NewQuestion();

        
    }
}
