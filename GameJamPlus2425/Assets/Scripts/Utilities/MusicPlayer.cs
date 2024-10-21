using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] musicToStart;
    public string[] musicToStop;

    void Start()
    {
        foreach (string music in musicToStart)
        {
            if (AudioManager.Instance) AudioManager.Instance.Play(music);
        }
    }

    void Awake()
    {
        //AudioManager.Instance.Play(music);
    }

    void OnDestroy()
    {
        foreach (string music in musicToStop)
        {
            if(AudioManager.Instance) AudioManager.Instance.Stop(music);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
