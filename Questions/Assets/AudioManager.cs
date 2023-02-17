using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager S;
    
    void Awake() {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSoundtrack() {
        // maybe for now, let's update based on what# question you're on
        // probably not good for later (especially with a branching structure) but a place to start!
        
        // should probably pass in the question number... but for now we can just grab it from the QuestionManager
        switch (QuestionManager.S.currQuestion)
        {
            case 0:
                Debug.Log("Question 0");
                break;
            case 1:
                Debug.Log("Question 1");
                break;
            case 2:
                Debug.Log("Question 2");
                break;
            case 3:
                Debug.Log("Question 3");
                break;
            case 4:
                Debug.Log("Question 4");
                break;
        }
    }
}
