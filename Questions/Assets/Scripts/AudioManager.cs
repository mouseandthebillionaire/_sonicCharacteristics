using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager S;

    private int         songRoot;

    private float loopLength = 32;

    // Chimer Stuff
    public  GameObject   chimerPrefab;
    private List<Chimer> chimerList = new List<Chimer>();
    
    // Get Tempo
    private float       startTime;
    private List<float> timeIntervals = new List<float>();
    public  float       tempo;


    public LibPdInstance rightHand;
    public LibPdInstance leftHand;
    public LibPdInstance sfx;
    public LibPdInstance woodwindArp;

    public AudioMixerSnapshot intro, beginQuestions, addKeys, addSFX, ending;

    public AudioMixerSnapshot[] foodQuestionResults;

    // this could be an array eventually / or a PD patch of different outro possibilities
    public AudioMixerSnapshot outro;

    public AudioMixerSnapshot[] snapshotStages;
    public AudioMixer           mainMixer;
    
    void Awake() {
        S = this;
    }

    public void Start()
    {
        tempo = 60f;
        leftHand.SendFloat("tempo", tempo);
        rightHand.SendFloat("tempo", tempo);

        intro.TransitionTo(2);
        StartCoroutine(SetBasePitch());
    }
    

    // Update is called once per frame
    void Update()
    {

    }
    
    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){
     
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
     
        return(NewValue);
    }
    

    private IEnumerator SetBasePitch()
    {
        // at the start of the song, we're going to effect overall pitch based on the current temperature
        float kelvinTemp = GlobalVariables.S.currentTemperature;
        float tempPitch = scale(202.03f, 327.15f, .4f, 2f, kelvinTemp);
        mainMixer.SetFloat("masterPitch", tempPitch);
        yield return null;
    }

    public void UpdateSoundtrack()
    {
        // stage is chosen based on what question we are on
        int stage = QuestionManager.S.currQuestion;
        
        // What value did we get from the question?
        int a = GlobalVariables.S.answers[stage];
        
        switch (stage)
        { 
            case 0:
                // Set the third note in the RhythmicSynth
                RhythmicSynth.S.AddNotes(a);
                // Update Instrument for Leads
                leftHand.SendFloat("instrument", a);
                rightHand.SendFloat("instrument", a);
                
                // Effect the overall pitch of this? That could be funny?
                // Let's try making this happen really slowly
                StartCoroutine(PitchChange(a));

                break;
            case 1:
                // Set the fourth note in the RhythmicSynth
                RhythmicSynth.S.AddNotes(a);
                
                // Update Note Speed for Leads
                // Less sleep means you need more notes to pep you up!
                float noteSpeed = (a + 1) * 0.25f;
                rightHand.SendFloat("noteSpeed", noteSpeed);
                // Left Hand is half the speed
                noteSpeed = noteSpeed * 2;
                leftHand.SendFloat("noteSpeed", noteSpeed);
                break;
            case 2:
                // Manipulate sfx patch
                sfx.SendFloat("levels", a);
            
                // Update Line Length for Leads
                float lineLength = (a + 1) * 2;
                leftHand.SendFloat("length", lineLength);
                rightHand.SendFloat("length", lineLength);
                break;
            case 3:
                // Add delay to SFX and bring them in
                addKeys.TransitionTo(5);
            
                // Update note spread for Leads
                float noteSpread = (a + 1) * 2;
                leftHand.SendFloat("noteSpread", noteSpread);
                rightHand.SendFloat("noteSpread", noteSpread);
                
                
                break;
            case 4:
                // first aside about the person not using screens
                // Start the SFX playing
                // sfx.SendBang("sfxPlay");
                // And then turn up the volume
                addSFX.TransitionTo(5);
                break;
            case 5:
                // animal question ties to arp
                woodwindArp.SendFloat("instrument", a);
                // ARP has its own automatic volume increasing
                woodwindArp.SendBang("start");
                break;
            case 6:
                // food question
                foodQuestionResults[a].TransitionTo(5);
                break;
            case 7:
                // Asking about their city
                break;
            case 8:
                // Bedroom
                break;
            case 9:
                // Banana Aside

                break;
            case 10:
                // Crisis
                // Eventually branch out here to other endings
                break;
            case 11:
                // Books #1 -
                // For now no matter what, we're going to SLOWLY transition to only woodwinds and SpaceSynth
                outro.TransitionTo(20);
                break;
            case 12:
                // Second City Aside
                break;
            case 13:
                // Bananas Aside
                break;
            case 14:
                // Last City Aside
                // Slowly fade the music out
                ending.TransitionTo(20);
                break;
            case 15:
                // Ask about the music
                break;
            default:
                break;
        }
    }

    private IEnumerator PitchChange(int _mood)
    {
        float currentPitch = 1f;
        float finalPitch = .9f + (_mood * .025f);
        while (currentPitch >= finalPitch)
        {
            currentPitch -= .001f;
            mainMixer.SetFloat("masterPitchShift", currentPitch);
            yield return new WaitForSeconds(.25f);
        }
    }
    
    public void CreateChime()
    {
        // Instantiate the chimer
        GameObject c;
        c = Instantiate(chimerPrefab);
        c.transform.position = Input.mousePosition;
        c.transform.parent = GameObject.Find("Chimers").GetComponent<Transform>();
        

        
        // Get this current Chimer script and add it to our list
        chimerList.Add(c.GetComponent<Chimer>());
        
        TapTempo();
    }
    
    // A method to return the adjusted tempo (from the MainMixer's Pitch Adjustment)
    public float GetMasterPitch()
    {
        float value;
        bool result = mainMixer.GetFloat("masterPitch", out value);
        if (result) return value;
        else return 1f;
    }
    
    private IEnumerator Chimers()
    {
        yield return new WaitForSeconds(tempo / 40);
        for (int i = 0; i < chimerList.Count; i++)
        {
            chimerList[i].Chime();
            yield return new WaitForSecondsRealtime(tempo / (40 * GetMasterPitch()));
        }

        StartCoroutine(Chimers());
    }
    
    private void TapTempo()
    {
        
        // if this is the first one just mark down when it was clicked
        if (startTime == 0)
        {
            startTime = Time.time;
        }
        else
        {
            float elapsedTime = Time.time - startTime;
            timeIntervals.Add(elapsedTime);
            startTime = Time.time;
        }
        
        // if we have all three time intervals, spit out that BPM!
        if (timeIntervals.Count == 3)
        {
            float totalTime = 0;
            for (int i = 0; i < timeIntervals.Count; i++)
            {
                totalTime += timeIntervals[i];
            }

            // Set the parameters based on this new Tempo
            tempo = (totalTime / timeIntervals.Count) * 8f;
            Debug.Log("BPM=" +tempo);
            leftHand.SendFloat("tempo", tempo);
            rightHand.SendFloat("tempo", tempo);

            // for the delays we need milliseconds
            float ms = tempo * 10f;
            mainMixer.SetFloat("sfx_delay", ms);
            mainMixer.SetFloat("rhythmSynth_delay", ms);
            // And Go to the Questions Portion
            StartQuestions();
        }
    }

    public void StartQuestions()
    {
        // Go to main section of music
        beginQuestions.TransitionTo(5f);
            
        // And Start the chimers going at regular intervals, based on tempo
        StartCoroutine(Chimers());
    }
    
}
