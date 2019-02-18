using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_UniqueBuildingNoDistrict : MonoBehaviour
{
    public int lastTotalEnergy { get; set; }
    [SerializeField]
    public int totalEnergy;
    public int requiredEnergy { get; set; }
    public BU_Building_Action2 buildingActionMesh;
    [SerializeField]
    public Interactible_Repeater[] plugs { get; set; }

    public Animator anim;

    // Use this for initialization
    public virtual void Start()
    {
        //Makes sure it checks for energy on the first run.
        lastTotalEnergy = 100;

        buildingActionMesh = transform.GetComponentInChildren<BU_Building_Action2>();
    }

    public virtual void BuildingAction()
    {
        buildingActionMesh.StopWorkingAnimator();
    }

}
