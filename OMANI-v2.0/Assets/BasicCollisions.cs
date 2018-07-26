using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCollisions : Power
{

    [SerializeField] ParticleSystem PS;
    bool hit = false;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy;
        Interactible interactible;
        Robot ally;

        if (other.CompareTag("Building") && hit != true)
        {
            interactible = other.GetComponent<Interactible>();
            interactible.Action();
            PS.transform.position = this.transform.position;
            PS.Play();

            hit = true;
        }

        else if (other.CompareTag("Enemy") && hit != true)
        {
            enemy = other.GetComponent<Enemy>();
            PS.transform.position = this.transform.position;
            PS.Play();
            enemy.TakeDamage(5, true, 0.2f, this.transform);

            hit = true;

        }

        else if (other.CompareTag("People") && hit != true)
        {

            ally = other.GetComponent<Robot>();
            PS.transform.position = this.transform.position;
            PS.Play();
            ally.Action();
            hit = true;
        }
    }

    public override void Update()
    {
        base.Update();
        if (hit)
        {
            if (PS.IsAlive() == false)
            {
                transform.parent.gameObject.SetActive(false);
                hit = false;
            }
        }

    }

}
