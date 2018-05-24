using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devourer : Enemy {
   [SerializeField] GameObject worker;
    void Awake()
    {
        boyType = "Devourer";
    }
    public override void Update()
    {
        if (state == "Alive")
        {
            if (life <= 0)
            {
                //provisional :D
                Die();
                state = "Dead";
            }
        }
    }
    public override void Die()
    {
        Instantiate<GameObject>(worker,new Vector3 (transform.position.x +1, transform.position.y, transform.position.z),transform.rotation);
        Instantiate<GameObject>(worker, new Vector3(transform.position.x -1, transform.position.y, transform.position.z +1), transform.rotation);
        Instantiate<GameObject>(worker, new Vector3(transform.position.x, transform.position.y, transform.position.z +1), transform.rotation);
        Destroy(transform.gameObject);
    }
}
