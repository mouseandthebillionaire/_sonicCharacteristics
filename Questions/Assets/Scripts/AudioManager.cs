using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager S;

    public  AudioClip[] rootNotes;
    private int         songRoot;

    private int         tempo;

    public AudioMixerSnapshot[] mainInstrumentVolume;
    public AudioMixer           mainMixer;
    
    void Awake() {
        S = this;
    }

    public void Start()
    {
        // Choose one of the root notes to make the MAIN ROOT for this song
        songRoot = Random.Range(0, rootNotes.Length);
        Debug.Log(songRoot);

    }

    // Update is called once per frame
    void Update()
    {
        
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
                
                StartCoroutine(Root());
                break;
            default:
                break;
        }
    }

    IEnumerator Root()
    {
        
        yield return new WaitForSecondsRealtime(tempo);
        return null;
    }
}
