using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public enum ObjectType
    {
        MovingPlatform,
        Hammer,
        DoorSwitch,
        WheelSwitch,
        SpikeArea,
        CameraBullet
    }
    public ObjectType currentObjectType;    // store type of this object.
    private MovingPlatform movingPlatform;
    private HammerObstacle hammer;
    private Animator doorSwitchAnimator;
    private Animator wheelSwitchAnimator;
    private DoorSwitch doorSwitch;
    private WheelSwitch wheelSwitch;
    private CameraBullet cameraBullet;
    private float timeElapsedSinceSpikeDamage;
    private bool playerExitedSpike = true;

    private void Update()
    {
        if (currentObjectType == ObjectType.SpikeArea)
        {
            if ((timeElapsedSinceSpikeDamage > GameManagerRocky.instance.spikeDamageInterval) && (playerExitedSpike == false))
            {
                timeElapsedSinceSpikeDamage = 0;
                GameManagerRocky.playerHealth.TakeDamage(GameManagerRocky.instance.spikeDamageAmount);
            }
            timeElapsedSinceSpikeDamage += Time.deltaTime;
            if (timeElapsedSinceSpikeDamage > 10000f)
            {
                timeElapsedSinceSpikeDamage = 1000f;
            }
        }
    }

    private void Start()
    {

        switch (currentObjectType)
        {
            case ObjectType.MovingPlatform:
                movingPlatform = GetComponent<MovingPlatform>();
                break;
            case ObjectType.Hammer:
                hammer = transform.parent.GetComponent<HammerObstacle>();
                break;
            case ObjectType.DoorSwitch:
                doorSwitchAnimator = transform.GetComponent<Animator>();
                doorSwitch = GetComponent<DoorSwitch>();
                break;
            case ObjectType.WheelSwitch:
                wheelSwitchAnimator = transform.GetComponent<Animator>();
                wheelSwitch = GetComponent<WheelSwitch>();
                break;
            case ObjectType.CameraBullet:
                cameraBullet = GetComponent<CameraBullet>();
                break;
            default:
                break;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentObjectType == ObjectType.DoorSwitch)
        {
            TriggerEnterDoorSwitch();
        }
        else if (currentObjectType == ObjectType.WheelSwitch)
        {
            TriggerEnterWheelSwitch();
        }
        else if (currentObjectType == ObjectType.CameraBullet)
        {
            if (collision.gameObject.layer == GameManagerRocky.PlatformLayer || collision.gameObject.layer == GameManager.PlayerLayer)
            {
                TriggerEnterCameraBullet();
            }
        }

        else if (collision.gameObject.layer == GameManagerRocky.PlayerLayer)
        {
            switch (currentObjectType)
            {
                case ObjectType.MovingPlatform:
                    TriggerEnterMovingPlatform();
                    break;
                case ObjectType.SpikeArea:
                    TriggerEnterSpikeArea();
                    break;
                // case ObjectType.CameraBullet:
                //     TriggerEnterCameraBullet();
                //     break;
                case ObjectType.Hammer:
                    TriggerEnterHammer();
                    break;
                default:
                    break;
            }
        }
    }

    private void TriggerEnterCameraBullet()
    {
        // Debug.Log("<insert player damage due to camera bullet code here >");
        Destroy(gameObject);
    }

    private void TriggerEnterSpikeArea()
    {
        playerExitedSpike = false;
        // Debug.Log("<insert player damage code here>");
    }

    private void TriggerEnterWheelSwitch()
    {
        wheelSwitchAnimator.SetBool(GameManagerRocky.IsDoorSwitchOnAnimBool, true);
        wheelSwitch.StartRotation();
    }

    private void TriggerEnterDoorSwitch()
    {
        doorSwitchAnimator.SetBool(GameManagerRocky.IsDoorSwitchOnAnimBool, true);
        doorSwitch.OpenTheGate();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentObjectType == ObjectType.DoorSwitch)
        {
            TriggerExitDoorSwitch();
        }

        if (collision.gameObject.layer == GameManagerRocky.PlayerLayer)
        {
            switch (currentObjectType)
            {
                case ObjectType.MovingPlatform:
                    TriggerExitMovingPlatform();
                    break;
                case ObjectType.SpikeArea:
                    TriggerExitSpikeArea();
                    break;
                default:
                    break;
            }
        }
    }

    private void TriggerExitSpikeArea()
    {
        playerExitedSpike = true;
    }

    private void TriggerExitDoorSwitch()
    {
        doorSwitchAnimator.SetBool(GameManagerRocky.IsDoorSwitchOnAnimBool, false);
        doorSwitch.CloseTheGate();
    }

    private void TriggerEnterHammer()
    {
        Debug.Log("hammer damage");
        GameManagerRocky.playerHealth.TakeDamage(1000f);
        // if (hammer.canDealDamage)
        // {
        //     //  GameManager.Player_Health.ModifyHealth(-GameManager.HammerObstacleDamage);
        //     Debug.Log("hammer damage2");
        // }
    }


    private void TriggerExitMovingPlatform()
    {
        GameManagerRocky.PlayerTransform.SetParent(GameManagerRocky.PlayerParentTransform);
    }

    private void TriggerEnterMovingPlatform()
    {
        GameManagerRocky.PlayerTransform.SetParent(transform);   // change parent to move the player with the platform.
    }

}
