using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCollisions : MonoBehaviour
{

    [SerializeField] ParticleSystem PS;
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy;
        Interactible interactible;
        Robot ally;

        if (other.CompareTag("Building"))
        {
            interactible = other.GetComponent<Interactible>();
            interactible.Action();
            PS.transform.position = this.transform.position;
            PS.Play();
        }

        else if (other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
            PS.transform.position = this.transform.position;
            PS.Play();
            enemy.TakeDamage(5, true, 0.2f, this.transform);
        }

        else if (other.CompareTag("People"))
        {

            ally = other.GetComponent<Robot>();
            PS.transform.position = this.transform.position;
            PS.Play();
            ally.Action();
        }
    }

}
