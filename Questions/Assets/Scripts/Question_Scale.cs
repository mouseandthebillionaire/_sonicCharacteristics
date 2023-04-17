using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Question_Scale : Question {

    // Eventually let's make this a 2D array so there's more variety
    // Also, we could import an array of responses from a resource file, but this is fine for now



    private IEnumerator Response(int responseValue)
    {
        TMPro.TMP_Text snark = GameObject.Find("Response").GetComponent<TMP_Text>();
        snark.text = responseText[responseValue];
        yield return null;
    }
}
