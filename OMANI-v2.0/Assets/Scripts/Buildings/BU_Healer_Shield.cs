using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Healer_Shield : BU_UniqueBuilding
{
    Transform healer;
    int healing;
    [SerializeField]
    private float timeToHeal = 0, totalTimeToHeal = 5, healingSize = 41;
    [SerializeField]
    private Transform baseCenter;
    private GameObject BU_HealingTexture;
    BU_Healing_GUI healingGUI;

    //Audios

    AudioSource addedWorker;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        baseCenter = this.transform.parent;
        requiredEnergy = 2;

        BU_HealingTexture = this.transform.Find("BU_HealingTexture").gameObject;
        healingGUI = this.transform.GetComponentInChildren<BU_Healing_GUI>();
        BU_HealingTexture.transform.position = new Vector3(baseCenter.transform.position.x, baseCenter.transform.position.y + 0.2f, baseCenter.transform.position.z);
        healer = this.transform.Find("Healer");

        //Audio
        addedWorker = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    public void Update()
    {

        switch (totalEnergy)
        {
            case 2:
                healing = 5;
                break;
            case 3:
                healing = 10;
                break;
            case 4:
                healing = 15;
                break;
        }

        if (totalEnergy >= requiredEnergy)
        {
            BU_HealingTexture.SetActive(true);
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

            BU_HealingTexture.SetActive(false);

            healingGUI.ChangeHealingClock(0);
        }
    }

    void Healer(int _heal)
    {

        Collider[] objectsInArea = null;
        objectsInArea = Physics.OverlapSphere(baseCenter.position, healingSize, 1 << 9);

        //Checks if there are possible interactions.
        if (objectsInArea.Length > 0)
        {
            for (int i = 0; i < objectsInArea.Length; i++)
            {
                NPC npc = objectsInArea[i].GetComponent<NPC>();

                if (npc != null)
                {
                    npc.Heal(_heal);
                }
            }

        }

    }


}
