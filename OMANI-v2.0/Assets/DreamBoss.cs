using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class DreamBoss : Enemy {
    [SerializeField] GameObject Meteorite;
    [SerializeField] GameObject SolidMeteorite;
    [SerializeField] GameObject ExpansiveAttack;
    [SerializeField] GameObject Mouth;
    [SerializeField] GameObject[] WeakSpots;


    public void ShootMeteorite()
    {
        var meteor = Instantiate(Meteorite,Mouth.transform.position, Meteorite.transform.rotation);
        var obj = AI_GetEnemy().transform.position;
        meteor.GetComponent<Meteorite>().objective = new Vector3(obj.x + Random.Range(-4,4),obj.y-2,obj.z + Random.Range(-4, 4));
    }
    public void ShootExpansiveAttack()
    {
        var meteor = Instantiate(ExpansiveAttack,new Vector3(transform.position.x,0.5f, transform.position.z) , transform.rotation);
    }
    public void Stun()
    {
        var stateVariable = (SharedBool)AI.GetVariable("Stunned");
        stateVariable.Value = true;
    }
    void activateWeakspot()
    {
        WeakSpots[Random.Range(0, WeakSpots.Length)].GetComponent<WeakSpotBrain>().ActivateWeakSpots();
    }
    void deactivateWeakspot()
    {
        foreach (var item in WeakSpots)
        {
            item.GetComponent<WeakSpotBrain>().DeactivateWeakSpots();
        }
    }
    public void ShootSolidMeteorite()
    {
        GameObject meteor = Instantiate(SolidMeteorite, Mouth.transform.position, SolidMeteorite.transform.rotation);
        Vector3 obj = AI_GetEnemy().transform.position;
        meteor.GetComponent<Meteorite>().objective = new Vector3(obj.x + Random.Range(-4, 4), obj.y - 2, obj.z + Random.Range(-4, 4));
    }




}
