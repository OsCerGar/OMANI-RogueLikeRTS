using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PointerSelection : MonoBehaviour
{

    [SerializeField]
    GameObject selectionAnimation;
    GameObject anim;
    SpriteRenderer arrow;

    bool timer;
    float timerAnimation;
    Army commander;

    // Use this for initialization
    void Start()
    {
        arrow = this.transform.Find("SelectionArrow").GetComponent<SpriteRenderer>();
        commander = FindObjectOfType<Army>();
        arrow.enabled = false;
    }

    public void OnTop()
    {
        Debug.Log("On top");
        arrow.enabled = false;
        //Debug.Break();
        anim = Instantiate(selectionAnimation, this.transform.position, this.transform.rotation);
        timer = true;
    }

    public void NotOnTop()
    {
        arrow.enabled = false;
        timer = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(commander.transform);

        if (anim != null)
        {
            anim.transform.LookAt(commander.transform);
        }
        if (timer)
        {
            timerAnimation += Time.deltaTime;
        }

        if (timerAnimation > 1f)
        {
            timerAnimation = 0;
            //Turns dots again.
            arrow.enabled = true;
            anim = null;
            timer = false;
        }
    }
}
