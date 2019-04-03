using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody rb;
    LookDirectionsAndOrder LookDirection;

    public float speed = 0.073f, originalSpeed = 6f, smooth = 5f;
    [SerializeField]
    private float minDistanceToGround, maxDistanceToGround;
    CharacterController controller;

    Vector3 desiredDirection;

    //audio
    AudioSource _sand;
    bool onMovement = false;
    public float onMovementTime, onNoMovementTime = 0;

    //Sound events

    public delegate void Stopped();
    public static event Stopped OnStopping;

    PlayerInputInterface inputs;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
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
        // If the axis has any sort of input on WASD.
        if (inputs.MovementAxis.x != 0f || inputs.MovementAxis.y != 0f)
        {

            onNoMovementTime = 0;

            if (controller.isGrounded)
            {
                onMovement = true;

                desiredDirection = new Vector3(inputs.MovementAxis.x, 0f, inputs.MovementAxis.y);

                //In case the player moves diagonally, it's normalized so that the speed is the same.
                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection = desiredDirection.normalized;
                }
            }

            desiredDirection.y -= 1 * Time.deltaTime;

            controller.Move(desiredDirection * speed * Time.deltaTime);
            Rotate(desiredDirection);

        }

        // If the axis has any sort of input on Joystick.
        else if (inputs.MovementAxisController.x > 0.2f || inputs.MovementAxisController.x < -0.2f || inputs.MovementAxisController.y > 0.2f || inputs.MovementAxisController.y < -0.2f)
        {
            onNoMovementTime = 0;


            if (controller.isGrounded)
            {
                onMovement = true;

                desiredDirection = new Vector3(inputs.MovementAxisController.x, 0f, inputs.MovementAxisController.y);

                //In case the player moves diagonally, it's normalized so that the speed is the same.
                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection = desiredDirection.normalized;
                }

            }
            desiredDirection.y -= 1 * Time.deltaTime;

            controller.Move(desiredDirection * speed * Time.deltaTime);

            LookDirection.LookAtWhileMoving(inputs.MovementAxisController.x, inputs.MovementAxisController.y);
            Rotate(desiredDirection);

        }

        else
        {
            desiredDirection = new Vector3(0, 0, 0); controller.Move(desiredDirection * speed);
        }

        if (onMovement)
        {
            if (inputs.MovementAxis.x == 0f && inputs.MovementAxis.y == 0f)
            {
                onMovement = false;
                OnStopping();
                onMovementTime = 0;
            }
        }
        else
        {
            onNoMovementTime += Time.deltaTime;
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


