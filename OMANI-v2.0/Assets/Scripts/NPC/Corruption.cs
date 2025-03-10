﻿using System.Collections;
using UnityEngine;

public class Corruption : Enemy
{
    [SerializeField] ParticleSystem Slash;

    [SerializeField] LayerMask LayerMasktoAttack;
    float playerspeed;
    bool dead = false;
    Collider coll;
    public override void Update()
    {

    }
    public override void AttackHit()
    {
        Attackzone.SetActive(true);
    }
    public override void Start()
    {
        coll = GetComponent<Collider>();

        SM = GetComponentInChildren<SoundsManager>();
        anim = gameObject.GetComponent<Animator>();
        if (transform.Find("UI") != null)
        {
            if (transform.Find("UI/SelectionAnimationParent") != null)
            {
                GUI = transform.Find("UI/SelectionAnimationParent").gameObject;
                GUI_Script = transform.Find("UI/SelectionAnimationParent").GetComponent<UI_PointerSelection>();
            }
            ui_information = transform.Find("UI").gameObject;

        }
        //Get AttackZone child Somewhere 

        startLife = life;
        numbersTransform = transform.Find("UI").Find("Numbers");
        numberPool = FindObjectOfType<NumberPool>();

        quarter = Mathf.RoundToInt(maxpowerPool * 0.25f);
        half = Mathf.RoundToInt(maxpowerPool * 0.5f);
        quarterAndHalf = Mathf.RoundToInt(maxpowerPool * 0.75f);


        anim.Play("Idle", -1, Random.Range(0.0f, 1.0f));

    }
    public override void Die()
    {
        dead = true;
        laserTarget.gameObject.SetActive(false);
        //collider reset
        coll.enabled = false;
        //audio
        SM.Die();

        CharacterMovement.movement.anim.SetFloat("Speed", 1f);
        anim.SetTrigger("Die");
        StartCoroutine("DieTempo");

    }

    public override void OnEnable()
    {
        if (laserTarget != null)
        {
            laserTarget.gameObject.SetActive(true);
        }

    }

    private IEnumerator DieTempo()
    {

        MK.Toon.MKToonMaterialHelper.SetSaturation(Renderer.material, 0);
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        dead = false;
        life = startLife;
        State = "Alive";
        anim.SetTrigger("Resurrect");
        coll.enabled = true;
        laserTarget.gameObject.SetActive(true);

        MK.Toon.MKToonMaterialHelper.SetSaturation(Renderer.material, 1);
    }

    public override IEnumerator gotHit()
    {
        if (Renderer != null)
        {
            MK.Toon.MKToonMaterialHelper.SetRimIntensity(Renderer.material, 1);
            yield return new WaitForSeconds(0.3f);
            //Set the main Color of the Material to green
            MK.Toon.MKToonMaterialHelper.SetRimIntensity(Renderer.material, 0);

        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // Convert the object's layer to a bitfield for comparison
        int objLayerMask = (1 << obj.layer);
        if ((layerMask.value & objLayerMask) > 0)  // Extra round brackets required!
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!dead)
        {
            if (IsInLayerMask(other.gameObject, LayerMasktoAttack))
            {
                CharacterMovement.movement.anim.SetFloat("Speed", 1f);

            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!dead)
        {
            if (IsInLayerMask(other.gameObject, LayerMasktoAttack))
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    if (other.GetComponent<Corruption>() == null)
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        {
                            anim.SetTrigger("Attack");
                        }
                    }
                }

                //check for player and robot
                if (other.CompareTag("Player"))
                {
                    CharacterMovement.movement.anim.SetFloat("Speed", 0.6f);

                }
            }

        }
    }
}
