using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour {
    public GameObject[] questions;
    public int currQuestion;

    public GameObject gameOverDialogue;

    public static QuestionManager S;

    void Awake() {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuestions()
    {
        currQuestion = 0;
        questions[currQuestion].SetActive(true);
    }
    
    // Load another Question
    public void NewQuestion() {
        Debug.Log("loading next question");
        // in this test version let's just work through a set of 5 questions

        // Eventually we'll have to figure out a different way to figure out where we are in the question process
        if (currQuestion+1 < questions.Length)
        {
            currQuestion++;
            questions[currQuestion].SetActive(true);
        }
        else
        {
            gameOverDialogue.SetActive(true);
        }
    }
}
