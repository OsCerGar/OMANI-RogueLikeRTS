using UnityEngine;

public class HE_PLAYER_SupplyDrop : MonoBehaviour
{

    Powers playerPowers;
    PeoplePool peoplePool;
    public Animator anim;
    MeshRenderer myMesh;
    // Use this for initialization
    void Start()
    {
        playerPowers = FindObjectOfType<Powers>();
        peoplePool = FindObjectOfType<PeoplePool>();
        anim = GetComponent<Animator>();
        myMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPowers.powerPool < 10 && anim.GetCurrentAnimatorStateInfo(0).IsName("SupplyDropBuffActivatedIddle"))
        {
            SupplyDropRescue();
        }
    }

    private void SupplyDropRescue()
    {
        playerPowers.addPower(250);
        peoplePool.WorkerSpawn(transform, transform.position);
        peoplePool.WorkerSpawn(transform, transform.position);
        peoplePool.WarriorSpawn(transform);
        peoplePool.WarriorSpawn(transform);
        myMesh.enabled = false;
        anim.SetTrigger("Done");

    }
}
