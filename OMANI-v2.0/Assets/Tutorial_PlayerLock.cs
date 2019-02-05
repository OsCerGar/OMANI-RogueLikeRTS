using UnityEngine;

public class Tutorial_PlayerLock : MonoBehaviour
{
    public OMANINPUT controls;

    [SerializeField]
    private InverseKinematics leg1, leg2, leg3, leg4;
    // Start is called before the first frame update
    void Start()
    {
        controls.PLAYER.WASD.Disable();
        controls.PLAYER.Joystick.Disable();
    }

    public void LegRelease(int _leg)
    {
        switch (_leg)
        {
            case 1:
                //Activate Leg
                leg1.enabled = true;
                break;
            case 2:
                leg2.enabled = true;
                break;
            case 3:
                leg3.enabled = true;
                break;
            case 4:
                leg4.enabled = true;
                break;
        }

        if (leg1.enabled && leg2.enabled && leg3.enabled && leg4.enabled)
        {
            controls.PLAYER.WASD.Enable();
            controls.PLAYER.Joystick.Enable();
        }
    }
}
