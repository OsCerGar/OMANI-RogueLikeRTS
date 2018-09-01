using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Cable_end : Interactible
{
    private CableComponent cable;
    private SpringJoint spring;
    public Transform destination;
    private Transform lastParent, top1FinalPosition, top2FinalPosition;
    private bool collecting = false, collectingStarters = false, launching = false, topDeployed = false, topYesorNo = false;
    private float maxDistance, speed = 0.2f, startTimess, journeyLength, timer;

    List<BU_Cable_end> cables = new List<BU_Cable_end>();
    BU_Energy buEnergy;

    [SerializeField]
    int terrainLayer;
    Vector3 finalPositions;
    RaycastHit hit;

    // Electric part
    [SerializeField]
    Electric electric;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        buEnergy = FindObjectOfType<BU_Energy>();
        disableRigid();
        maxDistance = spring.maxDistance;
        terrainLayer = (1 << LayerMask.NameToLayer("Terrain"));
    }
    public void OnEnable()
    {
        cable = this.transform.parent.GetComponent<CableComponent>();
        spring = this.transform.parent.GetComponent<SpringJoint>();
    }

    // Update is called once per frame
    public override void Update()
    {

        //Starts Collecting
        if (collecting == true)
        {

            if (collectingStarters == false)
            {
                //This calcs starting distance between cable end and wanted position.
                maxDistance = Vector3.Distance(this.transform.position, cable.transform.position);
                spring.maxDistance = maxDistance;
                collectingStarters = true;
            }

            float distRatio = (maxDistance) / (Vector3.Distance(this.transform.position, cable.transform.position));

            // If its close to the point, it Lerps itself instead of using physics to avoid weird behaviours.
            if (distRatio > 0.2f)
            {
                spring.maxDistance -= (distRatio * 0.075f) + 0.05f;
            }

            float distance = Vector3.Distance(this.transform.position, cable.transform.position);

            if (distance < 7f)
            {
                disableRigid();
                this.transform.position = Vector3.Lerp(this.transform.position, cable.transform.position, 0.4f);
            }

            if (distance < 2.5f)
            {
                this.transform.position = cable.transform.position;
                this.transform.parent = cable.transform;
                collecting = false;
                collectingStarters = false;
                launching = false;


                spring.maxDistance = maxDistance;

                this.transform.parent.gameObject.SetActive(false);
            }
        }
        if (launching == true && collecting != true)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > 0.5f)
            {                

                cable.CableLength(journeyLength - 4f);

                // Distance moved = time * speed.
                float distCovered = (Time.time - startTimess) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                float fracJourney = distCovered / journeyLength;

                // Set our position as a fraction of the distance between the markers.
                transform.position = Vector3.Lerp(this.transform.position, destination.transform.position + new Vector3(0, 1f, 0), fracJourney);
            }
        }
    }

    public void PullBack()
    {
        // Disengage from whatever was attached.
        cable.CableLength(0);
        electric.gameObject.SetActive(false);

        enableRigid();
        this.transform.SetParent(null);
        collecting = true;
    }

    public void PullBackStopWorking()
    {
        destination.parent.parent.GetComponent<Interactible_Repeater>().StopWorkingComplete();
        // Disengage from whatever was attached.
        cable.CableLength(0);
        electric.gameObject.SetActive(false);

        enableRigid();
        this.transform.SetParent(null);
        collecting = true;

    }

    public void Launch(Transform _destination, bool tops)
    {

        disableRigid();
        launching = true;
        // Calculate the journey length.
        electric.gameObject.SetActive(true);
        electric.transformPointB = _destination;

        this.transform.SetParent(null);
        journeyLength = Vector3.Distance(this.transform.position, _destination.transform.position);

        //Tops or not.
        topYesorNo = tops;

        startTimess = Time.time;
        timer = 0;
        destination = _destination;

    }

    public void LaunchVector3(Transform _destination, bool tops)
    {
        //Made this function just to add a freaking offset so i don't have to make the freaking animation again
        disableRigid();
        launching = true;
        // Calculate the journey length.
        electric.gameObject.SetActive(true);

        Transform finalDestination = _destination;
        finalDestination.position = new Vector3(_destination.position.x, _destination.position.y + 2f, _destination.position.z);
        Debug.Log(finalDestination.position);

        electric.transformPointB = finalDestination;

        this.transform.SetParent(null);
        journeyLength = Vector3.Distance(this.transform.position, _destination.transform.position);

        //Tops or not.
        topYesorNo = tops;

        startTimess = Time.time;
        timer = 0;
        destination = _destination;

    }

}

