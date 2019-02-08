using UnityEngine;

public class Tutorial_ArmLock : Enemy
{
    [SerializeField] Tutorial_PlayerLock playerLock;
    [SerializeField] int legToRelease;
    [SerializeField] DissolveController dissolveController;

    public override void Die()
    {
        playerLock.LegRelease(legToRelease);
        enabled = false;
        dissolveController.StartDissolving();
    }

    public override void Update()
    {
        base.Update();
    }

}
