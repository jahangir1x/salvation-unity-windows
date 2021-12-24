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
    public static int NullAnimID;

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

    }

}
