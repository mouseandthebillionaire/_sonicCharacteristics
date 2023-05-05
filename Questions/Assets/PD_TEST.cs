using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PD_TEST : MonoBehaviour
{
    public LibPdInstance pdpatch;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            pdpatch.SendBang("start");
            Debug.Log("Start from Unity");
        }
    }
}
