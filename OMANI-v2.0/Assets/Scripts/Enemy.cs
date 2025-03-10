﻿
using UnityEngine;

public class Enemy : NPC
{
    public delegate void DieEvent(Enemy _robot);
    public static event DieEvent OnDie;
    [SerializeField]
    private Collider col;
    public EnnuiSpawnerManager ennuis;
    public Transform laserTarget;

    private void Awake()
    {
        //col = GetComponent<Collider>();
        if (laserTarget == null)
        {
            laserTarget = transform.FindDeepChild("LaserObjective");
        }

        laserTarget.gameObject.SetActive(true);
        ennuis = FindObjectOfType<EnnuiSpawnerManager>();
        state = "Alive";
        SetTrees();

    }

    public override void Update()
    {

        if (Nav != null)
        {
            TPC.Move(Nav.desiredVelocity);
        }
        /*
        if (anim != null)
        {
            if (!RootMotion)
            {
                anim.SetFloat("AnimSpeed", Nav.velocity.magnitude);
            }
        }
        */
    }
    public override void AttackHit()
    {
        Attackzone.SetActive(true);
    }

    public virtual void OnEnable()
    {
        if (laserTarget != null)
        {
            laserTarget.gameObject.SetActive(true);
        }

        if (col != null) { col.enabled = true; }

    }

    /*
    private void OnDisable()
    {
        if (OnDie != null)
        {
            OnDie(gameObject);
        }
    }
    */
    public override void Die()
    {
        base.Die();

        int random = Random.Range(0, 10);

        if (random < 3)
        {
            EnnuiSpawnerManager.EnnuiSpawner.SpawnEnnui(laserTarget);
        }
        Nav.enabled = false;

        col.enabled = false;
        laserTarget.gameObject.SetActive(false);
        //GamemasterController.GameMaster.Money += 1;
        GamemasterController.GameMaster.Save();



        if (OnDie != null)
        {
            OnDie(this);
        }

    }

    public void StepSound()
    {
        if (SM != null)
        {
            SM.Step();
        }
    }
}
