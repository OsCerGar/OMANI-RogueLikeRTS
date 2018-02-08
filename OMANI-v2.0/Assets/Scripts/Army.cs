using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [Space]
    [SerializeField]
    private List<NPC> swordsmans, archers, musketeers, shieldmans, rogues = new List<NPC>();

    [SerializeField]
    private GameObject OrderPositionObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Adds the boy to the Army and makes it follow the Army commander.
    public void Reclute(NPC barroBoy)
    {
        if (barroBoy.AI_GetTarget() != this.gameObject)
        {
            switch (barroBoy.BoyType)
            {
                case "Swordsman":
                    swordsmans.Add(barroBoy);
                    break;
                case "Archer":
                    archers.Add(barroBoy);
                    break;
                case "Musketeer":
                    musketeers.Add(barroBoy);
                    break;
                case "Shieldman":
                    shieldmans.Add(barroBoy);
                    break;
                case "Rogue":
                    rogues.Add(barroBoy);
                    break;
            }

            barroBoy.Follow(this.gameObject);
        }

    }

    public void Order(string type, Vector3 orderPosition)
    {
        NPC barroBoy = null;
        GameObject orderPositionVar = Instantiate(OrderPositionObject);
        orderPositionVar.transform.position = orderPosition;

        switch (type)
        {
            case "Swordsman":
                barroBoy = swordsmans[swordsmans.Count - 1];
                swordsmans.Remove(barroBoy);

                break;
            case "Archer":
                barroBoy = archers[archers.Count - 1];
                archers.Remove(barroBoy);

                break;
            case "Musketeer":
                barroBoy = musketeers[musketeers.Count - 1];
                musketeers.Remove(barroBoy);
                break;
            case "Shieldman":
                barroBoy = shieldmans[shieldmans.Count - 1];
                shieldmans.Remove(barroBoy);
                break;
            case "Rogue":
                barroBoy = rogues[rogues.Count - 1];
                rogues.Remove(barroBoy);
                break;
        }
        orderPositionVar.GetComponent<OrderPositionObject>().NPC = barroBoy.gameObject;
        barroBoy.Order(orderPositionVar);
    }

    public NPC GetBoyArmy(string type)
    {
        NPC barroBoy = null;
        switch (type)
        {
            case "Swordsman":
                barroBoy = swordsmans[swordsmans.Count - 1];
                break;
            case "Archer":
                barroBoy = archers[archers.Count - 1];
                break;
            case "Musketeer":
                barroBoy = musketeers[musketeers.Count - 1];
                break;
            case "Shieldman":
                barroBoy = shieldmans[shieldmans.Count - 1];
                break;
            case "Rogue":
                barroBoy = rogues[rogues.Count - 1];
                break;
        }

        return barroBoy;
    }

    public void RemoveFromList(NPC barroBoy)
    {

        switch (barroBoy.BoyType)
        {
            case "Swordsman":
                swordsmans.Remove(barroBoy);

                break;
            case "Archer":
                archers.Remove(barroBoy);

                break;
            case "Musketeer":
                musketeers.Remove(barroBoy);
                break;
            case "Shieldman":
                shieldmans.Remove(barroBoy);
                break;
            case "Rogue":
                rogues.Remove(barroBoy);
                break;
        }
    }

}
