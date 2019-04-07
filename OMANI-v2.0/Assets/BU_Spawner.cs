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

    [SerializeField]
    AudioSource idle, summon;

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
            anim.SetTrigger("Reset");
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

    public void IdleStart() { if (!idle.isPlaying) {  idle.Play(); } }
    public void IdleStop() { if (idle.isPlaying) {  idle.Stop(); } }
    public void Summon() { if (!summon.isPlaying) { summon.Play(); } }
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
