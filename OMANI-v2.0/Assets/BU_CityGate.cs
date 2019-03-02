public class BU_CityGate : BU_UniqueBuildingNoDistrict
{
    bool state = false;
    public override void Start()
    {
        base.Start();
    }
    public override void BuildingAction()
    {
        if (!state)
        {
            state = true;
            anim.SetTrigger("Open");
        }
        else
        {
            anim.SetTrigger("Close");
            state = false;
        }
        base.BuildingAction();
    }
}
