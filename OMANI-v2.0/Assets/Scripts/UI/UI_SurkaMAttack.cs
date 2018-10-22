using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_SurkaMAttack : MonoBehaviour, UI_RobotAttack
{

    Canvas canvas;
    Image img;

    float fadeCounter, fillCounter, fillTime;
    public void Hide()
    {
        canvas.enabled = false;
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
    public void attackingUI() { }


    public void startFill(float _time)
    {
        fillTime = _time;
        fillCounter = 0;
    }

    // Use this for initialization
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        img = canvas.GetComponentInChildren<Image>();

    }

    private void LateUpdate()
    {
        fadeCounter -= Time.deltaTime;

        var tempColor = img.color;
        tempColor.a = fadeCounter;
        img.color = tempColor;

        if (fillCounter < fillTime)
        {
            fillCounter += Time.deltaTime;
            Show();
        }
        img.fillAmount = fillCounter / fillTime;

    }
    public void PreShow()
    {
        //Had to implement
    }
}
