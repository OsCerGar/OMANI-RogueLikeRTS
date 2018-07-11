using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Cable_end : Interactible
{
    private CableComponent cable;
    private SpringJoint spring;
    private Transform lastParent, destination;
    private bool collecting = false, collectingStarters = false, launching = false;
    private float maxDistance, speed = 0.2f, startTime, journeyLength, timer;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        disableRigid();

        cable = this.transform.parent.GetComponent<CableComponent>();
        spring = this.transform.parent.GetComponent<SpringJoint>();

        maxDistance = spring.maxDistance;
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

            if (distance < 4f)
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
                spring.maxDistance = maxDistance;
            }
        }
        if (launching == true)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > 2f)
            {
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

    public void Launch(GameObject _destination)
    {

        disableRigid();
        launching = true;
        // Calculate the journey length.

        this.transform.SetParent(null);
        journeyLength = Vector3.Distance(this.transform.position, _destination.transform.position);

        startTime = Time.time;
        timer = 0;
        destination = _destination.transform;

    }
}
