using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Energy : MonoBehaviour
{
    [SerializeField]
    private GameObject cablePrefab;

    [SerializeField]
    List<CableComponent> cables = new List<CableComponent>();
    public int energy = 0;

    public MeshRenderer[] buttons;

    GameObject top;

    // Use this for initialization
    void Start()
    {
        top = this.transform.Find("Top").gameObject;

        // Searches buttons
        //buttons = this.transform.Find("Office").GetChild(0).GetComponentsInChildren<MeshRenderer>();

        foreach (CableComponent cable in this.transform.Find("Cables").GetComponentsInChildren<CableComponent>())
        {
            cables.Add(cable);
        }

    }

    public void RequestCable(GameObject _position)
    {
        //ifenergyright
        LaunchCable(_position);
    }

    private void LaunchCable(GameObject _position)
    {
        cables[1].cableEnd.Launch(_position);
    }
}
