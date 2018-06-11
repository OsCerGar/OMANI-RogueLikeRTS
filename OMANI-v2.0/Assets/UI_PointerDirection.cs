using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PointerDirection : MonoBehaviour
{

    GameObject pointer, dots;
    Animator dotsAnim;
    bool timer;
    float timerAnimation;

    // Use this for initialization
    void Start()
    {
        dots = this.transform.Find("Dots").gameObject;
        pointer = this.transform.Find("Pointer").gameObject;
        dotsAnim = dots.GetComponent<Animator>();
    }

    public void Click()
    {
        dots.transform.parent = this.transform;
        dots.transform.localPosition = new Vector3(0, 0, 0);
        dots.transform.localRotation = Quaternion.identity;
        dots.transform.parent = null;
        Debug.Log(dots.transform.parent);
        dotsAnim.SetTrigger("Play");
        timer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            timerAnimation += Time.deltaTime;
        }

        if (timerAnimation > 1f)
        {
            timerAnimation = 0;
            dots.transform.parent = this.transform;
            dots.transform.localPosition = new Vector3(0, 0, 0);
            dots.transform.localRotation = Quaternion.identity;
            timer = false;
        }
    }
}
