using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootProjectile : MonoBehaviour {

    public ParticleSystem BasicProjectile, AdvancedProjectile;
    public Transform Gun;
	public void Shoot()
    {
        BasicProjectile.transform.position = Gun.position;
        BasicProjectile.transform.rotation = transform.rotation;
        BasicProjectile.Emit(1);
    }

    public void ShootStun()
    {
        AdvancedProjectile.transform.position = Gun.position;
        AdvancedProjectile.transform.rotation = transform.rotation;
        transform.GetComponent<NavMeshAgent>().velocity = transform.forward * -20;
        AdvancedProjectile.Emit(1);
    }
}
