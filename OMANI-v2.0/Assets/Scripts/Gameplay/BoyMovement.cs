using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoyMovement : MonoBehaviour
{

    #region Variables

    //Surface Hitted
    public RaycastHit hittedground;

    //Movement Related
    [HideInInspector]
    public bool onRoll = false;
    public List<Interactible> grabbedObject = new List<Interactible>();
    private Transform lastParent;
    [SerializeField]
    public GameObject hand;

    //Smoothing variables for turning the character
    private float smooth = 15f;
    private float rollTimer;
    //Timer to remove input from the player while in ragdoll.
    private float ragdollTimer = 0;
    //Speed for the animation
    private float AnimSpeed;
    //Time for the animation blend
    private float t, t2;

    //Reference Variables
    private Animator anim;
    private Animation animation;
    private Rigidbody rigid;
    private Ragdoll ragdll;
    private Collider coll;
    private LookDirectionsAndOrder LookDirection;

    #endregion

    #region Ragdollvariables
    //Additional vectors for storing the pose the ragdoll ended up in.
    [HideInInspector]
    public Vector3 ragdolledHipPosition, ragdolledHeadPosition, ragdolledFeetPosition;
    [HideInInspector]
    //A helper variable to store the time when we transitioned from ragdolled to blendToAnim state
    float ragdollingEndTime = -100;
    [HideInInspector]

    //How long do we blend when transitioning from ragdolled to animated
    public float ragdollToMecanimBlendTime = 0.5f;
    float mecanimToGetUpTransitionTime = 0.05f;

    public bool ragdolled
    {
        get
        {
            return state != RagdollState.animated;
        }
        set
        {
            if (value == true)
            {
                if (state == RagdollState.animated)
                {
                    //Transition from animated to ragdolled
                    ragdll.ragdollTrue(); //allow the ragdoll RigidBodies to react to the environment
                    anim.enabled = false; //disable animation
                    state = RagdollState.ragdolled;
                }
            }
            else
            {
                if (state == RagdollState.ragdolled)
                {
                    ragdll.ragdollFalse();
                    ragdollingEndTime = Time.time; //store the ragdollind ens time                           
                    anim.enabled = true;
                    state = RagdollState.blendToAnim;

                    //Remember some key positions
                    ragdolledFeetPosition = 0.5f * (anim.GetBoneTransform(HumanBodyBones.LeftFoot).position + anim.GetBoneTransform(HumanBodyBones.RightFoot).position);
                    ragdolledFeetPosition.y += 0.65f;
                    ragdolledHeadPosition = anim.GetBoneTransform(HumanBodyBones.Head).position;
                    ragdolledHeadPosition.y += 0.65f;
                    ragdolledHipPosition = anim.GetBoneTransform(HumanBodyBones.Hips).position;
                    ragdolledHipPosition.y += 0.65f;

                    //Initiate the get up animation
                    if (anim.GetBoneTransform(HumanBodyBones.Hips).forward.y > 0) //hip hips forward vector pointing upwards, initiate the get up from back animation
                    {
                        anim.SetBool("GetUpFromBack", true);
                    }
                    if (anim.GetBoneTransform(HumanBodyBones.Hips).forward.y < 0)
                    {
                        anim.SetBool("GetUpFromBelly", true);
                    }


                } //if (state==RagdollState.ragdolled)
            }   //if value==false	
        } //set
    }

    enum RagdollState
    {
        animated,    //Mecanim is fully in control
        ragdolled,   //Mecanim turned off, physics controls the ragdoll
        blendToAnim  //Mecanim in control, but LateUpdate() is used to partially blend in the last ragdolled pose
    }

    //The current state
    RagdollState state = RagdollState.animated;
    #endregion

    private void Awake()
    {
        SetInitialReferences();
        anim.SetLayerWeight(1, 1f); //TODO : Study.
    }

    // Sets all the references for the script
    void SetInitialReferences()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        animation = GetComponent<Animation>();
        coll = GetComponent<CapsuleCollider>();
        LookDirection = FindObjectOfType<LookDirectionsAndOrder>();
        //Gets the component of the children of the main character, which is "ProtaInterior".
        /*
         * Ragdoll searches for the components in children in charge of the Ragdoll system 
         * and desactivates or activates them. 
         */

        ragdll = GetComponentInChildren<Ragdoll>();

    }

    private void Start()
    {

    }

    void FixedUpdate()
    {
        // MOVEMENT

        #region Inputs
        // This stores the input in both vertical and horizontal axis. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // This stores the input in both vertical and horizontal axis donde by the left joystick. 
        float hj = Input.GetAxis("HorizontalJoystick");
        float vj = Input.GetAxis("VerticalJoystick");
        #endregion

        //This function controls the movement.
        MovementController(h, v, hj, vj);


    }

    // Called uppon each frame. 
    private void Update()
    {
        //Timer for the character to get up from ragdoll.
        ragdollTimer += Time.deltaTime;
        rollTimer += Time.deltaTime;

        //ROLL 
        // If space is pressed.
        //If the get up animation is not playing and ragdolled is false
        if (Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 1"))
            Action();
    }

    private void Action()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("GetUp_From_Belly") && !anim.GetCurrentAnimatorStateInfo(0).IsName("GetUp_From_Back") && ragdollTimer > 2)
        {
            if (grabbedObject.Count < 3)
            {
                Collider[] objectsInArea = null;
                objectsInArea = Physics.OverlapSphere(transform.position, 3f, 1 << 14);

                float minDistance = 0;
                GameObject closest = null;
                bool alreadyGrabbedObject = false;

                // Checks if there are interactible objects nearby
                if (objectsInArea.Length < 1)
                {
                    Roll();
                }

                else
                {
                    //If there are, the closest one.
                    for (int i = 0; i < objectsInArea.Length; i++)
                    {
                        if (objectsInArea[i].tag == "Interactible")
                        {
                            foreach (Interactible interact in grabbedObject)
                            {
                                if (interact.gameObject == objectsInArea[i].gameObject)
                                {
                                    alreadyGrabbedObject = true;
                                }
                            }

                            if (alreadyGrabbedObject == false)
                            {
                                float distance = Vector3.Distance(objectsInArea[i].transform.position, this.gameObject.transform.position);

                                if (minDistance == 0 || minDistance > distance)
                                {
                                    minDistance = distance;
                                    closest = objectsInArea[i].gameObject;
                                }
                            }
                        }

                    }
                }

                if (closest != null)
                {
                    //Does whatever Action does
                    //Sends himself to the action manager
                    closest.GetComponent<Interactible>().Action(this);
                }
                else if (grabbedObject.Count > 0)
                {
                    grabbedObject[grabbedObject.Count - 1].Action(this);

                }
            }


            else
            {
                grabbedObject[grabbedObject.Count - 1].Action(this);
            }
        }
    }


    private void Roll()
    {

        if (rollTimer > 1.75f && onRoll != true)
        {
            onRoll = true;
            rollTimer = 0;
            anim.SetTrigger("Roll");
        }
    }


    // LateUpdate is called after all Update functions have been called.
    private void LateUpdate()
    {

        #region Ragdollstand
        //Clear the get up animation controls so that we don't end up repeating the animations indefinitely
        anim.SetBool("GetUpFromBelly", false);
        anim.SetBool("GetUpFromBack", false);
        if (state == RagdollState.blendToAnim)
        {
            {
                if (Time.time <= ragdollingEndTime + mecanimToGetUpTransitionTime)
                {
                    //If we are waiting for Mecanim to start playing the get up animations, update the root of the mecanim
                    //character to the best match with the ragdoll
                    Vector3 animatedToRagdolled = ragdolledHipPosition - anim.GetBoneTransform(HumanBodyBones.Hips).position;
                    Vector3 newRootPosition = transform.position + animatedToRagdolled;

                    //Now cast a ray from the computed position downwards and find the highest hit that does not belong to the character 
                    RaycastHit[] hits = Physics.RaycastAll(new Ray(newRootPosition, Vector3.down));
                    newRootPosition.y = 0;
                    foreach (RaycastHit hit2 in hits)
                    {
                        if (!hit2.transform.IsChildOf(transform))
                        {
                            newRootPosition.y = Mathf.Max(newRootPosition.y, hit2.point.y);
                        }
                    }
                    transform.position = newRootPosition;

                    //Get body orientation in ground plane for both the ragdolled pose and the animated get up pose
                    Vector3 ragdolledDirection = ragdolledHeadPosition - ragdolledFeetPosition;
                    ragdolledDirection.y = 0;

                    Vector3 meanFeetPosition = 0.5f * (anim.GetBoneTransform(HumanBodyBones.LeftFoot).position + anim.GetBoneTransform(HumanBodyBones.RightFoot).position);
                    Vector3 animatedDirection = anim.GetBoneTransform(HumanBodyBones.Head).position - meanFeetPosition;
                    animatedDirection.y = 0;

                    //Try to match the rotations. Note that we can only rotate around Y axis, as the animated characted must stay upright,
                    //hence setting the y components of the vectors to zero. 
                    transform.rotation *= Quaternion.FromToRotation(animatedDirection.normalized, ragdolledDirection.normalized);
                }
                //compute the ragdoll blend amount in the range 0...1
                float ragdollBlendAmount = 1.0f - (Time.time - ragdollingEndTime - mecanimToGetUpTransitionTime) / ragdollToMecanimBlendTime;
                ragdollBlendAmount = Mathf.Clamp01(ragdollBlendAmount);

                //In LateUpdate(), Mecanim has already updated the body pose according to the animations. 
                //To enable smooth transitioning from a ragdoll to animation, we lerp the position of the hips 
                //and slerp all the rotations towards the ones stored when ending the ragdolling

                foreach (BodyPart b in ragdll.bodyParts)
                {
                    if (b.transform != transform)
                    { //this if is to prevent us from modifying the root of the character, only the actual body parts
                      //position is only interpolated for the hips
                        if (b.transform == anim.GetBoneTransform(HumanBodyBones.Hips))
                            b.transform.position = Vector3.Lerp(b.transform.position, b.storedPosition, ragdollBlendAmount);
                        //rotation is interpolated for all body parts
                        b.transform.rotation = Quaternion.Slerp(b.transform.rotation, b.storedRotation, ragdollBlendAmount);
                    }
                }

                //if the ragdoll blend amount has decreased to zero, move to animated state
                if (ragdollBlendAmount == 0)
                {
                    state = RagdollState.animated;
                    return;
                }
            }

        }
        #endregion

    }

    //Function in charge of the Main Character movement. Sends commands to the animator and allows the character to rotate.
    void MovementController(float horizontal, float vertical, float horizontalJoystick, float verticalJoystick)
    {
        //If the get up animation is not playing and ragdolled is false
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("GetUp_From_Belly") && !anim.GetCurrentAnimatorStateInfo(0).IsName("GetUp_From_Back") && ragdollTimer > 2)
        {
            //MOVEMENT IF NOT ROLLIN

            // If the axis has any sort of input on WASD.
            if (horizontal != 0f || vertical != 0f)
            {

                stopRagdoll();
                AnimSpeed = Mathf.Lerp(0, 4.5f, t);
                t += 2.3f * Time.deltaTime;
                // Calls the Rotate function, which makes the rotation of the character look good.
                if (onRoll != true)
                {
                    Rotate(horizontal, vertical);
                }
                anim.SetFloat("AnimSpeed", AnimSpeed);

            }
            // If the axis has any sort of input on Joystick.
            else if (horizontalJoystick != 0f || verticalJoystick != 0f)
            {
                stopRagdoll();
                AnimSpeed = Mathf.Clamp(Mathf.Abs(horizontalJoystick) + Mathf.Abs(verticalJoystick), 0, 1) * 4.5f;
                // Calls the Rotate function, which makes the rotation of the character look good.
                if (onRoll != true)
                {
                    Rotate(horizontalJoystick, verticalJoystick);
                }
                anim.SetFloat("AnimSpeed", AnimSpeed);

            }
            // If the axis doesn't have any sort of input.
            else
            {
                t = 0.2f;
                AnimSpeed = Mathf.Lerp(AnimSpeed, 0, t);
                t2 -= 0.4f * Time.deltaTime;
                anim.SetFloat("AnimSpeed", AnimSpeed);
            }
        }
    }

    // Function that makes the rotation of the character look good.
    /* This rotate function is called upon each frame. The rotation is smoothed.*/
    void Rotate(float horizontal, float vertical)
    {
        // Determines the new direction of the character
        Vector3 desiredDirection = new Vector3(horizontal, 0f, vertical);

        desiredDirection = Camera.main.transform.TransformDirection(desiredDirection);
        desiredDirection.y = 0.0f;

        // Calculates the rotation at which the character is directed.
        Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);

        // Smoothes the transition
        Quaternion smoothedRotation = Quaternion.Lerp(rigid.rotation, desiredRotation, smooth * Time.deltaTime);

        // Uses the rigidbody function  "MoveRotation" which sets the new rotation of the Rigidbody. 
        rigid.MoveRotation(smoothedRotation);
    }

    //Used by the animation events to play the steps.
    void rightStep()
    {
        /*
        if (Physics.Raycast(transform.position, Vector3.down, out hittedground))
        {
            if (hittedground.collider.gameObject.tag == "Cemento")
            {
                AudioManager.instance.PlayAtPosition("Paso Cemento Derecho", hittedground.transform.position);
            }

            else if (hittedground.collider.gameObject.tag == "Cesped")
            {
                AudioManager.instance.PlayAtPosition("Paso Cesped 1", hittedground.transform.position);
            }
        }
        */
    }
    void leftStep()
    {
        /*
        if (Physics.Raycast(transform.position, Vector3.down, out hittedground))
        {
            if (hittedground.collider.gameObject.tag == "Cemento")
            {
                AudioManager.instance.PlayAtPosition("Paso Cemento Izquierdo", hittedground.transform.position);
            }
            else if (hittedground.collider.gameObject.tag == "Cesped")
            {
                AudioManager.instance.PlayAtPosition("Paso Cesped 2", hittedground.transform.position);
            }
        }
        */
    }

    public void startRagdoll()
    {
        ragdolled = true;
        coll.enabled = false;
        rigid.isKinematic = true;
        ragdollTimer = 0;
    }
    public void stopRagdoll()
    {
        ragdolled = false;
        coll.enabled = true;
        rigid.isKinematic = false;

    }

    //LookAt
    void OnAnimatorIK()
    {

        //LOOK AT MOUSE
        //This function tells the Inverse Kinematics where to look at and stablishes its parameters.
        //LookAtWeight
        /*
         Parameters in order : 
         Global weight(multiplier for all the others), bodyWeight, headWeight, eyesWeight and clampWeight(0 means the character is unrestained in motion).
         */

        anim.SetLookAtWeight(1f, 0.2f, 0.2f, 0.1f, 1f);

        //Position too look at.
        anim.SetLookAtPosition(LookDirection.miradaposition);

        if (grabbedObject.Count > 0)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.6f);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.6f);

            anim.SetIKPosition(AvatarIKGoal.LeftHand, grabbedObject[0].transform.position);
            anim.SetIKPosition(AvatarIKGoal.RightHand, grabbedObject[0].transform.position);

        }
    }
}

