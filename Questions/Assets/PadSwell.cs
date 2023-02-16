using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PadSwell : MonoBehaviour
{
    private AudioSource a;
    
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<AudioSource>();
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        float r = Random.Range(5, 30);
        int p = Random.Range(1, 6);
        a.pitch = 0.25f * p;
        yield return new WaitForSeconds(r);
        a.Play();
        yield return new WaitUntil(() => a.isPlaying != true);
        StartCoroutine(Play());
        yield return null;
    }
    
}
