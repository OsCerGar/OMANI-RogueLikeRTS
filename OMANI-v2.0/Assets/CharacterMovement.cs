using UnityEngine;
using UnityEngine.Experimental.Input;

public class CharacterMovement : MonoBehaviour
{
    public OMANINPUT controls;

    //Objects
    Vector2 movementAxis, movementAxisController;

    Rigidbody rb;
    LookDirectionsAndOrder LookDirection;
    [SerializeField]
    public float speed = 1, smooth = 5f;
    [SerializeField]
    private float minDistanceToGround, maxDistanceToGround;
    CharacterController controller;

    Vector3 desiredDirection;

    //audio
    AudioSource _sand;
    bool onMovement = false;
    public float onMovementTime;

    //Sound events

    public delegate void Stopped();
    public static event Stopped OnStopping;


    private void Awake()
    {

        controls.PLAYER.WASD.performed += movement => MoveAxis(movement.ReadValue<Vector2>());
        controls.PLAYER.WASD.cancelled += movement => RestartAxis();
        //controls.PLAYER.Joystick.performed += Controllermovement => ControllerMoveAxis(Controllermovement.ReadValue<Vector2>());
        controls.PLAYER.Joystick.performed += ControllerMoveAxis;
        controls.PLAYER.Joystick.cancelled += movement => RestartControllerAxis();

    }
    private void OnEnable()
    {
        controls.PLAYER.WASD.Enable();
        controls.PLAYER.Joystick.Enable();

    }

    private void OnDisable()
    {
        controls.PLAYER.WASD.Disable();
        controls.PLAYER.Joystick.Disable();
    }

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        LookDirection = FindObjectOfType<LookDirectionsAndOrder>();
    }

    private void FixedUpdate()
    {

        //This function controls the movement.
        MovementController();

    }

    void MoveAxis(Vector2 _axis)
    {
        movementAxis = _axis;
    }
    void RestartAxis()
    {
        movementAxis = new Vector2(0, 0);
    }
    void ControllerMoveAxis(InputAction.CallbackContext context)
    {
        movementAxisController = context.ReadValue<Vector2>();
    }
    void RestartControllerAxis()
    {
        movementAxisController = new Vector2(0, 0);
    }

    //Function in charge of the Main Character movement. Sends commands to the animator and allows the character to rotate.
    void MovementController()
    {
        //MOVEMENT IF NOT ROLLIN
        // If the axis has any sort of input on WASD.
        if (movementAxis.x != 0f || movementAxis.y != 0f)
        {
            if (controller.isGrounded)
            {
                onMovement = true;

                desiredDirection = new Vector3(movementAxis.x, 0f, movementAxis.y);

                //In case the player moves diagonally, it's normalized so that the speed is the same.
                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection = desiredDirection.normalized;
                }
            }

            desiredDirection.y -= 2 * Time.deltaTime;

            controller.Move(desiredDirection * speed);
            Rotate(desiredDirection);

        }

        // If the axis has any sort of input on Joystick.
        else if (movementAxisController.x != 0f || movementAxisController.y != 0f)
        {
            if (controller.isGrounded)
            {
                onMovement = true;

                desiredDirection = new Vector3(movementAxisController.x, 0f, movementAxisController.y);

                //In case the player moves diagonally, it's normalized so that the speed is the same.
                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection = desiredDirection.normalized;
                }

            }
            desiredDirection.y -= 1 * Time.deltaTime;

            controller.Move(desiredDirection * speed);

            LookDirection.LookAtWhileMoving(movementAxisController.x, movementAxisController.y);
            Rotate(desiredDirection);

        }

        else
        {
            desiredDirection = new Vector3(0, 0, 0); controller.Move(desiredDirection * speed);
        }

        if (onMovement)
        {
            onMovementTime += Time.deltaTime;
            if (movementAxis.x == 0f && movementAxis.y == 0f)
            {
                onMovement = false;
                OnStopping();
                onMovementTime = 0;
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


