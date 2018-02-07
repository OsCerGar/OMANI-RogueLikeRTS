using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour {
    [SerializeField] string TagToAttack;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            EnemyNPC.Life -= transform.parent.GetComponent<NPC>().Damage;
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
