using UnityEngine;
using UnityEngine.Timeline;

public class SlowMo : MonoBehaviour
{
    //SLOWMO
    private float slowdownFactor = 0.1f;
    private float slowdownLength = 1.5f;
    private bool active = false;

    private void OnEnable()
    {
        // if inactive becomes active and loads the slowmo postfx added in inspector.
        active = true;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

    }
    private void OnDisable()
    {
        // if already active becomes inactive and loads the regular postfx.
        active = false;
        Time.fixedDeltaTime = 0.02F;
        Time.timeScale = 1f;
    }
}
