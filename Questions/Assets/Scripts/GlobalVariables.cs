using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    // Create a List of answers so we can have access to it at any time
    // For this first version, it's going to only be integers, because.... easy?
    public List<int> answers = new List<int>();
    
    // List of things that Open AI has said to the player
    public List<string> openAIMessages = new List<string>();

    public float  currentTemperature;
    public string currentConditions;
    public string city;

    public bool loading, ready;
    public bool error;
    
    public static GlobalVariables S;
    
    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        DontDestroyOnLoad(this);
        // reset all stored variables;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAnswer(int answerValue)
    {
        answers.Add(answerValue);
    }

    public void Reset()
    {
        for (int i = 0; i < answers.Capacity; i++)
        {
            answers.Remove(i);
        }

        loading = true;
        ready = false;
        error = false;
    }
}
