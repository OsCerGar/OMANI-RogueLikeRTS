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
        var positionForCanvas = objective.transform.position + (objective.transform.forward * 4);
        canvas.enabled = true;
        canvas.transform.parent = null;
        canvas.transform.forward = (transform.position - new Vector3(positionForCanvas.x, 0.1f, positionForCanvas.z)).normalized;
        canvas.transform.position = new Vector3(positionForCanvas.x, 0.1f, positionForCanvas.z);
        fadeCounter = 0.5f;
    }

    public void startFill(float _time)
    {
        fillTime = _time;
        fillCounter = 0;
    }

    // Use this for initialization
    void Start()
    {
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

        fillCounter += Time.deltaTime;
        img.fillAmount = fillCounter / fillTime;

    }
}
