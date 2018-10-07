using System;
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


    [SerializeField]
    GameObject UI;

    private int positionCounter;

    float alphaTarget = 0.111f;

    private List<GameObject> positions = new List<GameObject>();
    [SerializeField] private GameObject ShootingPosition;
    LookDirectionsAndOrder look;
    [SerializeField]
    float armyseparation = 4;
    public void setFormation()
    {

        positionCounter = 0;
        bool completed = false;


        int pointer = look.selectedTypeInt;
        checkNameAndOrganice(pointer);



        while (completed != false)
        {

            pointer++;

            if (pointer == look.selectedTypeInt)
            {
                //If the pointer got to the selected Type then everything has been checked
                completed = true;
            }
            else
            {

                if (pointer == look.selectedTypeList.Count)
                {
                    //if the pointer is at the end of the list , go to the start again
                    pointer = 0;
                }

                checkNameAndOrganice(pointer);

            }
        }
    }

    private void checkNameAndOrganice(int pointer)
    {
        var type = look.selectedTypeList[pointer];

        switch (type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    if (pointer == look.selectedTypeInt)
                    {
                        OrganiceRobots(swordsmans, true);
                    }
                    else
                    {
                        OrganiceRobots(swordsmans, false);
                    }
                }

                break;
            case "Archer":
                if (archers.Count > 0)
                {
                    if (pointer == look.selectedTypeInt)
                    {
                        OrganiceRobots(archers, true);
                    }
                    else
                    {
                        OrganiceRobots(archers, false);
                    }

                }
                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {
                    if (pointer == look.selectedTypeInt)
                    {
                        OrganiceRobots(musketeers, true);
                    }
                    else
                    {
                        OrganiceRobots(musketeers, false);
                    }

                }
                break;

            case "Worker":
                if (workers.Count > 0)
                {
                    if (pointer == look.selectedTypeInt)
                    {
                        OrganiceRobots(workers, true);
                    }
                    else
                    {
                        OrganiceRobots(workers, false);
                    }

                }
                break;
            case "Shieldman":

                if (shieldmans.Count > 0)
                {

                    if (pointer == look.selectedTypeInt)
                    {
                        OrganiceRobots(shieldmans, true);
                    }
                    else
                    {
                        OrganiceRobots(shieldmans, false);
                    }

                }
                break;
            case "Rogue":
                if (rogues.Count > 0)
                {
                    if (pointer == look.selectedTypeInt)
                    {
                        OrganiceRobots(rogues, true);
                    }
                    else
                    {
                        OrganiceRobots(rogues, false);
                    }

                }
                break;
        }


    }

    private void OrganiceRobots(List<NPC> robotsToCheck, bool special)
    {
        if (special)
        {
            for (int i = 0; i < robotsToCheck.Count; i++)
            {
                if (i == robotsToCheck.Count - 1) //If he's the last one, the give him an special position
                {
                    robotsToCheck[i].Follow(ShootingPosition);
                }
                else
                {
                    robotsToCheck[i].Follow(setPosition(positionCounter));
                    positionCounter++;
                }
            }
        }
        else
        {
            foreach (var robot in robotsToCheck)
            {
                robot.Follow(setPosition(positionCounter));
                positionCounter++;
            }
        }

    }

    private void Start()
    {
        look = FindObjectOfType<LookDirectionsAndOrder>();

    }

    private GameObject setPosition(int _position)
    {
        if (positions.Count > _position)
        {
            return positions[_position];
        }
        else
        {
            //ccalculate and add a Gameobject that sets itself behind Omani
            var newPosition = Instantiate(new GameObject(), transform.position, transform.rotation, transform);
            var positionMod = (_position + 5) % 5;
            var positionOffset = new Vector3((-armyseparation * 2) + (positionMod * armyseparation), 0, -(int)((_position + 5) / 5) - 2);
            newPosition.transform.localPosition = positionOffset;
            positions.Add(newPosition);
            return newPosition;
        }
    }

    //Adds the boy to the Army and makes it follow the Army commander.
    public void Reclute(NPC barroBoy)
    {
        if (ListSize(barroBoy.boyType) < 1)
        {
            look.selectedTypeList.Add(barroBoy.boyType);
        }

        bool alreadyIn = false;

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
        setFormation();
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
                    barroBoy.GUI_Script.DisableCircle();
                    swordsmans.Remove(barroBoy);

                    //   orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Attack(orderPositionVar);
                }

                break;
            case "Archer":
                if (archers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = archers[archers.Count - 1];
                    barroBoy.GUI_Script.DisableCircle();
                    archers.Remove(barroBoy);

                    //   orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Attack(orderPositionVar);
                }
                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = musketeers[musketeers.Count - 1];
                    barroBoy.GUI_Script.DisableCircle();
                    musketeers.Remove(barroBoy);

                    //    orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Attack(orderPositionVar);

                }
                break;

            case "Worker":
                if (workers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = workers[workers.Count - 1];
                    barroBoy.GUI_Script.DisableCircle();
                    workers.Remove(barroBoy);

                    //    orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Attack(orderPositionVar);
                }
                break;
            case "Shieldman":

                if (shieldmans.Count > 0)
                {

                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = shieldmans[shieldmans.Count - 1];
                    barroBoy.GUI_Script.DisableCircle();
                    shieldmans.Remove(barroBoy);

                    //   orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Attack(orderPositionVar);

                }
                break;
            case "Rogue":
                if (rogues.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = rogues[rogues.Count - 1];
                    barroBoy.GUI_Script.DisableCircle();
                    rogues.Remove(barroBoy);

                    //  orderPositionVar.GetComponent<OrderPositionObject>().npc = barroBoy;
                    barroBoy.Attack(orderPositionVar);

                }
                break;
        }
        setFormation();
    }
    public void Attack(string type, GameObject attackPosition)
    {
        NPC barroBoy = null;
        switch (type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = attackPosition.transform.position;

                    barroBoy = swordsmans[swordsmans.Count - 1];
                    swordsmans.Remove(barroBoy);
                    barroBoy.GUI_Script.DisableCircle();

                    barroBoy.Attack(orderPositionVar);
                }

                break;
            case "Archer":
                if (archers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = attackPosition.transform.position;

                    barroBoy = archers[archers.Count - 1];
                    archers.Remove(barroBoy);
                    barroBoy.GUI_Script.DisableCircle();

                    barroBoy.Attack(orderPositionVar);

                }
                break;
            case "Worker":
                if (workers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = attackPosition.transform.position;

                    barroBoy = workers[workers.Count - 1];
                    workers.Remove(barroBoy);
                    barroBoy.GUI_Script.DisableCircle();

                    barroBoy.Attack(orderPositionVar);

                }
                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = attackPosition.transform.position;

                    barroBoy = musketeers[musketeers.Count - 1];
                    musketeers.Remove(barroBoy);
                    barroBoy.GUI_Script.DisableCircle();

                    barroBoy.Attack(orderPositionVar);

                }
                break;
            case "Shieldman":

                if (shieldmans.Count > 0)
                {

                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = attackPosition.transform.position;

                    barroBoy = shieldmans[shieldmans.Count - 1];
                    shieldmans.Remove(barroBoy);
                    barroBoy.GUI_Script.DisableCircle();

                    barroBoy.Attack(orderPositionVar);

                }
                break;
            case "Rogue":
                if (rogues.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = attackPosition.transform.position;

                    barroBoy = rogues[rogues.Count - 1];
                    rogues.Remove(barroBoy);
                    barroBoy.GUI_Script.DisableCircle();

                    barroBoy.Attack(orderPositionVar);

                }
                break;
        }
        setFormation();

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
                    GUI_ListDisableCircle(musketeers);
                    GUI_ListDisableCircle(workers);
                    GUI_ListActivateCircle(swordsmans);
                }

                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {
                    GUI_ListDisableCircle(swordsmans);
                    GUI_ListDisableCircle(workers);
                    GUI_ListActivateCircle(musketeers);

                }
                break;
            case "Worker":
                if (workers.Count > 0)
                {
                    GUI_ListDisableCircle(swordsmans);
                    GUI_ListDisableCircle(musketeers);
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
                _list[i].GUI_Script.gameObject.SetActive(true);
                _list[i].GUI_Script.ActivateCircle();
                _list[i].GUI_Script.PriorityMaterial();
            }
            else
            {
                _list[i].GUI_Script.gameObject.SetActive(true);
                _list[i].GUI_Script.ActivateCircle();
                _list[i].GUI_Script.RegularMaterial();
            }
        }
    }
    public void GUI_ListDisableCircle(List<NPC> _list)
    {
        //Gives a priority circle to the npc that is going to get the order.
        for (int i = _list.Count - 1; i >= 0; i--)
        {
            _list[i].GUI_Script.DisableCircle();
        }
    }

}

