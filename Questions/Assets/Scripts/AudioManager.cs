using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager S;

    private int         songRoot;

    private int         tempo;

    public LibPdInstance pdPatch;

    public AudioMixerSnapshot[] mainInstrumentVolume;
    public AudioMixer           mainMixer;
    
    void Awake() {
        S = this;
    }

    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Test
        if (Input.GetKeyDown(KeyCode.A))
        {
            pdPatch.SendBang("start");
            Debug.Log("bang?");
        }
    }

    public void UpdateSoundtrack()
    {
        int a = GlobalVariables.S.answers[QuestionManager.S.currQuestion];
        Debug.Log(a);

        // stage is chosen based on what question we are on
        int stage = QuestionManager.S.currQuestion;
        Debug.Log("Starting Stage " + stage);

        switch (stage)
        { 
            case 0:
                // Set the tempo and start the root note playing
                tempo = 1 / GlobalVariables.S.answers[stage];
                // Create the AudioSource
                
                break;
            default:
                break;
        }
    }
    
}
