using System.Collections;
using UnityEngine;


public class BU_Spawner : BU_UniqueBuildingNoDistrict
{
    [SerializeField] GameObject SpawnPoint;
    PeoplePool peoplePool;
    BU_Building_ActionTutorial tutorialButton;

    [SerializeField]
    bool summons;
    bool summoned;

    [SerializeField]
    string whatToSummon;
    GameObject worker;

    void OnEnable()
    {
        Robot.OnDie += robotDied;
    }


    void OnDisable()
    {
        Robot.OnDie -= robotDied;
    }

    void robotDied(GameObject _robot)
    {
        if (_robot = worker)
        {
            summoned = false;
            tutorialButton.enableButton();
        }
    }

    public override void Start()
    {
        base.Start();
        peoplePool = FindObjectOfType<PeoplePool>();
        tutorialButton = GetComponentInChildren<BU_Building_ActionTutorial>();

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
        worker = peoplePool.Spawn(transform, new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z), whatToSummon);
        tutorialButton.enableButton();
    }
}
