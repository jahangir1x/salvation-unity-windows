using UnityEngine;

public class WheelSwitch : MonoBehaviour
{
    [SerializeField] private RotateAround wheelPlatform;
    [SerializeField] private float wheelPlatformSpeed = 50f;

    public void StartRotation()
    {
        wheelPlatform.rotationspeed = wheelPlatformSpeed;
    }

}
