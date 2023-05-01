using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimer : MonoBehaviour
{
    private AudioSource chime;
    private AudioClip   clip;

    private float startTime;
    public float endTime;

    void Awake()
    {
        chime = GetComponentInChildren<AudioSource>();
        int note = Random.Range(0, 6);
        string clipName = "chimer/chimer_" + note;
        clip = Resources.Load<AudioClip>(clipName);
        int octave = Random.Range(1, 3);
        chime.pitch = octave;
        
        startTime = Time.time;
        endTime = Time.time + 4;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Chime();
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
        chime.PlayOneShot(clip, 0.7f);
    }

    private IEnumerator ChimeRepeater()
    {
        Chime();
        yield return new WaitForSeconds(8);
        StartCoroutine(ChimeRepeater());
    }
}
