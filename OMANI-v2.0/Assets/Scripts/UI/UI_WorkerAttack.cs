using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WorkerAttack : MonoBehaviour , UI_RobotAttack {

    LineRenderer line;
    Canvas canvas;

    public void Hide()
    {
        line.enabled = false;
        canvas.enabled = false;
    }

    public void Show(GameObject objective)
    {
        line.enabled = true;
        canvas.enabled = true;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, new Vector3(objective.transform.position.x, 0.1f, objective.transform.position.z));
        canvas.transform.position = new Vector3(objective.transform.position.x, 0.1f, objective.transform.position.z);
    }

    // Use this for initialization
    void Start () {
        line = GetComponentInChildren<LineRenderer>();
        canvas = GetComponentInChildren<Canvas>();
    }
	
}
