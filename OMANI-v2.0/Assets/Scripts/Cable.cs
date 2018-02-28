using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{

    [SerializeField]
    Rigidbody cableStart, cableEnd;

    public List<RopeSection> allRopeSections = new List<RopeSection>();

    float ropeSectionLength = 0.5f;

    //Data we can change to change the properties of the rope
    //Spring constant
    public float kRope = 40f;
    //Damping from rope friction constant
    public float dRope = 2f;
    //Damping from air resistance constant
    public float aRope = 0.05f;
    //Mass of one rope section
    public float mRopeSection = 0.2f;

    // Use this for initialization
    void Start()
    {
        //CreateJoint(cableStart, cableEnd);
        Vector3 pos = cableStart.position;
        List<Vector3> ropePositions = new List<Vector3>();

        for (int i = 0; i < 3; i++)
        {
            ropePositions.Add(cableStart.position);

            pos.y -= ropeSectionLength;
        }

        //But add the rope sections from bottom because it's easier to add
        //more sections to it if we have a winch
        for (int i = ropePositions.Count - 1; i >= 0; i--)
        {
            allRopeSections.Add(new RopeSection(ropePositions[i]));
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Display the rope with the line renderer
        DisplayRope();
    }
    private void FixedUpdate()
    {
        RopeSections();

        if (allRopeSections.Count > 0)
        {
            //Simulate the rope
            //How accurate should the simulation be?
            int iterations = 1;

            //Time step
            float timeStep = Time.fixedDeltaTime / (float)iterations;

            for (int i = 0; i < iterations; i++)
            {
                UpdateRopeSimulation(allRopeSections, timeStep);
            }
        }

    }

    void RopeSections()
    {
        int numberOfRopePositions = Mathf.RoundToInt(Vector3.Distance(cableStart.position, cableEnd.position) / ropeSectionLength);
        int ropeDifference;
        if (numberOfRopePositions != allRopeSections.Count - 1)
        {
            ropeDifference = numberOfRopePositions - allRopeSections.Count - 1;
            RopeSectionsTotal(ropeDifference);
        }

    }

    void RopeSectionsTotal(int _ropeDifference)
    {
        if (Mathf.Sign(_ropeDifference) > 0)
        {
            for (int i = _ropeDifference; i > 0; i--)
            {
                allRopeSections.Add(new RopeSection(allRopeSections[allRopeSections.Count - 1].pos + new Vector3(0, 0.5f, 0)));
            }
        }
        else
        {
            for (int i = _ropeDifference; i < 0; i++)
            {
                allRopeSections.Remove(allRopeSections[allRopeSections.Count - 1]);
            }
        }

    }

/*
    void CreateJoint(Rigidbody body1, Rigidbody body2)
    {
        if (!body1 || !body2) return;

        ConfigurableJoint joint = body2.gameObject.AddComponent<ConfigurableJoint>();
        joint.anchor = new Vector3(0, 0, 0.5f);
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        //joint.angularXMotion = ConfigurableJointMotion.Locked;
        //joint.angularYMotion = ConfigurableJointMotion.Locked;
        //joint.angularZMotion = ConfigurableJointMotion.Locked;
        joint.targetPosition = new Vector3(0, 0, 0);
        joint.connectedBody = body1;
        DisplayRope();
    }
*/
    //Display the rope with a line renderer
    private void DisplayRope()
    {
        LineRenderer lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        float ropeWidth = 0.2f;

        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;

        //An array with all rope section positions
        Vector3[] positions = new Vector3[allRopeSections.Count];

        for (int i = 0; i < allRopeSections.Count; i++)
        {
            positions[i] = allRopeSections[i].pos;
        }

        lineRenderer.positionCount = positions.Length;

        lineRenderer.SetPositions(positions);
    }


    private void UpdateRopeSimulation(List<RopeSection> allRopeSections, float timeStep)
    {
        //Move the last position, which is the top position, to what the rope is attached to
        RopeSection lastRopeSection = allRopeSections[allRopeSections.Count - 1];

        lastRopeSection.pos = cableEnd.position;

        allRopeSections[allRopeSections.Count - 1] = lastRopeSection;

        //Move the first position, which is the top position, to what the rope is attached to
        RopeSection firstRopeSection = allRopeSections[0];

        firstRopeSection.pos = cableStart.position;

        allRopeSections[0] = firstRopeSection;


        //
        //Calculate the next pos and vel with Forward Euler
        //
        //Calculate acceleration in each rope section which is what is needed to get the next pos and vel
        List<Vector3> accelerations = CalculateAccelerations(allRopeSections);

        List<RopeSection> nextPosVelForwardEuler = new List<RopeSection>();

        //Loop through all line segments (except the last because it's always connected to something)
        for (int i = 0; i < allRopeSections.Count - 1; i++)
        {
            RopeSection thisRopeSection = RopeSection.zero;

            //Forward Euler
            //vel = vel + acc * t
            thisRopeSection.vel = allRopeSections[i].vel + accelerations[i] * timeStep;

            //pos = pos + vel * t
            thisRopeSection.pos = allRopeSections[i].pos + allRopeSections[i].vel * timeStep;

            //Save the new data in a temporarily list
            nextPosVelForwardEuler.Add(thisRopeSection);
        }

        //Add the last which is always the same because it's attached to something
        nextPosVelForwardEuler.Add(allRopeSections[allRopeSections.Count - 1]);
        //Add the first one which is always the same because it's attached to something
        nextPosVelForwardEuler.Add(allRopeSections[0]);

        //
        //Calculate the next pos with Heun's method (Improved Euler)
        //
        //Calculate acceleration in each rope section which is what is needed to get the next pos and vel
        List<Vector3> accelerationFromEuler = CalculateAccelerations(nextPosVelForwardEuler);

        List<RopeSection> nextPosVelHeunsMethod = new List<RopeSection>();

        //Loop through all line segments (except the last because it's always connected to something)
        for (int i = 0; i < allRopeSections.Count - 1; i++)
        {
            RopeSection thisRopeSection = RopeSection.zero;

            //Heuns method
            //vel = vel + (acc + accFromForwardEuler) * 0.5 * t
            thisRopeSection.vel = allRopeSections[i].vel + (accelerations[i] + accelerationFromEuler[i]) * 0.5f * timeStep;

            //pos = pos + (vel + velFromForwardEuler) * 0.5f * t
            thisRopeSection.pos = allRopeSections[i].pos + (allRopeSections[i].vel + nextPosVelForwardEuler[i].vel) * 0.5f * timeStep;

            //Save the new data in a temporarily list
            nextPosVelHeunsMethod.Add(thisRopeSection);
        }

        //Add the last which is always the same because it's attached to something
        nextPosVelHeunsMethod.Add(allRopeSections[allRopeSections.Count - 1]);

        //Add the first one which is always the same because it's attached to something
        nextPosVelForwardEuler.Add(allRopeSections[0]);


        //From the temp list to the main list
        for (int i = 0; i < allRopeSections.Count; i++)
        {
            allRopeSections[i] = nextPosVelHeunsMethod[i];

            //allRopeSections[i] = nextPosVelForwardEuler[i];
        }


        //Implement maximum stretch to avoid numerical instabilities
        //May need to run the algorithm several times
        /*int maximumStretchIterations = 2;

        for (int i = 0; i < maximumStretchIterations; i++)
        {
            ImplementMaximumStretch(allRopeSections);
        }*/
    }
    //Calculate accelerations in each rope section which is what is needed to get the next pos and vel
    private List<Vector3> CalculateAccelerations(List<RopeSection> allRopeSections)
    {
        List<Vector3> accelerations = new List<Vector3>();

        //Spring constant
        float k = kRope;
        //Damping constant
        float d = dRope;
        //Damping constant from air resistance
        float a = aRope;
        //Mass of one rope section
        float m = mRopeSection;
        //How long should the rope section be
        float wantedLength = ropeSectionLength;


        //Calculate all forces once because some sections are using the same force but negative
        List<Vector3> allForces = new List<Vector3>();

        for (int i = 0; i < allRopeSections.Count - 1; i++)
        {
            //From Physics for game developers book
            //The force exerted on body 1
            //pos1 (above) - pos2
            Vector3 vectorBetween = allRopeSections[i + 1].pos - allRopeSections[i].pos;

            float distanceBetween = vectorBetween.magnitude;

            Vector3 dir = vectorBetween.normalized;

            float springForce = k * (distanceBetween - wantedLength);


            //Damping from rope friction 
            //vel1 (above) - vel2
            float frictionForce = d * ((Vector3.Dot(allRopeSections[i + 1].vel - allRopeSections[i].vel, vectorBetween)) / distanceBetween);


            //The total force on the spring
            Vector3 springForceVec = -(springForce + frictionForce) * dir;

            //This is body 2 if we follow the book because we are looping from below, so negative
            springForceVec = -springForceVec;

            allForces.Add(springForceVec);
        }


        //Loop through all line segments (except the last because it's always connected to something)
        //and calculate the acceleration
        for (int i = 0; i < allRopeSections.Count - 1; i++)
        {
            Vector3 springForce = Vector3.zero;

            //Spring 1 - above
            springForce += allForces[i];

            //Spring 2 - below
            //The first spring is at the bottom so it doesnt have a section below it
            if (i != 0)
            {
                springForce -= allForces[i - 1];
            }

            //Damping from air resistance, which depends on the square of the velocity
            float vel = allRopeSections[i].vel.magnitude;

            Vector3 dampingForce = a * vel * vel * allRopeSections[i].vel.normalized;

            //The mass attached to this spring
            float springMass = m;

            //end of the rope is attached to a box with a mass
            if (i == 0)
            {
                springMass += cableEnd.GetComponent<Rigidbody>().mass;
            }

            //Force from gravity
            Vector3 gravityForce = springMass * new Vector3(0f, -9.81f, 0f);

            //The total force on this spring
            Vector3 totalForce = springForce + gravityForce - dampingForce;

            //Calculate the acceleration a = F / m
            Vector3 acceleration = totalForce / springMass;

            accelerations.Add(acceleration);
        }

        //The last line segment's acc is always 0 because it's attached to something
        accelerations.Add(Vector3.zero);


        return accelerations;
    }

}
