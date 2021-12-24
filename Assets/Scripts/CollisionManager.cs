using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public enum ObjectType
    {
        MovingPlatform
    }
    public ObjectType currentObjectType;    // store type of this object.
    private MovingPlatform movingPlatform;

    private void Start()
    {

        switch (currentObjectType)
        {
            case ObjectType.MovingPlatform:
                movingPlatform = GetComponent<MovingPlatform>();
                break;
            default:
                break;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
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

    private void OnTriggerExit2D(Collider2D collision)
    {
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

    private void TriggerExitMovingPlatform()
    {
        GameManager.PlayerTransform.SetParent(GameManager.PlayerParentTransform);
    }

    private void TriggerEnterMovingPlatform()
    {
        GameManager.PlayerTransform.SetParent(transform);   // change parent to move the player with the platform.
    }

}
