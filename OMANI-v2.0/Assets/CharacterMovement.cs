using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody rb;
    LookDirectionsAndOrder LookDirection;

    public float speed = 0.073f, originalSpeed = 6f, smooth = 5f;
    [SerializeField]
    private float minDistanceToGround, maxDistanceToGround;

    Vector3 desiredDirection;

    //audio
    AudioSource _sand;
    bool onMovement = false;
    public float onMovementTime, onNoMovementTime = 0;

    //Sound events

    public delegate void Stopped();
    public static event Stopped OnStopping;

    PlayerInputInterface inputs;

    //Animator 
    [SerializeField]
    Animator anim;

    float x, y;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LookDirection = FindObjectOfType<LookDirectionsAndOrder>();
        inputs = FindObjectOfType<PlayerInputInterface>();
    }

    private void Update()
    {
        //This function controls the movement.
        MovementController();


    }

    //Function in charge of the Main Character movement. Sends commands to the animator and allows the character to rotate.
    void MovementController()
    {
        if (inputs.Laser)
        {
            anim.SetBool("Laser", true);
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

            if (inputs.Laser)
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

                x = Mathf.Lerp(x, finalDirection.x, 0.2f);
                y = Mathf.Lerp(y, finalDirection.z, 0.2f);

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
                    if (angleDesiredDirection > 80 && angleDesiredDirection < 150)
                    {
                        anim.SetBool("TurnLeft", true);
                    }
                    else if (angleDesiredDirection > 150)
                    {
                        anim.SetBool("TurnLeft180", true);
                    }
                    else if (angleDesiredDirection < -80 && angleDesiredDirection > -150)
                    {
                        anim.SetBool("TurnRight", true);
                    }
                    else if (angleDesiredDirection < -150)
                    {
                        anim.SetBool("TurnRight180", true);
                    }

                    else
                    {
                        anim.SetBool("TurnRight", false);
                        anim.SetBool("TurnRight180", false);
                        anim.SetBool("TurnLeft", false);
                        anim.SetBool("TurnLeft180", false);
                        x = Mathf.Lerp(x, finalDirection.x, 0.5f);
                        y = Mathf.Lerp(y, finalDirection.z, 0.5f);
                        anim.SetFloat("X", x);
                        anim.SetFloat("Y", y);

                    }
                }



            }
        }

        // If the axis has any sort of input on Joystick.
        else if (inputs.MovementAxisController.x > 0.2f || inputs.MovementAxisController.x < -0.2f || inputs.MovementAxisController.y > 0.2f || inputs.MovementAxisController.y < -0.2f)
        {
            onNoMovementTime = 0;
            anim.SetBool("OnMovement", true);

            if (inputs.Laser)
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

                //controller.Move(desiredDirection * speed * Time.deltaTime);
                anim.SetFloat("X", desiredDirection.x);
                anim.SetFloat("Y", desiredDirection.z);
                LookDirection.LookAtWhileMoving(inputs.MovementAxisController.x, inputs.MovementAxisController.y);
                Rotate(desiredDirection);
            }
        }

        else
        {
            desiredDirection = new Vector3(0, 0, 0);
            //controller.Move(desiredDirection * speed);

            anim.SetBool("OnMovement", false);
        }

        if (onMovement)
        {
            if (inputs.MovementAxis.x == 0f && inputs.MovementAxis.y == 0f)
            {
                anim.SetFloat("X", 0);
                anim.SetFloat("Y", 0);

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
}


