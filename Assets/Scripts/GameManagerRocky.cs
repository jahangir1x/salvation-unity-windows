using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerRocky : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    public bool inMainMenu = false;
    public float spikeDamageInterval = 1f;
    public float spikeDamageAmount = 20f;

    public static GameManagerRocky instance;
    public static Transform PlayerTransform;
    public static Transform PlayerParentTransform;
    public static PlayerHealth playerHealth;

    public bool isGameOver;                            //Is the game currently over?
    public static int PlayerLayer;
    public static int PlatformLayer;
    public static int PlatformShakeAnimID;
    public static int PlatformVanishAnimID;
    public static int PlayerDetectedAnimBoolID;
    public static int NullAnimID;
    public static int IsDoorSwitchOnAnimBool;
    public static int IsDoorOpenAnimBool;
    public static int CreditsClickTrigger;
    public static int QuitClickTrigger;
    public static int ResetGameClickTrigger;
    public static int ResetGameConfirmationNoClickTrigger;
    public static int QuitGameConfirmationNoClickTrigger;
    public static int CreditsBackClickTrigger;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        if (!inMainMenu)
        {
            TransitionManager.instance.PlayAnimSwitch("transitionclose");
        }
        if (playerTransform != null)
        {

            PlayerTransform = instance.playerTransform;
            PlayerParentTransform = playerTransform.parent;
            playerHealth = playerTransform.GetComponent<PlayerHealth>();
        }

        PlayerLayer = LayerMask.NameToLayer("Player");
        PlatformLayer = LayerMask.NameToLayer("Platform");
        PlatformShakeAnimID = Animator.StringToHash(Constants.PLATFORM_SHAKE_ANIM);
        NullAnimID = Animator.StringToHash(Constants.NULL);
        PlatformVanishAnimID = Animator.StringToHash(Constants.PLATFORM_VANISH_ANIM);
        PlayerDetectedAnimBoolID = Animator.StringToHash(Constants.PLAYER_DETECTED_BOOL_ANIM);
        IsDoorSwitchOnAnimBool = Animator.StringToHash(Constants.IS_DOOR_SWITCH_ON_ANIM_BOOL);
        IsDoorOpenAnimBool = Animator.StringToHash(Constants.IS_DOOR_OPEN_ANIM_BOOL);
        CreditsClickTrigger = Animator.StringToHash(Constants.CREDITS_CLICK_ANIM_TRIGGER);
        QuitClickTrigger = Animator.StringToHash(Constants.QUIT_CLICK_ANIM_TRIGGER);
        ResetGameClickTrigger = Animator.StringToHash(Constants.RESET_GAME_CLICK_ANIM_TRIGGER);
        ResetGameConfirmationNoClickTrigger = Animator.StringToHash(Constants.RESET_NO_CLICK_ANIM_TRIGGER);
        QuitGameConfirmationNoClickTrigger = Animator.StringToHash(Constants.QUIT_NO_CLICK_ANIM_TRIGGER);
        CreditsBackClickTrigger = Animator.StringToHash(Constants.CREDITS_BACK_CLICK_ANIM_TRIGGER);
    }

    public static bool IsGameOver()
    {
        //If there is no current Game Manager, return false
        if (instance == null)
            return false;

        //Return the state of the game
        return instance.isGameOver;
    }

    public void LoadNextScene()
    {
        TransitionManager.instance.PlayAnimSwitch("transition");
        Invoke("LoadNextSceneRoutine", 1f);
    }

    private void LoadNextSceneRoutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // SceneManager.LoadScene(1);
    }

    public void RestartScene()
    {
        TransitionManager.instance.PlayAnimSwitch("transition");
        Invoke("LoadCurrentSceneRoutine", 1f);
    }

    private void LoadCurrentSceneRoutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // SceneManager.LoadScene(1);
    }

}
