using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Question: MonoBehaviour {
    [Header("What question do we want to ask them")]
    public string question;

    public enum Effects
    {
        LeadInstrument = 0,
        DelayWetMix = 1,
        DelayTime = 2,
        Distortion = 3,
        Pitch = 4
    };

    public Effects effect = new Effects();
    
    // Eventually let's make this a 2D array so there's more variety
    // Also, we could import an array of responses from a resource file, but this is fine for now
    private string[] responseText = new string[]
    {
        "really?",
        "I'm sorry to hear that.",
        "yeah?",
        "oh, yeah? Huh.",
        "well, okay then!"
    };
    public void SubmitAnswer()
    {
        StartCoroutine(ProcessAnswer());
    }

    private IEnumerator ProcessAnswer()
    {
        // for now we're dealing with a discrete value from the dropdown
        // this will need to be more nunanced later
        int questionValue = (int)GetComponentInChildren<Slider>().value;
        Debug.Log(questionValue);
        
        // Say something about it?
        // Maybe attach a text file to each question holding these comments
        // Probably with a Coroutine?
        // For now, let's just pick some random snarky things to say
        StartCoroutine(Response(questionValue));
        
        // Process the audio changes...
        int effect = 0;
        AudioManager.S.UpdateSoundtrack(effect, questionValue);
        
        // Take a second to read the snark
        yield return new WaitForSeconds(2);
        
        // Hide the question when all this is finished
        this.gameObject.SetActive(false);
        
        // Tell the Question Manager to load the next question
        QuestionManager.S.NewQuestion();
        yield return null;
    }

    private IEnumerator Response(int responseValue)
    {
        TMPro.TMP_Text snark = GameObject.Find("Response").GetComponent<TMP_Text>();
        snark.text = responseText[responseValue];
        yield return null;
    }
}
