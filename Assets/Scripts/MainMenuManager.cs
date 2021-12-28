using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer SFX_Mixture;
    [SerializeField] private Animator mainMenuCanvasAnimator;
    [SerializeField] private Slider masterVolumeSlider;

    private void ButtonPressed()
    {
        audio_Manager.instance.Play("button");
    }

    private void Start()
    {
        audio_Manager.instance.Play("MainMenu");
    }

    public void StartClick()
    {
        GameManagerRocky.instance.LoadNextScene();

    }



    public void ResetGameYesClick()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGameYesClick()
    {
        Application.Quit();
    }

    public void VolumeSave()
    {
        audioMixer.SetFloat("volume", Mathf.Log10(masterVolumeSlider.value) * 20f);
        SFX_Mixture.SetFloat("sfxVolume", Mathf.Log10(masterVolumeSlider.value) * 20f);
    }

    public void QuitClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManagerRocky.QuitClickTrigger);
    }

    public void CreditsClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManagerRocky.CreditsClickTrigger);
    }

    public void ResetGameClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManagerRocky.ResetGameClickTrigger);
    }

    public void CreditsBackClick()
    {

        mainMenuCanvasAnimator.SetTrigger("Credits back click");

    }

    public void ResetGameConfirmationNoClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManagerRocky.ResetGameConfirmationNoClickTrigger);
    }

    public void QuitConfirmationNoClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManagerRocky.QuitGameConfirmationNoClickTrigger);
    }
}
