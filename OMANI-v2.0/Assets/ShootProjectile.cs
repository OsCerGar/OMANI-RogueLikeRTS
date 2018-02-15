using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour {

    public ParticleSystem Projectile;
    public Transform Gun;
	public void Shoot()
    {
        Projectile.transform.position = Gun.position;
        Projectile.transform.rotation = Gun.rotation;
        Projectile.Emit(1);
    }

    public void ShootStun()
    {
        Projectile.transform.position = Gun.position;
        Projectile.transform.rotation = Gun.rotation;
        Projectile.Emit(1);
    }
}
