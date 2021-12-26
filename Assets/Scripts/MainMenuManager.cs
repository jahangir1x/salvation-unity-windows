using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Animator mainMenuCanvasAnimator;
    [SerializeField] private UnityEngine.UI.Slider masterVolumeSlider;

    public void StartClick()
    {
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
        audioMixer.SetFloat("masterVolume", Mathf.Log10(masterVolumeSlider.value) * 20f);
        Debug.Log("volume: " + masterVolumeSlider.value);
    }

    public void QuitClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManager.QuitClickTrigger);
    }

    public void CreditsClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManager.CreditsClickTrigger);
    }

    public void ResetGameClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManager.ResetGameClickTrigger);
    }

    public void CreditsBackClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManager.CreditsBackClickTrigger);
    }

    public void ResetGameConfirmationNoClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManager.ResetGameConfirmationNoClickTrigger);
    }

    public void QuitConfirmationNoClick()
    {
        mainMenuCanvasAnimator.SetTrigger(GameManager.QuitGameConfirmationNoClickTrigger);
    }
}
