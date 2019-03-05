using System.Collections;
using UnityEngine;


public class BU_Spawner : BU_UniqueBuildingNoDistrict
{
    [SerializeField] GameObject SpawnPoint;
    PeoplePool peoplePool;

    [SerializeField]
    bool summons;
    bool summoned;

    public override void Start()
    {
        base.Start();
        peoplePool = FindObjectOfType<PeoplePool>();

    }
    public override void BuildingAction()
    {
        if (!summoned)
        {
            //base.BuildingAction();
            anim.SetTrigger("Activate");
            if (summons)
            {
                StartCoroutine("SpawnRoutine");
            }
            summoned = true;
        }
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(2f);
        peoplePool.WorkerSpawn(transform, new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z));

    }
}
