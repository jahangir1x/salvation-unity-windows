using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class audio_Manager : MonoBehaviour
{
    public Sounds[] sounds;

    public AudioClip gameOverClip;

    public static audio_Manager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = s.masterMixtureGroup;
            s.source.pitch = s.pinch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex > 1 && SceneManager.GetActiveScene().buildIndex < 6)
            Play("bg_sound");
        else if (SceneManager.GetActiveScene().buildIndex == 6)
            Play("boss");
    }
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }


}
