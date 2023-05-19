using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityFacts : MonoBehaviour
{
    public TMPro.TMP_Text displayText;

    [Header("Set this manually for each stage")]
    public int stage;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void UpdateText()
    {
        switch (stage)
        {
            case 0:
                displayText.text = $"I see that you're in {GlobalVariables.S.city} right now. Is it a nice place?";
                break;
            case 1:
                displayText.text = GlobalVariables.S.openAIMessages[stage - 1];
                break;
            case 2:
                displayText.text = GlobalVariables.S.openAIMessages[stage - 1];
                break;
            default:
                break;
        }
    }
}
