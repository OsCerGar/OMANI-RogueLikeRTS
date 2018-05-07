using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPositionObject : MonoBehaviour
{

    public NPC npc = null;

    int layermask1 = 1 << 9;
    int layermask2 = 1 << 11;

    float time;

    private void Start()
    {
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 4f)
        {
            this.transform.position = npc.transform.position;
            npc.AI_SetTarget(null);
            //Self destroys after x seconds. It shouldn't autodestroy like this.
            Destroy(this.gameObject, 7f);

        }
    }
    void FixedUpdate()
    {
        Collider[] targetsInViewRadius = null;
        targetsInViewRadius = Physics.OverlapSphere(transform.position, 1.5f, layermask2, QueryTriggerInteraction.UseGlobal);
        if (targetsInViewRadius.Length > 1)
        {
            Vector3 oposite = (this.transform.position - targetsInViewRadius[0].transform.position).normalized;
            Vector3 position = this.transform.position;
            position.x += oposite.x;
            position.z += oposite.z;
            this.transform.position = position;
        }

        Collider[] PeopleInViewRadius = null;
        PeopleInViewRadius = Physics.OverlapSphere(transform.position, 1f, layermask1, QueryTriggerInteraction.Ignore);
        if (PeopleInViewRadius.Length > 0 && PeopleInViewRadius[0].gameObject.GetComponent<Player>() == null)
        {
            if (PeopleInViewRadius[0].gameObject != npc.gameObject && PeopleInViewRadius[0].gameObject.GetComponent<NPC>().AI_GetState() != "Follow")
            {
                Vector3 oposite = (this.transform.position - PeopleInViewRadius[0].transform.position).normalized;
                Vector3 position = this.transform.position;
                position.x += oposite.x;
                position.z += oposite.z;
                this.transform.position = position;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == npc.gameObject)
        {
            npc.AI_SetTarget(null);
            //Self destroys
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, 0.5f);
    }

}
