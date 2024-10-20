using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{


    public Sound[] sounds;

    public Dictionary<string, Sound> mapSounds{ get; set; }

    void Awake()
    {
        mapSounds = new Dictionary<string, Sound>();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.dopplerLevel = 0;
            mapSounds.Add(s.name, s);
        }
        sounds = new Sound[]{}; 
    }

    public void Play(string sound)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.Play();
    }

    public void Play(string sound, float volume, float pitch)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = volume;
        s.source.pitch = pitch;

        s.source.Play();
    }

    public void Pause(string sound)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.Pause();
    }

    public void UnPause(string sound)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.UnPause();
    }

    public void Stop(string sound)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.Stop();
    }

    public void PlayHornEffect(float volume, float pitch)
    {
        int rand = UnityEngine.Random.Range(1, 16);
        Play("hornEffect" + rand.ToString(), volume, pitch);
    }
}