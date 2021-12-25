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
        DoorSwitch
    }
    public ObjectType currentObjectType;    // store type of this object.
    private MovingPlatform movingPlatform;
    private HammerObstacle hammer;
    private Animator doorSwitchAnimator;


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
                break;
            default:
                break;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentObjectType == ObjectType.DoorSwitch)
        {
            Debug.Log("triggered: " + collision);
            TriggerEnterDoorSwitch();
        }
        if (collision.gameObject.layer == GameManager.PlayerLayer)
        {
            switch (currentObjectType)
            {
                case ObjectType.MovingPlatform:
                    TriggerEnterMovingPlatform();
                    break;
                default:
                    break;
            }
        }
    }

    private void TriggerEnterDoorSwitch()
    {
        doorSwitchAnimator.Play(GameManager.DoorSwitchOnAnimID);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentObjectType == ObjectType.DoorSwitch)
        {
            TriggerExitDoorSwitch();
        }

        if (collision.gameObject.layer == GameManager.PlayerLayer)
        {
            switch (currentObjectType)
            {
                case ObjectType.MovingPlatform:
                    TriggerExitMovingPlatform();
                    break;
                default:
                    break;
            }
        }
    }

    private void TriggerExitDoorSwitch()
    {
        doorSwitchAnimator.Play(GameManager.DoorSwitchOffAnimID);
    }

    // private void TriggerEnterHammer()
    // {
    //     if (hammer.canDealDamage)
    //     {
    //          GameManager.Player_Health.ModifyHealth(-GameManager.HammerObstacleDamage);
    //     }
    // }


    private void TriggerExitMovingPlatform()
    {
        GameManager.PlayerTransform.SetParent(GameManager.PlayerParentTransform);
    }

    private void TriggerEnterMovingPlatform()
    {
        GameManager.PlayerTransform.SetParent(transform);   // change parent to move the player with the platform.
    }

}
