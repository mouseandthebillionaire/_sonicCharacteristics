using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmicSynth : MonoBehaviour
{
    public AudioSource note;

    public  AudioClip[] notes;
    public  List<int>   noteValues = new List<int>();
    private int         currNote;
    
    private float       delaySpeed;

    public AudioMixerGroup mixerGroup;

    public static RhythmicSynth S;

    void Awake()
    {
        S = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeSynth());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator InitializeSynth()
    {
        float quarterNotes = 60000 / AudioManager.S.tempo;
        // .12 for 8th notes
        delaySpeed = quarterNotes / 2; // 8th notes
        mixerGroup.audioMixer.SetFloat("rhythmSynth_delay", delaySpeed);
        
        // Initialize the first two notes to be the root note;
        noteValues.Add(5);
        noteValues.Add(5);
        currNote = 0;
        note.clip = notes[noteValues[currNote]];
        
        StartCoroutine(RootRhythm());
        yield return  null;
    }

    public void AddNotes(int note)
    {
        noteValues.Add(note);
    }

    private IEnumerator RootRhythm()
    {
        // Load it up
        int noteClip = noteValues[currNote];
        note.clip = notes[noteClip];
        
        note.Play();
        float nextNote = 60 / AudioManager.S.tempo; // quarter notes
        Debug.Log("Next Note in " + nextNote + " seconds");
        // what's Next
        currNote = (currNote + 1) % noteValues.Count;
        
        yield return new WaitForSecondsRealtime(nextNote);
        StartCoroutine(RootRhythm());
    }
}
