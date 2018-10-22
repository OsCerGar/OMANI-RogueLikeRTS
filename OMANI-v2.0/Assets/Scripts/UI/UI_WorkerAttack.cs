using UnityEngine;
using UnityEngine.UI;

public class UI_WorkerAttack : MonoBehaviour, UI_RobotAttack
{

    LineRenderer line;
    Canvas canvas;
    Image img;
    private Transform Mouse;
    float fadeCounter, fillCounter, fillTime, lineFadeCounter = 0;
    Vector3 attackingPosition;
    Quaternion rotationPosition;
    bool attacking;
    public void Hide()
    {
        canvas.enabled = false;
    }
    public void startFill(float _time)
    {
        fillTime = _time;
        fillCounter = 0;
    }
    public void Show(GameObject objective)
    {
        transform.localPosition = Vector3.zero;
        canvas.enabled = true;
        fadeCounter = 0.5f;
    }
    public void Show()
    {
        transform.localPosition = Vector3.zero;
        attacking = false;
        canvas.enabled = true;
        fadeCounter = 1f;
    }

    public void attackingUI()
    {
        attackingPosition = transform.position;
        rotationPosition = transform.rotation;

        attacking = true;

    }

    public void PreShow()
    {
        Show();
    }

    // Use this for initialization
    void Start()
    {
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
        img.color = tempColor;


        if (fillCounter < fillTime)
        {
            fillCounter += Time.deltaTime;
            Show();
        }

        //Fading for the line (Preattack)

        if (!attacking)
        {
            img.transform.LookAt(Mouse);
            img.transform.Rotate(90, img.transform.rotation.y, 0);
        }
        else
        {
            transform.position = attackingPosition;
            transform.rotation = rotationPosition;
        }
    }

}
