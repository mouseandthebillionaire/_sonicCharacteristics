using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Question : MonoBehaviour {
    public TMPro.TMP_Dropdown dropdown;

    private string[] snarkText = new string[]{"really?", "sure. okay, that's a pretty good number. I guess.",
        "oh now you're just messing with me", "way to phone it in.", "Nothing surprises me at this point."}; 

    public void SubmitAnswer() {
        StartCoroutine(ProcessAnswer());
    }

    private IEnumerator ProcessAnswer() {
        Debug.Log(dropdown.value);
        AudioManager.S.UpdateSoundtrack();
        
        // Say something about it?
        // Maybe attach a text file to each question holding these comments
        // Probably with a Coroutine?
        // For now, let's just pick some random snarky things to say
        StartCoroutine(Snark());
        
        // Take a second to read the snark
        yield return new WaitForSeconds(2);
        
        // Hide the question when all this is finished
        this.gameObject.SetActive(false);
        
        // Tell the Question Manager to load the next question
        QuestionManager.S.NewQuestion();
        yield return null;
    }

    private IEnumerator Snark()
    {
        TMPro.TMP_Text snark = GameObject.Find("Snark").GetComponent<TMP_Text>();
        snark.text = snarkText[QuestionManager.S.currQuestion];
        yield return null;
    }
}
