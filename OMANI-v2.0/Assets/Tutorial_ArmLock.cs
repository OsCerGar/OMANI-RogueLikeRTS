using UnityEngine;

public class Tutorial_ArmLock : Enemy
{
    [SerializeField] Tutorial_PlayerLock playerLock;
    [SerializeField] int legToRelease;

    public override void Die()
    {
        playerLock.LegRelease(legToRelease);
        enabled = false;
    }

}
