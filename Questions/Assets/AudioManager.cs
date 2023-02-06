using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager S;
    
    void Awake() {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSoundtrack() {
        // Do something
        Debug.Log("soundtrack updated");
    }
}
