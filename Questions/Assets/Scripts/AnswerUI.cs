using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AnswerUI : MonoBehaviour
{
    private TMP_Text t;
    
    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.GetComponentInChildren<TMP_Text>();
        t.color = new Color(1f, 1f, 1f, 0.75f);
    }

    void OnMouseOver()
    {
        t.color = Color.white;
        if (Input.GetMouseButton(1)) SetAnswer();
    }

    private void OnMouseExit()
    {
        t.color = new Color(1f, 1f, 1f, 0.75f);
    }

    public void SetAnswer()
    {
        int answer = int.Parse(this.gameObject.name);
        Question Q = GetComponentInParent<Question>();
        Q.SubmitAnswer(answer);
    }
}
