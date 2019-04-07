using UnityEngine;

public class EnnuiSphereEnergy : Interactible
{
    Ennui_Ground ennui;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //Laser Price
        linkPrice = 15;
        price = 50;
        finalLinkPrice = 40;
        currentLinkPrice = 0;

        t = 0.2f; ennui = GetComponent<Ennui_Ground>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        ennui.ennuiAnimator.Play("Iddle", 0, powerReduced / price);
        
    }

    public override void ActionCompleted() {
        base.ActionCompleted();
        ennui.ennuiAnimator.SetTrigger("Die");
    }
}
