using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Healer : BU_UniqueBuilding
{
    Transform healer;
    int healing;
    float timeToHeal = 0, totalTimeToHeal = 5;
    BU_Healing_GUI healingGUI;

    float healingSize = 6;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        requiredEnergy = 2;

        healingGUI = this.transform.GetComponentInChildren<BU_Healing_GUI>();

        healer = this.transform.Find("Healer");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        switch (totalEnergy)
        {
            case 2:
                healing = 1;
                break;
            case 3:
                healing = 2;
                break;
            case 4:
                healing = 3;
                break;
        }

        if (totalEnergy >= requiredEnergy)
        {

            timeToHeal += Time.deltaTime;

            healingGUI.ChangeHealingClock(timeToHeal / totalTimeToHeal);

            if (timeToHeal > totalTimeToHeal)
            {
                Healer(healing);
                timeToHeal = 0;
            }
        }

        else
        {


            healingGUI.ChangeHealingClock(0);
        }
    }

    void Healer(int _heal)
    {
        Debug.Log("Healer");

        Collider[] objectsInArea = null;
        objectsInArea = Physics.OverlapSphere(transform.position, healingSize, 1 << 9);

        //Checks if there are possible interactions.
        if (objectsInArea.Length > 0)
        {

            for (int i = 0; i < objectsInArea.Length; i++)
            {
                if (objectsInArea[i].GetComponent<NPC>() != null)
                {
                    objectsInArea[i].GetComponent<NPC>().Heal(_heal);
                }
            }

        }

    }


}
