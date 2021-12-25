using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    public static GameManager instance;
    public static Transform PlayerTransform;
    public static Transform PlayerParentTransform;

    public static int PlayerLayer;
    public static int PlatformShakeAnimID;
    public static int PlatformVanishAnimID;
    public static int PlayerDetectedAnimBoolID;
    public static int NullAnimID;
    public static int DoorSwitchOnAnimID;
    public static int DoorSwitchOffAnimID;

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
        PlayerLayer = LayerMask.NameToLayer(Constants.PLAYER_LAYER);

        PlayerTransform = instance.playerTransform;
        PlayerParentTransform = playerTransform.parent;
        PlatformShakeAnimID = Animator.StringToHash(Constants.PLATFORM_SHAKE_ANIM);
        NullAnimID = Animator.StringToHash(Constants.NULL);
        PlatformVanishAnimID = Animator.StringToHash(Constants.PLATFORM_VANISH_ANIM);
        PlayerDetectedAnimBoolID = Animator.StringToHash(Constants.PLAYER_DETECTED_BOOL_ANIM);
        DoorSwitchOnAnimID = Animator.StringToHash(Constants.DOOR_SWITCH_ON_ANIM);
        DoorSwitchOffAnimID = Animator.StringToHash(Constants.DOOR_SWITCH_OFF_ANIM);



    }

}
