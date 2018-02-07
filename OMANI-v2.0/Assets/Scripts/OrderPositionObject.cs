using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPositionObject : MonoBehaviour
{

    public GameObject NPC;
    public LayerMask targetMask;

    private void Start()
    {
        //Self destroys after 100 seconds.
        Destroy(this.gameObject, 10f);
    }

    void FixedUpdate()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, 0.3f, targetMask, QueryTriggerInteraction.UseGlobal);
        if (targetsInViewRadius.Length > 0)
        {
            Vector3 oposite = (this.transform.position - targetsInViewRadius[0].transform.position).normalized;
            this.transform.position += oposite * 2;
            Debug.Log("Movido" + oposite * 2);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == NPC)
        {
            //Self destroys after 100 seconds.
            Destroy(this.gameObject);
        }
    }



}
