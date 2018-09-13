using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WorkerAttack : MonoBehaviour , UI_RobotAttack {

    LineRenderer line;
    Canvas canvas;
    Image img;

    float fadeCounter,fillCounter, fillTime;
    public void Hide()
    {
        line.enabled = false;
        canvas.enabled = false;
    }
    public void startFill(float _time)
    {
        fillTime = _time;
        fillCounter = 0;
    }
    public void Show(GameObject objective)
    {
        line.enabled = true;
        canvas.enabled = true;
        canvas.transform.parent = null;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, new Vector3(objective.transform.position.x, 0.1f, objective.transform.position.z));
        canvas.transform.forward = (new Vector3(objective.transform.position.x, 0.1f, objective.transform.position.z) - transform.position).normalized;
        canvas.transform.position = new Vector3(objective.transform.position.x, 0.1f, objective.transform.position.z);
        fadeCounter = 0.5f;
    }

    // Use this for initialization
    void Start () {
        line = GetComponentInChildren<LineRenderer>();
        canvas = GetComponentInChildren<Canvas>();
        canvas.transform.parent = null;
        img = canvas.GetComponentInChildren<Image>();
        
    }

    private void LateUpdate()
    {
        fadeCounter -= Time.deltaTime;

        var tempColor = img.color;
        tempColor.a = fadeCounter;
        img.color = tempColor;
        
        line.startColor = tempColor;
        line.endColor = tempColor;


        fillCounter += Time.deltaTime;
        img.fillAmount = fillCounter / fillTime;
    }

}
