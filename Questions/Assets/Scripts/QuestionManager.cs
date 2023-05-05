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
        currQuestion = -1;
        AudioManager.S.StartQuestions();
        NewQuestion();
    }
    
    // Load another Question
    public void NewQuestion() {
        

        // Eventually we'll have to figure out a different way to figure out where we are in the question process
        if (currQuestion+1 < questions.Length)
        {
            currQuestion++;
            Debug.Log("loading Question #" + currQuestion);
            questions[currQuestion].SetActive(true);
        }
    }

    public void EndQuestioning()
    {
        StartCoroutine(ClosingOut());
    }

    public IEnumerator ClosingOut()
    {
        // Get rid of the current question
        questions[currQuestion].SetActive(false);
        // Play a note to end the song better
        
        // Transition to the GameOverScreen
        gameOverDialogue.SetActive(true);
        // Wait a few seconds
        yield return new WaitForSeconds(3);
        // And shut down the program
        Application.Quit();

        yield return null;
    }
}
