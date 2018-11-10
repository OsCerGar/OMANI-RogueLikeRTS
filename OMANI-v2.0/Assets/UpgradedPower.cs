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
            NPC.TakeDamage(25);
        }
        else if (ally = other.GetComponent<Robot>())
        {
            ally.robot_energy.FullAction();

        }
        else if (interactible = other.GetComponent<Interactible>())
        {
            interactible.FullAction();
        }
    }
}
