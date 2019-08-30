using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityStandardAssets.Characters.ThirdPerson;
public class Player : NPC
{
    /*
    public delegate void OnDeath();
    public static event OnDeath playerDead;
    */

    [SerializeField] PlayableDirector Director;
    bool protection;
    //Inputs
    public override void Start()
    {
        TPC = GetComponent<ThirdPersonCharacter>();
        SM = GetComponentInChildren<SoundsManager>();
        peopl = LayerMask.NameToLayer("People");
        //We get all behaviourTrees
        Nav = gameObject.GetComponent<NavMeshAgent>();
        circle = gameObject.GetComponentInChildren<SpriteRenderer>();
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
        if (Nav != null)
        {
            Nav.updateRotation = false;
            // Nav.updatePosition = false;
        }
        numberPool = FindObjectOfType<NumberPool>();
        UI_Attack = GetComponentInChildren<UI_RobotAttack>();

        quarter = Mathf.RoundToInt(maxpowerPool * 0.25f);
        half = Mathf.RoundToInt(maxpowerPool * 0.5f);
        quarterAndHalf = Mathf.RoundToInt(maxpowerPool * 0.75f);
    }
    void Awake()
    {
        boyType = "Swordsman";

    }

    public override void Die()
    {
        Director.Play();
    }
    public override void TakeDamage(int damage, Color damageType, Transform transform)
    {
        //He dies if life lowers 
        //TODO : Make this an animation, and make it so that it swaps his layer and tag to something neutral
        numberPool.NumberSpawn(numbersTransform, damage, Color.red, numbersTransform.gameObject, false);

        if (!protection)
        {
            if (Powers.powers.armor > 0)
            {
                StartCoroutine(gotHit());
                anim.SetTrigger("GetHit");
                Powers.powers.reduceAsMuchPower(damage);
                PlayerInputInterface.player.SetVibration(0, 1f, 0.25f, false);
                PlayerInputInterface.player.SetVibration(1, 1f, 0.25f, false);

                if (Powers.powers.armor < 1)
                {
                    StartCoroutine(DamageProtection());
                    StartCoroutine(CoolDown());
                }

            }

            else
            {
                //provisional :D
                Die();
                state = "Dead";
            }

        }

    }

    IEnumerator CoolDown()
    {
        PlayerInputInterface.player.SetVibration(0, 1f, 1f, false);
        PlayerInputInterface.player.SetVibration(1, 1f, 1f, false);
        PlayerInputInterface.player.SetDS4Lights(new Color(1f, 0f, 0.0f, 1f));

        Powers.powers.enabled = false;
        CharacterMovement.movement.speed = 0;
        yield return new WaitForSeconds(5f);
        if (state != "Dead")
        {
            CharacterMovement.movement.speed = CharacterMovement.movement.originalSpeed;
            Powers.powers.enabled = true;
            Powers.powers.armor = 25;
        }

    }

    IEnumerator DamageProtection()
    {

        protection = true;
        yield return new WaitForSeconds(1f);
        if (state != "Dead")
        {
            protection = false;
        }
    }
}

