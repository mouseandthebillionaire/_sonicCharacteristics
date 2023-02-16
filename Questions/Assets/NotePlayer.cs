using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlayer : MonoBehaviour
{
    // 1, 2, 4, 8, 16 notes
    public float noteFrequency;
    public float noteOffset;
    // 1 = all / 0.75, 0.5, 0.25, 0.125 etc
    public float notePossibilityBreadth;

    public AudioClip[] notes;
    public string      currentNote;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NoteControl());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator NoteControl()
    {
        if (noteOffset > 0)
        {
            yield return new WaitForSeconds(noteOffset);
            noteOffset = 0;
        }

        AudioSource a = GetComponent<AudioSource>();
        if(!a.isPlaying) PlayNote();
        yield return new WaitForSeconds(noteFrequency);
        StartCoroutine(NoteControl());
    }
    
    private void PlayNote()
    {
        AudioSource a = GetComponent<AudioSource>();
        // Possible Choices
        float pc = notes.Length * notePossibilityBreadth;
        // Pick a Note
        int r = (int)Random.Range(0, pc);
        a.clip = notes[r];
        currentNote = notes[r].ToString();
        a.Play();
    }
}
