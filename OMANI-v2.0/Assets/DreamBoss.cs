using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamBoss : Enemy {
    [SerializeField] GameObject Meteorite;
    [SerializeField] GameObject ExpansiveAttack;
    [SerializeField] GameObject Mouth;

    public void ShootMeteorite()
    {
        var meteor = Instantiate(Meteorite,Mouth.transform.position, Meteorite.transform.rotation);
        var obj = AI_GetEnemy().transform.position;
        meteor.GetComponent<Meteorite>().objective = new Vector3(obj.x,obj.y-2,obj.z);
    }
    public void ShootExpansiveAttack()
    {
        var meteor = Instantiate(ExpansiveAttack,new Vector3(transform.position.x,0.5f, transform.position.z) , transform.rotation);
    }
}
