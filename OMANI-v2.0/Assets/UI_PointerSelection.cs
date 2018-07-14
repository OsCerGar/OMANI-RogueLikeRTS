using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PointerSelection : MonoBehaviour
{

    [SerializeField]
    GameObject selectionAnimation, firstAnimation;
    [SerializeField]
    ParticleSystem selectionAnimationParticleSystem, firstAnimationParticleSystem;
    GameObject anim;
    GameObject arrow, selected;

    bool timer;
    float timerAnimation;
    Army commander;
    LookDirectionsAndOrder lookDirections;
    // Use this for initialization
    void OnEnable()
    {
        arrow = this.transform.Find("SelectionArrow").gameObject;
        selected = this.transform.Find("SelectedCircle").gameObject;
        commander = FindObjectOfType<Army>();
        lookDirections = FindObjectOfType<LookDirectionsAndOrder>();
        arrow.SetActive(false);
        selected.SetActive(false);

        //Play Spawn animation

    }

    // Update is called once per frame
    void Update()
    {

        var lookPos = commander.transform.position - transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;


        #region SpawnAnimation
        //If Spawn animation over
        //Arrow SetActive

        //If no longer 
        if (lookDirections.closestTarget != null)
        {
            if (lookDirections.closestTarget != lookDirections.latestClosestTarget)
            {
                //Play Spawn animation
            }
        }
        #endregion

        //If no longer the selectable unit, start dissapearing
        //If Spawnanimation is still alive, get time and start dissapearing animation
        //If Spawnanimation is over, Start dissapearing from full.

        if (anim != null)
        {
            anim.transform.LookAt(commander.transform);

            //Should be the npc.
            anim.transform.position = this.transform.position;
        }
        /*
        if (timer)
        {
            timerAnimation += Time.deltaTime;
        }
        */
        /*
        if (firstAnimationParticleSystem.IsAlive())
        {
            timerAnimation = 0;
            //Turns dots again.
            arrow.SetActive(true);
            anim = null;
            timer = false;
        }
        */
    }
    public void OnTop()
    {
        Debug.Log("On top");
        arrow.SetActive(false);
        //Debug.Break();
        //anim = Instantiate(firstAnimation, this.transform.position, this.transform.rotation);
        timer = true;
    }

    public void NotOnTop()
    {
        arrow.SetActive(false);
        timer = true;
    }

    public void Selected()
    {
        arrow.SetActive(false);
        anim = Instantiate(selectionAnimation, this.transform.position, this.transform.rotation);
    }


}
