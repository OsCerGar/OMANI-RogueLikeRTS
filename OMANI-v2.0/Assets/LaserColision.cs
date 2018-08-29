using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserColision : MonoBehaviour
{
    public bool laserEnabled { get; set; }
    Power_Laser powerLaser;

    private void Awake()
    {
        powerLaser = FindObjectOfType<Power_Laser>();
    }
    private void Update()
    {
        if (laserEnabled)
        {
            LaserCollisions();
        }
    }

    private void LaserCollisions()
    {
        Enemy enemy, closestEnemyTarget = null;
        Interactible interactible, closestBUTarget = null;
        Robot ally, closestTarget = null;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(this.transform.position, 0.5f);
        foreach (Collider other in targetsInViewRadius)
        {
            if (other.CompareTag("Building"))
            {
                interactible = other.GetComponent<Interactible>();

                if (interactible != null)
                {
                    Transform target = other.transform;

                    //Distance to target
                    float dstToTarget = Vector3.Distance(this.transform.position, target.position);
                    //If the closestTarget is null he is the closest target.
                    // If the distance is smaller than the distance to the closestTarget.
                    if (closestBUTarget == null || dstToTarget < Vector3.Distance(this.transform.position, target.position))
                    {
                        closestBUTarget = interactible;
                    }
                }
            }

            else if (other.CompareTag("Enemy"))
            {

                enemy = other.GetComponent<Enemy>();

                if (enemy != null)
                {
                    Transform target = other.transform;

                    //Distance to target
                    float dstToTarget = Vector3.Distance(this.transform.position, target.position);
                    //If the closestTarget is null he is the closest target.
                    // If the distance is smaller than the distance to the closestTarget.
                    if (closestBUTarget == null || dstToTarget < Vector3.Distance(this.transform.position, target.position))
                    {
                        closestEnemyTarget = enemy;
                    }
                }
            }

            else if (other.CompareTag("People"))
            {

                ally = other.GetComponent<Robot>();

                if (ally != null)
                {
                    Transform target = other.transform;

                    //Distance to target
                    float dstToTarget = Vector3.Distance(this.transform.position, target.position);
                    //If the closestTarget is null he is the closest target.
                    // If the distance is smaller than the distance to the closestTarget.
                    if (closestBUTarget == null || dstToTarget < Vector3.Distance(this.transform.position, target.position))
                    {
                        closestTarget = ally;
                    }
                }
            }
        }

        if (closestBUTarget != null)
        {
            closestBUTarget.Action();
            if (closestBUTarget.actionBool)
            {
                powerLaser.setWidth(closestBUTarget.linkPrice);
            }
        }
        if (closestTarget != null)
        {
            closestTarget.robot_energy.Action();
        }
        if (closestEnemyTarget != null)
        {
            //closestEnemyTarget.TakeDamage(1);
            //powerLaser.setWidth(closestEnemyTarget.linkPrice);
        }

        if (closestBUTarget == null && closestTarget == null && closestEnemyTarget == null)
        {
            powerLaser.setWidth(1);
        }

    }
}

