using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [Space]
    [SerializeField]
    private List<NPC> swordsmans, archers, musketeers, shieldmans, rogues, workers = new List<NPC>();

    [SerializeField]
    private GameObject OrderPositionObject;


    //G_UI
    private List<SpriteRenderer> GameUI = new List<SpriteRenderer>();

    [SerializeField]
    GameObject UI;

    float alphaTarget = 0.111f;


    void Start()
    {

        for (int i = 0; i < 50; i++)
        {
            SpriteRenderer pooled = Instantiate(UI, this.transform).GetComponent<SpriteRenderer>();
            GameUI.Add(pooled);
        }


    }

    //Adds the boy to the Army and makes it follow the Army commander.
    public void Reclute(NPC barroBoy)
    {
        bool alreadyIn = false;
        if (barroBoy.AI_GetEnemy() != this.gameObject)
        {
            switch (barroBoy.BoyType)
            {
                case "Swordsman":
                    foreach (NPC boy in swordsmans)
                    {
                        if (boy == barroBoy)
                        {
                            alreadyIn = true;
                        }
                    }
                    if (alreadyIn != true)
                    {
                        swordsmans.Add(barroBoy);
                    }
                    break;
                case "Archer":
                    foreach (NPC boy in archers)
                    {
                        if (boy == barroBoy)
                        {
                            alreadyIn = true;
                        }
                    }
                    if (alreadyIn != true)
                    {
                        archers.Add(barroBoy);
                    }

                    break;
                case "Musketeer":
                    foreach (NPC boy in musketeers)
                    {
                        if (boy == barroBoy)
                        {
                            alreadyIn = true;
                        }
                    }
                    if (alreadyIn != true)
                    {
                        musketeers.Add(barroBoy);
                    }

                    break;

                case "Shieldman":
                    foreach (NPC boy in shieldmans)
                    {
                        if (boy == barroBoy)
                        {
                            alreadyIn = true;
                        }
                    }
                    if (alreadyIn != true)
                    {

                        shieldmans.Add(barroBoy);
                    }
                    break;
                case "Rogue":
                    foreach (NPC boy in rogues)
                    {
                        if (boy == barroBoy)
                        {
                            alreadyIn = true;
                        }
                    }
                    if (alreadyIn != true)
                    {

                        rogues.Add(barroBoy);
                    }
                    break;

                case "Worker":
                    foreach (NPC boy in workers)
                    {
                        if (boy == barroBoy)
                        {
                            alreadyIn = true;
                        }
                    }
                    if (alreadyIn != true)
                    {

                        workers.Add(barroBoy);
                    }
                    break;

            }

            barroBoy.Follow(this.gameObject);
        }

    }

    public void Order(string type, Vector3 orderPosition)
    {
        NPC barroBoy = null;

        switch (type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = swordsmans[swordsmans.Count - 1];
                    swordsmans.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Order(orderPositionVar);
                }

                break;
            case "Archer":
                if (archers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = archers[archers.Count - 1];
                    archers.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Order(orderPositionVar);

                }
                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = musketeers[musketeers.Count - 1];
                    musketeers.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Order(orderPositionVar);

                }
                break;

            case "Worker":
                if (workers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = workers[workers.Count - 1];
                    workers.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Order(orderPositionVar);

                }
                break;
            case "Shieldman":

                if (shieldmans.Count > 0)
                {

                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = shieldmans[shieldmans.Count - 1];
                    shieldmans.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Order(orderPositionVar);

                }
                break;
            case "Rogue":
                if (rogues.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = rogues[rogues.Count - 1];
                    rogues.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Order(orderPositionVar);

                }
                break;
        }
    }
    public void OrderDirect(string type, GameObject orderPosition)
    {
        NPC barroBoy = null;
        switch (type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    barroBoy = swordsmans[swordsmans.Count - 1];
                    swordsmans.Remove(barroBoy);

                    barroBoy.Order(orderPosition);
                }

                break;
            case "Archer":
                if (archers.Count > 0)
                {

                    barroBoy = archers[archers.Count - 1];
                    archers.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
            case "Worker":
                if (workers.Count > 0)
                {

                    barroBoy = workers[workers.Count - 1];
                    workers.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {

                    barroBoy = musketeers[musketeers.Count - 1];
                    musketeers.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
            case "Shieldman":

                if (shieldmans.Count > 0)
                {


                    barroBoy = shieldmans[shieldmans.Count - 1];
                    shieldmans.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
            case "Rogue":
                if (rogues.Count > 0)
                {

                    barroBoy = rogues[rogues.Count - 1];
                    rogues.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
        }
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
            case "Worker":
                barroBoy = workers[workers.Count - 1];
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
    public List<NPC> GetArmy(string type)
    {

        List<NPC> armyList = null;
        switch (type)
        {
            case "Swordsman":
                armyList = swordsmans;
                break;
            case "Archer":
                armyList = archers;
                break;
            case "Musketeer":
                armyList = musketeers;
                break;
            case "Worker":
                armyList = workers;
                break;
            case "Shieldman":
                armyList = shieldmans;
                break;
            case "Rogue":
                armyList = rogues;
                break;
        }

        return armyList;
    }

    public void ArmyChargedOrder(string type)
    {

        switch (type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    foreach (NPC npc in swordsmans)
                    {
                        npc.ChargedOrder();
                    }
                    swordsmans.Clear();
                }
                break;
            case "Archer":
                if (archers.Count > 0)
                {
                    foreach (NPC npc in archers)
                    {
                        npc.ChargedOrder();
                    }
                    archers.Clear();
                }
                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {
                    foreach (NPC npc in musketeers)
                    {
                        npc.ChargedOrder();
                    }
                    musketeers.Clear();
                }
                break;
            case "Worker":
                if (workers.Count > 0)
                {
                    foreach (NPC npc in workers)
                    {
                        npc.ChargedOrder();
                    }
                    workers.Clear();
                }
                break;
            case "Shieldman":
                if (shieldmans.Count > 0)
                {
                    foreach (NPC npc in shieldmans)
                    {
                        npc.ChargedOrder();
                    }
                    shieldmans.Clear();
                }
                break;
            case "Rogue":
                if (rogues.Count > 0)
                {
                    foreach (NPC npc in rogues)
                    {
                        npc.ChargedOrder();
                    }
                    rogues.Clear();
                }
                break;
        }

    }

    public int ListSize(string _type)
    {
        int size = 0;
        switch (_type)
        {
            case "Swordsman":
                size = swordsmans.Count;
                break;
            case "Archer":
                size = archers.Count;
                break;
            case "Musketeer":
                size = musketeers.Count;
                break;
            case "Worker":
                size = workers.Count;
                break;
            case "Shieldman":
                size = shieldmans.Count;
                break;
            case "Rogue":
                size = rogues.Count;
                break;
        }

        return size;
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

            case "Worker":
                workers.Remove(barroBoy);

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

    public void GUI_ActivateCircle(string _type)
    {
        switch (_type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    GUI_ListDisableCircle();

                    GUI_ListActivateCircle(swordsmans);
                }

                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {
                    GUI_ListDisableCircle();

                    GUI_ListActivateCircle(musketeers);

                }
                break;
            case "Worker":
                if (workers.Count > 0)
                {
                    GUI_ListDisableCircle();

                    GUI_ListActivateCircle(workers);

                }
                break;
        }
    }

    private void GUI_ListActivateCircle(List<NPC> _list)
    {
        //Gives a priority circle to the npc that is going to get the order.


        for (int i = _list.Count - 1; i >= 0; i--)
        {
            if (i == _list.Count - 1)
            {

                GameUI[i].transform.position = _list[i].ui_information.transform.position;
                GameUI[i].transform.localScale = _list[i].ui_information.transform.localScale;
                //Priority, guy who gets the order
                GameUI[i].color = Color.yellow;
            }
            else
            {

                GameUI[i].transform.position = _list[i].ui_information.transform.position;
                GameUI[i].transform.localScale = _list[i].ui_information.transform.localScale;
                GameUI[i].color = Color.white;

            }
        }


    }

    public void GUI_ListDisableCircle()
    {

        foreach (SpriteRenderer gui in GameUI)
        {
            gui.transform.position = new Vector3(1000, 1000, 1000);
        }
    }

}

