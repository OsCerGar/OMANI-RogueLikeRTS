using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserColision : MonoBehaviour
{

    Enemy enemy, lastEnemy;
    Interactible interactible, lastInteractible;
    Robot ally, lastAlly;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            if (interactible != null)
            {
                if (interactible != lastInteractible)
                {
                    interactible = other.GetComponent<Interactible>();
                    lastInteractible = interactible;
                }

                interactible.Action();
            }
            else
            {
                interactible = other.GetComponent<Interactible>();
                interactible.Action();
            }

        }

        else if (other.CompareTag("Enemy"))
        {
            if (enemy != null)
            {
                if (enemy != lastEnemy)
                {
                    enemy = other.GetComponent<Enemy>();
                    lastEnemy = enemy;
                }

                enemy.TakeDamage(1);
            }
            else
            {
                enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(1);
            }
        }

        else if (other.CompareTag("People"))
        {
            if (ally != null)
            {
                if (ally != lastAlly)
                {
                    ally = other.GetComponent<Robot>();
                    ally = lastAlly;
                }

                ally.Action();
            }
            else
            {
                ally = other.GetComponent<Robot>();
                ally.Action();
            }
        }

    }


}
