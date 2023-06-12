using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public TMP_Text t;
    
    public void Start()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        while (GlobalVariables.S.loading)
        {
            t.text = "          Loading.";
            yield return new WaitForSeconds(0.5f);
            t.text = "          Loading..";
            yield return new WaitForSeconds(0.5f);
            t.text = "          Loading...";
            yield return new WaitForSeconds(0.5f);
        }

        if (GlobalVariables.S.error) t.text = "           Error";
        else t.text = "Click Anywhere to Begin";
    }

    public void OnMouseDown()
    {
        if(!GlobalVariables.S.loading) SceneManager.LoadScene("Questions");
    }
}
