using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager S;

    public AudioMixerSnapshot[] mainInstrumentVolume;
    public AudioMixer           mainMixer;
    
    void Awake() {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSoundtrack(int effect, int answer)
    {
        int a = answer;
        Debug.Log(a);
        
        // maybe for now, let's update based on what# question you're on
        // probably not good for later (especially with a branching structure) but a place to start!
        
        // question decides which effect to employ
        switch (effect)
        {
            case 0:
                Debug.Log("Question 0");
                // Choose a dominant lead instrument
                if (a < 4)
                {
                    // piano
                    mainInstrumentVolume[0].TransitionTo(2);
                }
                else if (a > 6)
                {
                    // guitar
                    mainInstrumentVolume[2].TransitionTo(2);
                }
                else
                {
                    // vocal
                    mainInstrumentVolume[1].TransitionTo(2);
                }
                break;
            case 1:
                Debug.Log("Question 1");
                // Example of Delay Wetmix
                float delayMix = a * 10f;
                mainMixer.SetFloat("echoMix", delayMix);
                break;
            case 2:
                Debug.Log("Question 2");
                // Example of Delay MS
                float delayAmt = a * 555.0f;
                mainMixer.SetFloat("echoDelay", delayAmt);
                break;
            case 3:
                Debug.Log("Question 3");
                // Example of Overall Distortion
                float distortion = a * .10f;
                float volume = 20 - (a * 5);
                mainMixer.SetFloat("distortionLevel", distortion);
                mainMixer.SetFloat("leadVolume", volume);
                break;
            case 4:
                Debug.Log("Question 4");
                // Example of Pitching
                float pitch = a * .10f;
                mainMixer.SetFloat("leadPitch", pitch);
                break;
            default:
                break;
        }
    }
}
