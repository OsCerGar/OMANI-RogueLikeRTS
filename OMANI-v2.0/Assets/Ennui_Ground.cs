using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennui_Ground : Interactible
{

    [SerializeField]
    private int energy = 25;
    private float timer, totalTimer = 5;
    int build, mask, peopl;
    public override void Start()
    {
        base.Start();
        build = 1 << LayerMask.NameToLayer("Building");
        peopl = 1 << LayerMask.NameToLayer("People");
        mask = build | peopl;
    }

    void Update()
    {
        if (timer < totalTimer)
        {
            timer += Time.deltaTime;
        }

        if (timer > totalTimer)
        {
            timer = 0;
            this.transform.gameObject.SetActive(false);
        }

    }
    public void Action(Powers _powerPool)
    {
        _powerPool.addPower(energy);
        this.transform.gameObject.SetActive(false);
    }
}
