using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArpManager : MonoBehaviour
{
    public LibPdInstance pdPatch;

    public int   noteSpread = 6;
    public int   lineLength = 16;
    public int   instrument = 0;
    public float noteSpeed  = 1;

    // Start is called before the first frame update
    void Start()
    {
        pdPatch = GetComponent<LibPdInstance>();
    }

    // Update is called once per frame
    void Update()
    {
        // Testers
        // Reset the Lead Line
        if (Input.GetKeyDown(KeyCode.A))
        {
            pdPatch.SendBang("start");
        }
        // Cycle through lengths
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateLead();
        }
    }
    
    public void UpdateLead()
    {
        pdPatch.SendFloat("noteSpread", noteSpread);
        pdPatch.SendFloat("length", lineLength);
        pdPatch.SendFloat("instrument", instrument);
        pdPatch.SendFloat("noteSpeed", noteSpeed);
    }
}
