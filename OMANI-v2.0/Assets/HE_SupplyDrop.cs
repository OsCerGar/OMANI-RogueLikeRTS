using UnityEngine;

public class HE_SupplyDrop : BU_UniqueBuilding
{
    Transform supplyPlayerDrop;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        requiredEnergy = 10;
        supplyPlayerDrop = FindObjectOfType<HE_PLAYER_SupplyDrop>().transform;
        supplyPlayerDrop.gameObject.SetActive(false);
    }
    public override void BuildingAction()
    {
        base.BuildingAction();

        SupplyDrop();
    }

    private void SupplyDrop()
    {
        supplyPlayerDrop.gameObject.SetActive(true);
    }

}
