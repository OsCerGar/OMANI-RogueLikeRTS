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
        meteor.GetComponent<Meteorite>().objective = AI_GetEnemy().transform.position;
    }
    public void ShootExpansiveAttack()
    {
        var meteor = Instantiate(ExpansiveAttack, transform.position, transform.rotation);
    }
}
