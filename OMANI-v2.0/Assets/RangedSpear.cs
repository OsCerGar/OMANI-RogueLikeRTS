using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZObjectPools;

public class RangedSpear : MonoBehaviour
{
    [SerializeField] bool Knockback;
    [HideInInspector] public bool PowerUp;
     EZObjectPool Effect;
    [SerializeField] AttackSoundsManager Sounds;
    [SerializeField] NPC thisNpcScript;
    string tagToAttack, secondTagToAttack;
    bool missed;
    ParticleSystem PowerUpEffect, PowerUpHitEffect;
    Vector3 destination,startPos;
    bool go = true;
    float x = 0;
    // Use this for initialization

    private void Start()
    {
        Effect =  GameObject.Find("EnemyHit").GetComponent<EZObjectPool>();
        if (thisNpcScript.transform.tag == "Enemy")
        {
            tagToAttack = "People";
            secondTagToAttack = "Player";
        }
        else
        {
            PowerUpEffect = transform.parent.Find("PowerUpEffect").GetComponent<ParticleSystem>();
            PowerUpHitEffect = transform.parent.Find("PowerUpHit").GetComponent<ParticleSystem>();
            tagToAttack = "Enemy";
            secondTagToAttack = "Enemy";
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == tagToAttack || other.tag == secondTagToAttack)
        {
            if (Sounds != null)
            {
                Sounds.AttackHit();
            }
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            var attackDamage = thisNpcScript.Damage;
            if (PowerUp)
            {
                attackDamage = attackDamage * 2;
                Knockback = true;
                PowerUpHit();
                PowerUp = false;
            }

            //Make his take damage;
            if (Knockback)
            {
                EnemyNPC.TakeDamage(attackDamage, true, 5, transform.parent.transform);

                Knockback = false;
            }
            else
            {
                EnemyNPC.TakeDamage(attackDamage);
            }
            //Set his Enemy to this
            if (EnemyNPC != null)
            {
                EnemyNPC.AI_SetTarget(thisNpcScript.gameObject);
            }
            //If he's dead, then forget about him
            missed = false;
            Hit();

        }
    }
    
    

    // Update is called once per frame
    void Update () {
        if (go)
        {
            x += (Time.deltaTime * 1f);
            transform.position = MathParabola.Parabola(startPos, destination, 2f, x);
            transform.LookAt(MathParabola.Parabola(startPos, destination, 2f, x+ (Time.deltaTime * 1f)));
            if (x > 0.95)
            {
                Hit();
            }
        }
    }
    public void Hit()
    {
        x = 0;
        go = false;
        if (Effect != null)
        {
            Effect.TryGetNextObject(transform.position,transform.rotation);
        }
        this.transform.gameObject.SetActive(false);
    }
    public void setDestination(Transform _from, Vector3 _to)
    {
        this.transform.position = _from.position;
        this.transform.rotation = _from.rotation;
        startPos = _from.position;
        destination = _to;
        go = true;
    }
    public void ActivateBoostAttack()
    {
        PowerUpEffect.Play();
        PowerUp = true;
    }
    public void DeactivateBoostAttack()
    {

        PowerUpEffect.Stop();
    }
    public void PowerUpHit()
    {
        DeactivateBoostAttack();
        PowerUpHitEffect.Play();
    }
}
