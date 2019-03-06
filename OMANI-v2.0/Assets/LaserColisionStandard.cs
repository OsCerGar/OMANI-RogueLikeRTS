using UnityEngine;

public class LaserColisionStandard : MonoBehaviour
{

    [SerializeField]
    public bool laserEnabled { get; set; }
    Power_Laser powerLaser;
    [SerializeField] float rad;
    ParticleSystem PSArea;

    Rigidbody MovableObjectRigid;
    Powers powers;

    private void Awake()
    {
        powerLaser = FindObjectOfType<Power_Laser>();
        powers = FindObjectOfType<Powers>();
        PSArea = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (laserEnabled)
        {
            LaserCollisions();

            //emit effect of zone
            if (PSArea != null)
            {
                var main = PSArea.main;
                main.startSize = rad * 3f;
                PSArea.Play();
            }
        }
        else
        {
            PSArea.Stop();

        }


    }

    private void LaserCollisions()
    {
        Enemy enemy;
        Interactible interactible;
        Robot ally;
        bool somethingHitted = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, rad);
        foreach (Collider other in targetsInViewRadius)
        {
            if (other.CompareTag("Building"))
            {

                interactible = other.GetComponent<Interactible>();

                if (interactible != null)
                {
                    interactible.Action();
                    if (interactible.actionBool)
                    {
                        powerLaser.setWidth(interactible.linkPrice);
                        somethingHitted = true;
                        powers.ConnectedValue(true, interactible.transform);

                    }

                }
            }

            else if (other.CompareTag("Enemy"))
            {

                enemy = other.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeWeakLaserDamage(4f, 1);
                    somethingHitted = true;
                    powers.ConnectedValue(true, enemy.transform);

                }
            }

            else if (other.CompareTag("People"))
            {

                ally = other.GetComponent<Robot>();

                if (ally != null)
                {
                    
                    ally.robot_energy.Action();
                    somethingHitted = true;
                    powers.ConnectedValue(true, ally.ball);

                }
            }

            else if (other.CompareTag("Inactive"))
            {

                ally = other.GetComponent<Robot>();

                if (ally != null)
                {
                    ally.robot_energy.Action();
                    somethingHitted = true;
                    powers.ConnectedValue(true, ally.ball);

                }
            }

            else if (other.CompareTag("MovableObject"))
            {
                if (MovableObjectRigid != null && MovableObjectRigid.gameObject == other.gameObject)
                {
                    MovableObjectRigid.AddForce(Vector3.Normalize(MovableObjectRigid.transform.position - transform.position) * 10f, ForceMode.Force);
                }
                else
                {
                    MovableObjectRigid = other.GetComponent<Rigidbody>();
                    MovableObjectRigid.AddForce(Vector3.Normalize(MovableObjectRigid.transform.position - transform.position) * 10f, ForceMode.Force);
                }
            }
        }

        if (somethingHitted != true)
        {
            powerLaser.setWidth(1);
            //powers.ConnectedValue(false, null);
        }


    }
}
