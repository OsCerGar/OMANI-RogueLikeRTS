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
    private float maxDistance, speed = 0.2f, startTime, journeyLength, timer;

    // Tops
    List<Transform> tops = new List<Transform>();
    List<BU_Cable_end> cables = new List<BU_Cable_end>();

    [SerializeField]
    int terrainLayer;
    Vector3 finalPositions;
    RaycastHit hit;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //tops
        foreach (Transform child in transform)
        {
            //child is your child transform
            tops.Add(child);
            cables.Add(child.GetChild(0).GetComponent<BU_Cable_end>());
        }

        disableRigid();

        cable = this.transform.parent.GetComponent<CableComponent>();
        spring = this.transform.parent.GetComponent<SpringJoint>();

        maxDistance = spring.maxDistance;
        terrainLayer = (1 << LayerMask.NameToLayer("Terrain"));
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

            if (distance < 6f)
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
            }
        }
        if (launching == true && collecting != true)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > 2f)
            {
                /*
                if (topYesorNo)
                {
                    if (!topDeployed)
                    {
                        deployTops();
                    }
                    else
                    {
                        moveTops();
                    }
                }
                */

                cable.CableLength(journeyLength - 4f);

                // Distance moved = time * speed.
                float distCovered = (Time.time - startTime) * speed;

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

        enableRigid();
        this.transform.SetParent(null);
        collecting = true;
    }

    private void deployTops()
    {
        int i = 0;

        //tops
        foreach (Transform top in tops)
        {
            top.transform.parent = null;
            if (i == 0)
            {
                //top.transform.position = (transform.position + _destination.transform.position) / 2;
                top.transform.position = Vector3.Lerp(transform.position, destination.transform.position, 0.3f);
                top1FinalPosition = top.transform;
                top.transform.position -= new Vector3(0f, 15f, 0f);
            }
            else
            {
                top.transform.position = Vector3.Lerp(transform.position, destination.transform.position, 0.6f);
                top2FinalPosition = top.transform;
                top.transform.position -= new Vector3(0f, 15f, 0f);
            }
            i++;
        }

        topDeployed = true;
    }
    private void moveTops()
    {
        int i = 0;

        //tops
        foreach (Transform top in tops)
        {
            if (i == 0)
            {
                top.transform.position = Vector3.Lerp(top.transform.position, top1FinalPosition.transform.position, 0.5f * Time.unscaledDeltaTime);
            }
            else
            {
                top.transform.position = Vector3.Lerp(top.transform.position, top2FinalPosition.transform.position, 0.5f * Time.unscaledDeltaTime);
            }

            i++;
        }
    }

    private void deployCables()
    {
        int i = 0;
        //tops
        foreach (BU_Cable_end cable in cables)
        {
            if (i == 0)
            {
                cable.Launch(top1FinalPosition, false);
            }
            else
            {
                cable.Launch(top2FinalPosition, false);
            }

            i++;
        }

    }


    public void Launch(Transform _destination, bool tops)
    {

        disableRigid();
        launching = true;
        // Calculate the journey length.

        this.transform.SetParent(null);
        journeyLength = Vector3.Distance(this.transform.position, _destination.transform.position);

        //Tops or not.
        topYesorNo = tops;

        startTime = Time.time;
        timer = 0;
        destination = _destination;
    }
}

