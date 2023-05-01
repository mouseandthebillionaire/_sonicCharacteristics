using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    // Create a List of answers so we can have access to it at any time
    // For this first version, it's going to only be integers, because.... easy?
    public List<int> answers = new List<int>();
    
    public static GlobalVariables S;
    
    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        
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
    }
}
