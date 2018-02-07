using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPositionObject : MonoBehaviour
{

    public GameObject NPC;

    int layermask1 = 1 << 9;
    int layermask2 = 1 << 11;

    private void Start()
    {
        //Self destroys after 10 seconds. It shouldn't autodestroy like this.
        Destroy(this.gameObject, 10f);
    }

    void FixedUpdate()
    {
        Collider[] targetsInViewRadius = null;
        targetsInViewRadius = Physics.OverlapSphere(transform.position, 1.5f, layermask2, QueryTriggerInteraction.UseGlobal);
        if (targetsInViewRadius.Length > 1)
        {
            Vector3 oposite = (this.transform.position - targetsInViewRadius[0].transform.position).normalized * 2;
            Vector3 position = this.transform.position;
            position.x += oposite.x;
            position.z += oposite.z;
            this.transform.position = position;
        }

        Collider[] PeopleInViewRadius = null;
        PeopleInViewRadius = Physics.OverlapSphere(transform.position, 1f, layermask1);
        if (PeopleInViewRadius.Length > 0 && PeopleInViewRadius[0].gameObject != NPC)
        {
            Vector3 oposite = (this.transform.position - PeopleInViewRadius[0].transform.position).normalized * 2;
            Vector3 position = this.transform.position;
            position.x += oposite.x;
            position.z += oposite.z;
            this.transform.position = position;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == NPC)
        {
            //Self destroys after 100 seconds.
            Destroy(this.gameObject, 1f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, 0.5f);
    }

}
