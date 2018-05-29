using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    private float slowdownFactor = 0.1f;
    private float slowdownLength = 10f;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        //Time.fixedDeltaTime = Time.timeScale;

        if (Input.GetKey("1"))
        {
            SlowMotion();
        }
    }

    public void SlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
    }

}
