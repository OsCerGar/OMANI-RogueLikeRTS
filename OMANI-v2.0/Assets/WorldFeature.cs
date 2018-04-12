using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFeature : MonoBehaviour {

    public float Rad;

    // Update is called once per frame
    void Update() {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, Rad);
    }
    public Vector3 getPos ()
    {
        return transform.position;
        }

}
