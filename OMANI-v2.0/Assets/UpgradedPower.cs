using UnityEngine;

public class UpgradedPower : Power
{
    private void OnTriggerEnter(Collider other)
    {
        Enemy NPC;
        Interactible interactible;
        Robot ally;
        if (NPC = other.GetComponent<Enemy>())
        {
            NPC.TakeDamage(25, Color.green);
        }
        else if (ally = other.GetComponent<Robot>())
        {
            ally.robot_energy.FullAction();
        }
        else if (interactible = other.GetComponent<Interactible>())
        {
            interactible.FullAction();
        }
        else if (other.CompareTag("MovableObject"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(other.transform.position - transform.position) * 15f, ForceMode.Impulse);
        }
    }
}
