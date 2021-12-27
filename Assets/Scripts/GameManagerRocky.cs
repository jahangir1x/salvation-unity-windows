using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerRocky : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    public static GameManagerRocky instance;
    public static Transform PlayerTransform;
    public static Transform PlayerParentTransform;

    public static int PlayerLayer;
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
        if (playerTransform != null)
        {
            PlayerTransform = instance.playerTransform;
            PlayerParentTransform = playerTransform.parent;
        }

        PlayerLayer = LayerMask.NameToLayer(Constants.PLAYER_LAYER);
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

}
