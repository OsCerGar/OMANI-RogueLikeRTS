public class WackaEnuRail : Interactible
{
    WackaEnu wackaEnu;

    public override void Start()
    {
        wackaEnu = GetComponentInParent<WackaEnu>();
    }
    public override void Action()
    {
        wackaEnu.Action();
    }
    public override void FullAction()
    {
        wackaEnu.FullAction();
    }
}
