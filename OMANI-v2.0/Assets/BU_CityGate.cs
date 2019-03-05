public class BU_CityGate : BU_UniqueBuildingNoDistrict
{
    public override void Start()
    {
        base.Start();
    }
    public override void BuildingAction()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            anim.SetTrigger("Close");
        }
        else
        {
            anim.SetTrigger("Open");
        }
        base.BuildingAction();
    }
}
