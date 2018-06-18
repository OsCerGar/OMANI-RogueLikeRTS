using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //Objects
    Rigidbody rb, childRb;
    HingeJoint childHj;
    LookDirectionsAndOrder LookDirection;
    [SerializeField]
    public float speed = 1, smooth = 5f;
    bool onMovement = false;

    Vector3 desiredDirection;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        childRb = this.transform.Find("Throne").GetComponent<Rigidbody>();
        childHj = this.transform.Find("Throne").GetComponent<HingeJoint>();
        LookDirection = FindObjectOfType<LookDirectionsAndOrder>();

    }

    private void FixedUpdate()
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

    //Function in charge of the Main Character movement. Sends commands to the animator and allows the character to rotate.
    void MovementController(float horizontal, float vertical, float horizontalJoystick, float verticalJoystick)
    {
        //MOVEMENT IF NOT ROLLIN
        // If the axis has any sort of input on WASD.
        if (horizontal != 0f || vertical != 0f)
        {
            onMovement = true;

            desiredDirection = new Vector3(horizontal, 0f, vertical);

            //In case the player moves diagonally, it's normalized so that the speed is the same.
            if (desiredDirection.magnitude > 1)
            {
                desiredDirection = desiredDirection.normalized;
            }

            rb.MovePosition(this.transform.position + desiredDirection * Time.deltaTime * speed);
            Rotate(desiredDirection);
        }
        // If the axis has any sort of input on Joystick.
        else if (horizontalJoystick != 0f || verticalJoystick != 0f)
        {
            onMovement = true;

            desiredDirection = new Vector3(horizontalJoystick, 0f, verticalJoystick);

            //In case the player moves diagonally, it's normalized so that the speed is the same.
            if (desiredDirection.magnitude > 1)
            {
                desiredDirection = desiredDirection.normalized;
            }

            rb.MovePosition(this.transform.position + desiredDirection * Time.deltaTime * speed);
            Rotate(desiredDirection);

            LookDirection.LookAtWhileMoving(horizontalJoystick, verticalJoystick);

        }
        // If the axis doesn't have any sort of input.
        else
        {
            /*
            if (onMovement)
            {
                childHj.useSpring = false;
                childRb.AddForce(desiredDirection * 15f, ForceMode.Impulse);
                onMovement = false;
            }
            else
            {
                childHj.useSpring = true;
                //childRb.MovePosition(this.transform.position * Time.deltaTime * 0.1f);
            }
            */
        }

    }

    // Function that makes the rotation of the character look good.
    /* This rotate function is called upon each frame. The rotation is smoothed.*/
    void Rotate(Vector3 desiredDirection)
    {

        desiredDirection = Camera.main.transform.TransformDirection(desiredDirection);
        desiredDirection.y = 0.0f;

        // Calculates the rotation at which the character is directed.
        Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);

        // Smoothes the transition
        Quaternion smoothedRotation = Quaternion.Lerp(rb.rotation, desiredRotation, smooth * Time.deltaTime);

        // Uses the rigidbody function  "MoveRotation" which sets the new rotation of the Rigidbody. 
        rb.MoveRotation(smoothedRotation);
    }


}
