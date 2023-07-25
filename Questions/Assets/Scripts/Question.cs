using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Question: MonoBehaviour
{
    [Header("What question do we want to ask them")] 
    public bool AI_Question;
    [Header("Set this manually for each stage")]
    public int ai_stage;

    [TextArea(5, 10)]
    public string question;

    public string[] answers;

    private Image fadePanel;
    private float textFadeSpeed = 1f;

    public bool respondAfter;

    private void Start()
    {
        StartCoroutine(PrepQuestion());
        StartCoroutine(FadeIn());
    }

    private IEnumerator PrepQuestion()
    {
        GameObject questionText = GameObject.Find("Canvas/QuestionText");
        // Set the Question
        if (AI_Question)
        {
            switch (ai_stage) {
                case 0: 
                    questionText.GetComponent<TMP_Text>().text = $"I see that you're in {GlobalVariables.S.city} right now. Is it a nice place?";
                    break;
                case 1: 
                    questionText.GetComponent<TMP_Text>().text = GlobalVariables.S.openAIMessages[ai_stage - 1]; 
                    break;
                case 2:
                    questionText.GetComponent<TMP_Text>().text = GlobalVariables.S.openAIMessages[ai_stage - 1]; 
                    break;
                default:
                    break;
            }
        }
        else
        {
            questionText.GetComponent<TMP_Text>().text = question;
        }

        // Set the Answers
        for (int i = 0; i < answers.Length; i++) {
            GameObject a_GO = GameObject.Find("Answers/" + i);
            a_GO.GetComponentInChildren<TMP_Text>().text = answers[i];
        } 
        yield return null;
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
    }

    public void SubmitAnswer(int _answer)
    { 
        Debug.Log("answer is " + _answer);
        StartCoroutine(ProcessAnswer(_answer));
    }

    private IEnumerator ProcessAnswer(int value)
    {
        int v = value;
        
        GlobalVariables.S.AddAnswer(v);

        AudioManager.S.UpdateSoundtrack();

        // And fade out
        StartCoroutine(FadeOut());
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
        
                
        // Do we use this opportunity to comment and make an Audio Change?
        if (respondAfter)
        {
            ResponseManager.S.Respond();
        }
        else
        {
            // Otherwise, just tell the Question Manager to load the next question
            QuestionManager.S.NewQuestion();
        }

    }
}
