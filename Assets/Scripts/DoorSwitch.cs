using UnityEngine;
using System.Collections;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField]
    public enum DoorType
    {
        SlowResponsive,
        FastResponsive
    }
    public DoorType currentDoorType;
    public void OpenTheGate()
    {
        doorAnimator.SetBool(GameManager.IsDoorOpenAnimBool, true);
    }
    public void CloseTheGate()
    {
        if (currentDoorType == DoorType.FastResponsive)
        {
            doorAnimator.SetBool(GameManager.IsDoorOpenAnimBool, false);
        }
        else
        {
            StartCoroutine(OpenDoorSlowly());
        }
    }

    IEnumerator OpenDoorSlowly()
    {
        yield return new WaitForSeconds(2.5f);
        doorAnimator.SetBool(GameManager.IsDoorOpenAnimBool, false);

    }

}
