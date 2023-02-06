using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Question : MonoBehaviour {
    public TMPro.TMP_Dropdown dropdown;

    public void SubmitAnswer() {
        StartCoroutine(ProcessAnswer());
    }

    private IEnumerator ProcessAnswer() {
        Debug.Log(dropdown.value);
        AudioManager.S.UpdateSoundtrack();
        // Take a second to respond to the answer
        yield return new WaitForSeconds(2);
        // Say something about it?
        // Probably with a Coroutine?
        // Also probably launch this from the dialogue coroutine
        QuestionManager.S.NewQuestion();
        yield return null;
    }
}
