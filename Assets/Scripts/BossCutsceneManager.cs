using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutsceneManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayCutscene1Devil_1()
    {
        audioSource.clip = clip1;

        audioSource.Play();
    }
    public void PlayCutscene1Player_1()
    {
        audioSource.clip = clip2;

        audioSource.Play();
    }

    public void StartBossFight()
    {
        // do stuffs here..
    }
}
