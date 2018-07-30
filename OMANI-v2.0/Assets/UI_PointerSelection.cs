using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PointerSelection : MonoBehaviour
{

    [SerializeField]
    GameObject selectionAnimation, firstAnimation;
    [SerializeField]
    public ParticleSystem selectionAnimationParticleSystem, firstAnimationParticleSystem, fadeAnimationPArticleSystem;
    [SerializeField]
    private ParticleSystemRenderer selectionAnimationParticleRenderer;
    [SerializeField]
    Material PrioritySelectedMaterial, regularMaterial;

    [SerializeField]
    bool fading, selected, temporalUIfix;
    float timerAnimation;
    Army commander;
    LookDirectionsAndOrder lookDirections;

    // Use this for initialization
    void OnEnable()
    {
        commander = FindObjectOfType<Army>();
        lookDirections = FindObjectOfType<LookDirectionsAndOrder>();

        //Play Spawn animation
        firstAnimationParticleSystem.Play();
    }

    private void Start()
    {
        lookDirections = FindObjectOfType<LookDirectionsAndOrder>();
    }

    // Update is called once per frame
    void Update()
    {

        var lookPos = commander.transform.position - transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
        #region CircleAnimation
        if (selected)
        {
            if (selectionAnimationParticleSystem.time >= 0.38f)
            {
                selectionAnimationParticleSystem.Pause();
            }
            if (Time.time - timerAnimation > 0.4f && !temporalUIfix)
            {
                selectionAnimationParticleSystem.Simulate(0.38f);
                selectionAnimationParticleSystem.Pause();
                temporalUIfix = true;
            }
        }
        else
        {
            #region SpawnAnimation
            //If Spawn animation over
            //Arrow SetActive
            if (firstAnimationParticleSystem.time > 0.29)
            {
                firstAnimationParticleSystem.Pause();
            }
            if (lookDirections != null)
            {
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
            }
        }
        #endregion


        #endregion

        //If no longer the selectable unit, start dissapearing
        //If Spawnanimation is still alive, get time and start dissapearing animation
        //If Spawnanimation is over, Start dissapearing from full.
    }
    public void ActivateCircle()
    {
        if (!selected)
        {
            timerAnimation = Time.time;
            //Play Spawn animation
            selectionAnimationParticleSystem.Play();

            firstAnimationParticleSystem.Stop();
            firstAnimationParticleSystem.Clear();
            selected = true;
            fading = false;
            temporalUIfix = false;
        }

    }

    public void DisableCircle()
    {
        if (selected)
        {
            selected = false;
            fading = false;
            temporalUIfix = false;
            lookDirections.latestClosestTarget = null;
            selectionAnimationParticleSystem.Stop();
            selectionAnimationParticleSystem.Clear();
            this.gameObject.SetActive(false);
        }
    }

    public void PriorityMaterial()
    {
        selectionAnimationParticleRenderer.material = PrioritySelectedMaterial;
    }

    public void RegularMaterial()
    {
        selectionAnimationParticleRenderer.material = regularMaterial;
    }
}
