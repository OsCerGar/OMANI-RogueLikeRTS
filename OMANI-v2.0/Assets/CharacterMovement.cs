using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody rb;
    LookDirectionsAndOrder LookDirection;

    public float speed = 1f, originalSpeed = 6f, smooth = 5f;

    public float dashCooldown;

    [SerializeField]
    private float minDistanceToGround, maxDistanceToGround;

    Vector3 desiredDirection;

    //audio
    AudioSource _sand;
    bool onMovement = false, ableToMove = true;
    public float onMovementTime, onNoMovementTime = 0;

    //Sound events

    public delegate void Stopped();
    public static event Stopped OnStopping;

    PlayerInputInterface inputs;
    Powers powers;

    //Animator 
    public Animator anim;

    float x, y;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LookDirection = FindObjectOfType<LookDirectionsAndOrder>();
        inputs = FindObjectOfType<PlayerInputInterface>();
        powers = FindObjectOfType<Powers>();
    }

    private void Update()
    {
        if (ableToMove)
        {

            dashCooldown += Time.deltaTime;
            if (inputs.Dash)
            {
                if (dashCooldown > 3f)
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("DownBlend"))
                    {
                        //if (!anim.GetBool("TurnLeft180") && !anim.GetBool("TurnRight180"))
                        //{
                            if (inputs.MovementAxis.x != 0f || inputs.MovementAxis.y != 0f)
                            {
                                DirectRotate(desiredDirection);
                            }
                            else if (inputs.MovementAxisController.x > 0.2f || inputs.MovementAxisController.x < -0.2f || inputs.MovementAxisController.y > 0.2f || inputs.MovementAxisController.y < -0.2f)
                            {
                                DirectRotate(desiredDirection);
                            }
                        //}

                        anim.SetBool("Dash", true);
                        dashCooldown = 0;
                    }
                }
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("ANIM_DASH"))
            {
                anim.SetBool("Dash", false);
            }

            //If not dashing it moves
            if (!anim.GetBool("Dash"))
            {
                MovementController();
            }
        }

        ///This should stop the character when not able to move
        else
        {
            onNoMovementTime += Time.deltaTime;
            anim.SetBool("OnMovement", false);

            anim.SetFloat("X", Mathf.Lerp(anim.GetFloat("X"), 0, 0.25f));
            anim.SetFloat("Y", Mathf.Lerp(anim.GetFloat("Y"), 0, 0.25f));

        }
    }

    //Function in charge of the Main Character movement. Sends commands to the animator and allows the character to rotate.
    void MovementController()
    {
        if (inputs.Laser || Army.army.currentFighter != null)
        {
            anim.SetBool("Laser", true);
            if (!onMovement)
            {
                float angle = Vector3.SignedAngle(LookDirection.miradaposition, transform.position, Vector3.up);
                angle = angle / 160;


                anim.SetFloat("Z", angle);
                //Rotate((LookDirection.miradaposition - transform.position).normalized);

            }
        }
        else
        {
            anim.SetBool("Laser", false);
        }
        // If the axis has any sort of input on WASD.
        if (inputs.MovementAxis.x != 0f || inputs.MovementAxis.y != 0f)
        {
            onNoMovementTime = 0;
            anim.SetBool("OnMovement", true);
            if (inputs.Laser || Army.army.currentFighter != null)
            {
                //if (controller.isGrounded)
                //{
                onMovement = true;


                desiredDirection = new Vector3(inputs.MovementAxis.x, 0f, inputs.MovementAxis.y);
                //In case the player moves diagonally, it's normalized so that the speed is the same.
                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection = desiredDirection.normalized;
                }
                //}

                desiredDirection.y -= 1 * Time.deltaTime;
                desiredDirection.y = 0;


                float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
                Vector3 finalDirection = Quaternion.Euler(0, -angle, 0) * desiredDirection;

                x = Mathf.Lerp(x, finalDirection.x, 0.25f);
                y = Mathf.Lerp(y, finalDirection.z, 0.25f);

                anim.SetFloat("X", x);
                anim.SetFloat("Y", y);

                Rotate((LookDirection.miradaposition - transform.position).normalized);
            }

            else
            {

                //if (controller.isGrounded)
                //{
                onMovement = true;

                desiredDirection = new Vector3(inputs.MovementAxis.x, 0f, inputs.MovementAxis.y);
                //In case the player moves diagonally, it's normalized so that the speed is the same.
                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection = desiredDirection.normalized;
                }
                //}

                desiredDirection.y -= 1 * Time.deltaTime;


                float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
                Vector3 finalDirection = Quaternion.Euler(0, -angle, 0) * desiredDirection;

                float angleDesiredDirection = Vector3.SignedAngle(desiredDirection, transform.forward, Vector3.up);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("DownBlend"))
                {

                    if (angleDesiredDirection > 150)
                    {
                        anim.SetBool("TurnLeft180", true);
                    }

                    if (angleDesiredDirection < -150)
                    {
                        anim.SetBool("TurnRight180", true);
                    }

                }

                else
                {
                    anim.SetBool("TurnRight180", false);
                    anim.SetBool("TurnLeft180", false);
                }
                anim.SetFloat("X", finalDirection.x);
                anim.SetFloat("Y", finalDirection.z);


            }


        }


        // If the axis has any sort of input on Joystick.
        else if (inputs.MovementAxisController.x > 0.2f || inputs.MovementAxisController.x < -0.2f || inputs.MovementAxisController.y > 0.2f || inputs.MovementAxisController.y < -0.2f)
        {
            onNoMovementTime = 0;
            anim.SetBool("OnMovement", true);

            if (inputs.Laser || Army.army.currentFighter != null)
            {


                //if (controller.isGrounded)
                //{
                onMovement = true;

                desiredDirection = new Vector3(inputs.MovementAxisController.x, 0f, inputs.MovementAxisController.y);

                //In case the player moves diagonally, it's normalized so that the speed is the same.
                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection = desiredDirection.normalized;
                }

                //}
                desiredDirection.y -= 1 * Time.deltaTime;

                float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
                Vector3 finalDirection = Quaternion.Euler(0, -angle, 0) * desiredDirection;

                //controller.Move(desiredDirection * speed * Time.deltaTime);
                anim.SetFloat("X", finalDirection.x);
                anim.SetFloat("Y", finalDirection.z);
                LookDirection.LookAtWhileMoving(inputs.MovementAxisController.x, inputs.MovementAxisController.y);
                Rotate((LookDirection.miradaposition - transform.position).normalized);
            }
            else
            {
                //if (controller.isGrounded)
                //{
                onMovement = true;

                desiredDirection = new Vector3(inputs.MovementAxisController.x, 0f, inputs.MovementAxisController.y);
                //In case the player moves diagonally, it's normalized so that the speed is the same.
                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection = desiredDirection.normalized;
                }
                //}

                desiredDirection.y -= 1 * Time.deltaTime;


                float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
                Vector3 finalDirection = Quaternion.Euler(0, -angle, 0) * desiredDirection;

                float angleDesiredDirection = Vector3.SignedAngle(desiredDirection, transform.forward, Vector3.up);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("DownBlend"))
                {


                    if (angleDesiredDirection > 150)
                    {
                        anim.SetBool("TurnLeft180", true);
                    }

                    if (angleDesiredDirection < -150)
                    {

                        anim.SetBool("TurnRight180", true);
                    }
                }

                else
                {
                    anim.SetBool("TurnRight180", false);
                    anim.SetBool("TurnLeft180", false);
                }
                x = Mathf.Lerp(x, finalDirection.x, 0.25f);
                y = Mathf.Lerp(y, finalDirection.z, 0.25f);

                anim.SetFloat("X", x);
                anim.SetFloat("Y", y);

            }
        }

        else
        {
            desiredDirection = Vector3.zero;

            anim.SetBool("OnMovement", false);
        }

        if (onMovement)
        {
            if (inputs.MovementAxis.x == 0f && inputs.MovementAxis.y == 0f && inputs.MovementAxisController.x == 0 && inputs.MovementAxisController.y == 0)
            {
                anim.SetBool("TurnRight", false);
                anim.SetBool("TurnRight180", false);
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnLeft180", false);

                onMovement = false;

                //sound 
                //OnStopping();
                onMovementTime = 0;
            }

            anim.SetBool("Iddle", false);

        }
        else
        {
            onNoMovementTime += Time.deltaTime;

            anim.SetFloat("X", Mathf.Lerp(anim.GetFloat("X"), 0, 0.25f));
            anim.SetFloat("Y", Mathf.Lerp(anim.GetFloat("Y"), 0, 0.25f));

            if (onNoMovementTime > 5)
            {
                anim.SetBool("Iddle", true);
            }
        }
    }

    // Function that makes the rotation of the character look good.
    /* This rotate function is called upon each frame. The rotation is smoothed.*/
    void Rotate(Vector3 desiredDirection)
    {
        desiredDirection.y = 0.0f;

        // Calculates the rotation at which the character is directed.
        Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);

        // Smoothes the transition
        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smooth * Time.deltaTime);

        // Uses the rigidbody function  "MoveRotation" which sets the new rotation of the Rigidbody. 
        transform.rotation = smoothedRotation;

    }

    // Function that makes the rotation of the character look good.
    /* This rotate function is called upon each frame. The rotation is smoothed.*/
    void DirectRotate(Vector3 desiredDirection)
    {
        desiredDirection.y = 0.0f;

        // Calculates the rotation at which the character is directed.
        Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);


        // Uses the rigidbody function  "MoveRotation" which sets the new rotation of the Rigidbody. 
        transform.rotation = desiredRotation;

    }
    public void StopMovement()
    {
        ableToMove = false;
    }
    public void AbleToMove()
    {
        ableToMove = true;
    }
}


