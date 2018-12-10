using UnityEngine;

public class BU_TOOL_SlotMachine : BU_UniqueBuilding
{

    public BU_Energy_CityDistricts parentDistrict;
    PeoplePool peoplePool;

    private GameObject workerSpawn;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        requiredEnergy = 50;
        parentDistrict = GetComponentInParent<BU_Energy_CityDistricts>();
        peoplePool = FindObjectOfType<PeoplePool>();
        workerSpawn = transform.Find("WorkerSpawn").gameObject;

    }

    public override void BuildingAction()
    {
        base.BuildingAction();

        DingCachingChing();
    }

    public void DingCachingChing()
    {
        //Pay the cost
        parentDistrict.removeEnergy(50);
        parentDistrict.energyUpdateReduced();

        //Roll the dice
        float random = Random.Range(0f, 1f);

        //regularenu
        if (random < 0.6f)
        {
            peoplePool.WorkerSpawn(transform, new Vector3(workerSpawn.transform.position.x + Random.Range(-2f, 2f), workerSpawn.transform.position.y, workerSpawn.transform.position.z - 3f));
            peoplePool.WorkerSpawn(transform, new Vector3(workerSpawn.transform.position.x + Random.Range(-2f, 2f), workerSpawn.transform.position.y, workerSpawn.transform.position.z - 3f));

        }
        //goldennu
        else
        {
            peoplePool.SpearWarriorSpawn(workerSpawn.transform);
            peoplePool.SpearWarriorSpawn(workerSpawn.transform);

        }

    }

}
