using UnityEngine;

public class HE_SupplyDrop : HE
{
    HE_PLAYER_SupplyDrop supplyPlayerDrop;
    Animator anim;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        requiredEnergy = 10;
        supplyPlayerDrop = FindObjectOfType<HE_PLAYER_SupplyDrop>();
        anim = transform.Find("HSupplyDrop").GetComponent<Animator>();
    }
    public override void BuildingAction()
    {
        base.BuildingAction();

        anim.SetTrigger("Activate");
    }

    public override void Action()
    {
        supplyPlayerDrop.anim.SetTrigger("Activated");
    }

}
