using UnityEngine;

public class BU_TOOL_SlotMachine : HE
{

    public BU_Energy_CityDistricts parentDistrict;
    PeoplePool peoplePool;

    private GameObject workerSpawn;
    public Animator anim;
    float random;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        requiredEnergy = 10;
        parentDistrict = GetComponentInParent<BU_Energy_CityDistricts>();
        peoplePool = FindObjectOfType<PeoplePool>();
        workerSpawn = transform.Find("WorkerSpawn").gameObject;
        anim = transform.Find("HSlotMachine").GetComponent<Animator>();
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
        random = Random.Range(0f, 1f);

        //regularenu
        if (random < 0.6f)
        {
            anim.SetTrigger("Level1");
        }
        //goldennu
        else
        {
            anim.SetTrigger("Level2");
        }

    }

    public override void Action()
    {
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
