using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PointerDirection : MonoBehaviour
{

    SpriteRenderer dots;
    [SerializeField]
    ParticleSystem dotsAnimation;
    bool timer;
    float timerAnimation;

    // Use this for initialization
    void Start()
    {
        dots = this.transform.Find("Dots").GetComponent<SpriteRenderer>();
    }

    public void Click()
    {
        if (dots != null)
        {
            dots.enabled = false;

            Instantiate(dotsAnimation, dots.transform.position, dots.transform.rotation);

            timer = true;
        }
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
            //Turns dots again.
            dots.enabled = true;
            timer = false;
        }
    }
}
