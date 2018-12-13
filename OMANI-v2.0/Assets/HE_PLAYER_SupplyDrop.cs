using UnityEngine;

public class HE_PLAYER_SupplyDrop : MonoBehaviour
{

    Powers playerPowers;
    PeoplePool peoplePool;

    // Use this for initialization
    void Start()
    {
        playerPowers = GetComponentInParent<Powers>();
        peoplePool = FindObjectOfType<PeoplePool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPowers.powerPool < 10)
        {
            SupplyDropRescue();
        }
    }

    private void SupplyDropRescue()
    {

        playerPowers.addPower(250);
        peoplePool.WorkerSpawn(transform, transform.position);
        peoplePool.WorkerSpawn(transform, transform.position);
        peoplePool.SpearWarriorSpawn(transform);
        peoplePool.SpearWarriorSpawn(transform);
        transform.gameObject.SetActive(false);

    }
}
