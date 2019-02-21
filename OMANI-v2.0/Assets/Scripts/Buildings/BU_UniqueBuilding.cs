using UnityEngine;

public class BU_UniqueBuilding : MonoBehaviour
{
    public int lastTotalEnergy { get; set; }
    [SerializeField]
    public int totalEnergy;
    public int requiredEnergy { get; set; }
    public BU_Building_Action buildingActionMesh;
    public BU_Energy_CityDistricts buildingDistrict;
    [SerializeField]
    public Interactible_Repeater[] plugs { get; set; }

    public Animator animator;
    // Use this for initialization
    public virtual void Start()
    {
        //Makes sure it checks for energy on the first run.
        lastTotalEnergy = 100;

        buildingActionMesh = transform.GetComponentInChildren<BU_Building_Action>();
        buildingDistrict = transform.parent.GetComponentInParent<BU_Energy_CityDistricts>();
        animator = GetComponent<Animator>();
    }

    public virtual void BuildingAction()
    {
        buildingActionMesh.StopWorkingAnimator();
    }
}
