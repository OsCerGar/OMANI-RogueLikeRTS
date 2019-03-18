using System.Collections;
using UnityEngine;

public class CorruptionBoss : Enemy
{
    [SerializeField] ParticleSystem Slash;

    [SerializeField] LayerMask LayerMasktoAttack;
    float playerspeed;
    CharacterMovement CM;
    bool dead = false;
    Collider coll;
    [SerializeField]CleanCorruption Clean;
    public override void Update()
    {

    }
    public override void AttackHit()
    {
        Attackzone.SetActive(true);
    }
    public override void Start()
    {
        CM = FindObjectOfType<CharacterMovement>();
        coll = GetComponent<Collider>();
        if (CM != null)
        {
            playerspeed = CM.originalSpeed;
        }

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

        CM.speed = playerspeed;
        anim.SetTrigger("Die");
        Clean.enabled = true;

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
    private void OnTriggerEnter(Collider other)
    {
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
                if (CM != null)
                {
                    CM.speed = playerspeed;
                }
                else
                {
                    CM = FindObjectOfType<CharacterMovement>();
                }
            }

        }
    }

    public override void OnEnable()
    {
        if (laserTarget != null)
        {
            laserTarget.gameObject.SetActive(true);
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
                    if (CM != null)
                    {
                        CM.speed = 2f;
                    }
                    else
                    {
                        CM = FindObjectOfType<CharacterMovement>();
                        if (CM.speed > 2f)
                        {
                            playerspeed = CM.originalSpeed;
                        }
                    }
                }
            }

        }
    }
}
