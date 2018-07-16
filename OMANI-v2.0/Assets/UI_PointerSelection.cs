using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PointerSelection : MonoBehaviour
{

    [SerializeField]
    GameObject selectionAnimation, firstAnimation;
    [SerializeField]
    ParticleSystem selectionAnimationParticleSystem, firstAnimationParticleSystem, fadeAnimationPArticleSystem;
    GameObject anim;
    GameObject arrow, selected;

    bool timer, fading;
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
        firstAnimationParticleSystem.Play();
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

        if (firstAnimationParticleSystem.time > 0.29)
        {
            firstAnimationParticleSystem.Pause();
        }

        //If no longer 
        if (lookDirections.closestTarget == null)
        {
            if (!fading)
            {
                fading = true;

                fadeAnimationPArticleSystem.Simulate(0.3f - firstAnimationParticleSystem.time);
                fadeAnimationPArticleSystem.Play();

                firstAnimationParticleSystem.Stop();
                firstAnimationParticleSystem.Clear();
            }

            else
            {
                if (fadeAnimationPArticleSystem.time >= 0.29)
                {
                    fading = false;
                    this.gameObject.SetActive(false);
                    lookDirections.latestClosestTarget = null;
                }
            }

        }

        else
        {
            if (lookDirections.UISelectionSpawned != this.gameObject)
            {

                if (!fading)
                {

                    fading = true;
                    fadeAnimationPArticleSystem.Simulate(0.3f - firstAnimationParticleSystem.time);
                    fadeAnimationPArticleSystem.Play();

                    firstAnimationParticleSystem.Stop();
                    firstAnimationParticleSystem.Clear();

                }
                else
                {
                    if (fadeAnimationPArticleSystem.time > 0.28)
                    {
                        fading = false;
                        this.gameObject.SetActive(false);
                    }
                }
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
    }
    public void OnTop()
    {
        arrow.SetActive(false);
        //Debug.Break();
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
