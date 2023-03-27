using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Question_ShortAnswer : Question {

    public void TextAnswer()
    {
        TMP_InputField input = GameObject.Find("AnswerText").GetComponent<TMP_InputField>();
        string answer = input.text;
        int answerLength = answer.Length;
        Debug.Log(answerLength);
    }
    
}
