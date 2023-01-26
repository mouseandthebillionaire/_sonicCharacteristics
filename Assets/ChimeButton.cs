using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeButton : MonoBehaviour
{
    public AudioSource chime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        chime.Play();
    }
}
