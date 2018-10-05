using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WorkerAttack : MonoBehaviour , UI_RobotAttack {

    LineRenderer line;
    Canvas canvas;
    Image img;
    private Transform Mouse;
    float fadeCounter,fillCounter, fillTime, lineFadeCounter = 0;
    public void Hide()
    {
        //line.enabled = false;
        //canvas.enabled = false;
    }
    public void startFill(float _time)
    {
        fillTime = _time;
        fillCounter = 0;
    }
    public void Show(GameObject objective)
    {
        canvas.enabled = true;
        fadeCounter = 0.5f;
    }
    public void Show()
    {
        canvas.enabled = true;
        fadeCounter = 0.5f;
    }
   
    public void PreShow()
    {
        Show();
    }

    // Use this for initialization
    void Start () {
        line = GetComponentInChildren<LineRenderer>();
        canvas = GetComponentInChildren<Canvas>();
        img = canvas.GetComponentInChildren<Image>();
        Mouse = GameObject.Find("PointerDirection").transform;
    }

    private void LateUpdate()
    {
        //Fading for the attack 
        fadeCounter -= Time.deltaTime;

        var tempColor = img.color;
        tempColor.a = fadeCounter;
        //img.color = tempColor;
        

        if (fillCounter < fillTime)
        {
            fillCounter += Time.deltaTime;
            Show();
        }

        //img.fillAmount = fillCounter / fillTime;

        //Fading for the line (Preattack)

       
        img.transform.LookAt(Mouse);
        img.transform.Rotate(90, img.transform.rotation.y, 0);
        /*
         *  LineRenderer Stuff
        if (lineFadeCounter > 0)
        {

            lineFadeCounter -= Time.deltaTime;

            line.transform.LookAt(Mouse);
            line.transform.Rotate(line.transform.rotation.x, 0, line.transform.rotation.z);



        }

        var endlinetempColor = line.endColor;
        endlinetempColor.a = lineFadeCounter;
        line.endColor = endlinetempColor;
        line.startColor = endlinetempColor;
        */
    }

}
