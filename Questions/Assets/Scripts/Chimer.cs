using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimer : MonoBehaviour
{
    private AudioSource chime;

    private float startTime;
    public float endTime;

    void Awake()
    {
        startTime = Time.time;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        chime = GetComponentInChildren<AudioSource>();
        int r = Random.Range(0, 4);
        float pitchShift = 1 + (0.25f * r);
        chime.pitch = pitchShift;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Chime();
        }
    }

    public void Chime()
    {
        chime.Play();
    }
}
