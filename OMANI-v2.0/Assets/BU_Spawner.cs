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
            Debug.Log("hello");

            summoned = false;
            tutorialButton.enableButton();
        }
    }

    public override void Start()
    {
        peoplePool = FindObjectOfType<PeoplePool>();
        tutorialButton = GetComponentInChildren<BU_Building_ActionTutorial>();

        //Makes sure it checks for energy on the first run.
        lastTotalEnergy = 100;

    }
    public override void BuildingAction()
    {
        if (!summoned)
        {
            tutorialButton.StopWorkingAnimator();
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
    }
}
