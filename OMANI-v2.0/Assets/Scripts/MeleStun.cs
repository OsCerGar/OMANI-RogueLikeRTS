using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class MeleStun : MonoBehaviour {

    [SerializeField] string TagToAttack;
    [SerializeField] float PushBack;
    [SerializeField] float Stuntime;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            EnemyNPC.Life -= transform.parent.GetComponent<NPC>().Damage;
            EnemyNavMesh.velocity = (other.transform.position - transform.position).normalized * PushBack;

            //Stablish Stun Time and make him go stunn!!
            var time = (SharedFloat)other.gameObject.GetComponent<BehaviorTree>().GetVariable("Stuntime");
            time.Value = Stuntime;
            var goStunned = (SharedBool)other.gameObject.GetComponent<BehaviorTree>().GetVariable("Stunned");
            goStunned.Value = true;
        }
    }
    private void OnEnable()
    {
        Debug.Log("funciona");
        StartCoroutine(WaitandDisable());
    }
    IEnumerator WaitandDisable()
    {

        yield return new WaitForSeconds(0.1f);
        transform.gameObject.SetActive(false);
    }
}
