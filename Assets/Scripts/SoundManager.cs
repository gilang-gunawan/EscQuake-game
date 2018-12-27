using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip gameover, tone, collect;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        tone = Resources.Load<AudioClip>("tone");
        collect = Resources.Load<AudioClip>("collect");
        gameover = Resources.Load<AudioClip>("gameover");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "tone":
                audioSrc.PlayOneShot(tone);
                break;
            case "gameover":
                audioSrc.PlayOneShot(gameover);
                break;
            case "collect":
                audioSrc.PlayOneShot(collect);
                break;
        }
    }
}
