using UnityEngine;

public class LaserColisionStandard : MonoBehaviour
{

    [SerializeField]
    public bool laserEnabled { get; set; }
    Power_Laser powerLaser;
    [SerializeField] float rad;
    ParticleSystem PSArea;

    Rigidbody MovableObjectRigid;
    [SerializeField]
    Powers powers;
    private bool connected;
    private Transform connectObject;
    Enemy enemy;
    Interactible interactible;
    Robot ally;

    [SerializeField]
    AudioSource laserLoop;
    [SerializeField]
    AudioClip regularLaserLoop, interactibleLaserLoop, damageLaserLoop;

    //Inputs
    PlayerInputInterface inputController;

    private void Awake()
    {
        powerLaser = FindObjectOfType<Power_Laser>();
        powers = FindObjectOfType<Powers>();
        inputController = FindObjectOfType<PlayerInputInterface>();
        PSArea = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (laserEnabled)
        {
            if (!connected)
            {
                LaserCollisions();
            }

            if (connected)
            {
                ConnectedLaserBehaviour();
                if (powers.army.currentFighter != null) { enemy = null; interactible = null; ally = null; }
            }

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
            ConnectedValue(false, null);
        }


    }

    private void ConnectedLaserBehaviour()
    {
        //Distance to player check
        if (Vector3.Distance(powers.transform.position, connectObject.transform.position) > 20f)
        {
            ConnectedValue(false, null);
        }

        //Finished transmision
        if (connectObject != null && !connectObject.gameObject.activeInHierarchy)
        {
            ConnectedValue(false, null);
        }

        //Energy
        if (enemy != null)
        {
            enemy.TakeWeakLaserDamage(2f, 4);
            inputController.SetVibration(1, 0.25f, 0.1f, false);

        }
        if (interactible != null)
        {
            interactible.Action();
            if (interactible.actionBool)
            {
                powerLaser.setWidth(interactible.linkPrice);

                inputController.SetVibration(1, interactible.currentLinkPrice / interactible.finalLinkPrice / 3, 0.15f, false);

            }
        }
        if (ally != null)
        {
            ally.robot_energy.Action();
            inputController.SetVibration(1, ally.robot_energy.currentLinkPrice / ally.robot_energy.finalLinkPrice / 3, 0.15f, false);

        }
    }

    public void ConnectedValue(bool _connectedValue, Transform _connectedObject)
    {
        connected = _connectedValue;
        connectObject = _connectedObject;
        powers.ConnectedValue(_connectedValue, _connectedObject);
    }

    private void LaserCollisions()
    {
        enemy = null;
        ally = null;
        interactible = null;

        bool somethingHitted = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, rad);
        foreach (Collider other in targetsInViewRadius)
        {
            if (other.CompareTag("Building") || other.CompareTag("Ennui"))
            {
                interactible = other.GetComponent<Interactible>();

                if (interactible != null)
                {
                    interactible.Action();
                    if (interactible.actionBool)
                    {
                        powerLaser.setWidth(interactible.linkPrice);
                        somethingHitted = true;
                        ConnectedValue(true, interactible.laserTarget);
                        laserLoop.clip = interactibleLaserLoop;
                        if (!laserLoop.isPlaying)
                        {
                            laserLoop.Play();
                        }
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
                    ConnectedValue(true, enemy.laserTarget);
                    laserLoop.clip = damageLaserLoop;
                    if (!laserLoop.isPlaying)
                    {
                        laserLoop.Play();
                    }
                }
            }

            else if (other.CompareTag("People"))
            {

                ally = other.GetComponent<Robot>();

                if (ally != null)
                {
                    ally.robot_energy.Action();
                    somethingHitted = true;
                    ConnectedValue(true, ally.ball);
                    laserLoop.clip = interactibleLaserLoop;
                    if (!laserLoop.isPlaying)
                    {
                        laserLoop.Play();
                    }
                }
            }

            else if (other.CompareTag("Inactive"))
            {
                ally = other.GetComponent<Robot>();

                if (ally != null)
                {
                    ally.robot_energy.Action();
                    somethingHitted = true;
                    ConnectedValue(true, ally.ball);
                    laserLoop.clip = interactibleLaserLoop;
                    if (!laserLoop.isPlaying)
                    {
                        laserLoop.Play();
                    }

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

        if (!somethingHitted)
        {
            powerLaser.setWidth(1);
            laserLoop.clip = regularLaserLoop;
            if (!laserLoop.isPlaying)
            {
                laserLoop.Play();
            }

            //powers.ConnectedValue(false, null);
        }
        else
        {

        }


    }
}
