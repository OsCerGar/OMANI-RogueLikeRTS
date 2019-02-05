using UnityEngine;

public class Tutorial_PlayerLock : MonoBehaviour
{
    public OMANINPUT controls;

    // Start is called before the first frame update
    void Start()
    {
        controls.PLAYER.WASD.Disable();
        controls.PLAYER.Joystick.Disable();

    }
}
