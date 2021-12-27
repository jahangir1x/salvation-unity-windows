using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MainMenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer SFX_Mixture;
    [SerializeField] private Animator mainMenuCanvasAnimator;
    [SerializeField] private UnityEngine.UI.Slider masterVolumeSlider;

    private void ButtonPressed()
    {
        audio_Manager.instance.Play("button");
    }

    private void Start()
    {
        audio_Manager.instance.Play("MainMenu");
        // TransitionManager.instance.PlayANimLoad("transition");
    }

    public void StartClick()
    {
        GameManagerRocky.instance.LoadNextScene();

        Debug.Log("start game");
    }



    public void ResetGameYesClick()
    {
        Debug.Log("reset game progress");
    }

    public void QuitGameYesClick()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    public void VolumeSave()
    {
        audioMixer.SetFloat("volume", Mathf.Log10(masterVolumeSlider.value) * 20f);
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(masterVolumeSlider.value) * 20f);
        Debug.Log("volume: " + masterVolumeSlider.value);
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
        Debug.Log("reset game");
        mainMenuCanvasAnimator.SetTrigger(GameManagerRocky.ResetGameClickTrigger);
    }

    public void CreditsBackClick()
    {
        Debug.Log("back credits");
        // mainMenuCanvasAnimator.SetTrigger(GameManagerRocky.CreditsBackClickTrigger);
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
