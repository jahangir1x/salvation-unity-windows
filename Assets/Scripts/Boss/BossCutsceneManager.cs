using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossCutsceneManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;

    [SerializeField] GameObject specialBullet;


    public PlayerInput player;
    public Boss boss;

    public Animator animator;

    private void Start()
    {
        if(PlayerPrefs.GetInt("HasCutsceneSeen", 0) == 0) // has not seen yet..
        {
            player.canMove = false;
            boss.enabled = false;

            Invoke("playAnim", 1f);
        }


    }

    void playAnim()
    {
        animator.Play("BossCutscene1");
    }

    public void ResumeEverything()
    {

        player.canMove = true;
        boss.enabled = true;

        PlayerPrefs.SetInt("HasCutsceneSeen", 1);
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

    public void PlayFinalCutscene()
    {
        // do stuffs here..
        player.canMove = false;
        //audio_Manager.instance.Stop("boss");
        animator.Play("BossCutscene2");


    }

    public void PlayFinalAudio()
    {
        audioSource.clip = clip3;

        audioSource.Play();
    }

    public void KillBoss()
    {
        player.Bullet = specialBullet;
        StartCoroutine(player.Fire());

    }

    public void EndTheGame()
    {
        animator.Play("EndGame");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
