using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedPower : Power
{
    private void OnTriggerEnter(Collider other)
    {
        Enemy NPC;
        if (NPC = other.GetComponent<Enemy>())
        {
            NPC.TakeDamage(25);
        }
    }
}
