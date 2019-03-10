using UnityEngine;

public class BU_WorkerMaker : BU_UniqueBuilding
{
    #region Vars

    PeoplePool peoplePool;
    [SerializeField]
    private GameObject workerSpawn;

    #endregion
    public override void Start()
    {
        base.Start();

        workerSpawn = transform.Find("WorkerSpawn").gameObject;
        peoplePool = FindObjectOfType<PeoplePool>();

        requiredEnergy = 25;
    }

    public override void BuildingAction()
    {
        base.BuildingAction();

        MakeWorker();
    }

    public void MakeWorker()
    {
        peoplePool.WorkerSpawn(transform, new Vector3(workerSpawn.transform.position.x + Random.Range(-2f, 2f), workerSpawn.transform.position.y, workerSpawn.transform.position.z - 3f));
    }
}
