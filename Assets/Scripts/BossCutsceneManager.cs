using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        player.canMove = false;
        boss.enabled = false;

        Invoke("playAnim", 1f);

    }

    void playAnim()
    {
        animator.Play("BossCutscene1");
    }

    public void ResumeEverything()
    {

        player.canMove = true;
        boss.enabled = true;
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

}
