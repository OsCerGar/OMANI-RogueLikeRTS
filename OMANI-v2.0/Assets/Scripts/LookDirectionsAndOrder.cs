using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirectionsAndOrder : MonoBehaviour
{

    //Movement Related
    //Look variables
    //Vector3 that keeps track of the LookPositions
    private Vector3 mousePosition, direction, tpoint, miradaposition;
    float visibleCursorTimer = 10.0f, timeLeft;

    float cursorPosition;
    bool catchCursor = true;
    private Animator anim;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    Camera viewCamera;

    //Gameplay
    public Army commander;
    public GameObject closestTarget;
    public LayerMask targetMask;
    public string selectedType = "Swordsman";


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        viewCamera = Camera.main;
        commander = FindObjectOfType<Army>();

        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    void Update()
    {

        // LOOK

        #region Inputs
        //RightJoystick
        float hrj = Input.GetAxis("HorizontalRightJoystick");
        float vrj = Input.GetAxis("VerticalRightJoystick");
        #endregion
        #region before
        /*
        if (catchCursor)
        {
            catchCursor = false;
            cursorPosition = Input.GetAxis("Mouse X");
        }
        if (Input.GetAxis("Mouse X") == cursorPosition)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = visibleCursorTimer;
                Cursor.visible = false;
                catchCursor = true;
            }

            if (hrj != 0 || vrj != 0)
            {
                Vector3 tdirection = new Vector3(hrj, 0, vrj);
                miradaposition.y = this.transform.position.y;
                this.transform.position = this.transform.position + tdirection.normalized * 6;

            }
        }       
        else
        {
            timeLeft = visibleCursorTimer;
            Cursor.visible = true;

            #region LookInput
            //Mouse
            //Sends a ray to where the mouse is pointing at.

            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Saves the information of the hit.
            RaycastHit hit;
            if (Physics.Raycast(cursorRay, out hit))
            {
                //Player is not taken into account due to weird behaviours.
                if (hit.transform.tag != "Player")
                {
                    mousePosition = hit.point;

                    tpoint = (hit.point - transform.position).normalized * 6f;
                    tpoint.y = 0;
                    this.transform.position = this.transform.position + tpoint;
                }
            }

        }
#endregion*/
        #endregion

        LookAt(hrj, vrj);

        //En un futuro, R2/L2
        if (Input.GetKey("joystick button 5") || Input.GetMouseButtonDown(1))
        {
            commander.Order(selectedType, this.transform.TransformDirection(Vector3.forward).normalized * viewRadius);
        }

        if (Input.GetKey("joystick button 4") || Input.GetMouseButtonDown(0))
        {
            if (closestTarget != null) { 
            commander.Reclute(closestTarget.GetComponent<NPC>());
            }
        }
    }

    #region FindVisibleTargets
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {

        closestTarget = null;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        foreach (Collider col in targetsInViewRadius)
        {
            Transform target = col.transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                // This needs a fix
                if (col.gameObject.GetComponent<NPC>().AI_GetState() != "Follow") {

                    if (closestTarget == null || dstToTarget < Vector3.Distance(transform.position, closestTarget.transform.position))
                    {
                        closestTarget = col.gameObject;
                    }
                }

                /*if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) {

                }*/
            }
        }
    }
    #endregion

    void LookAt(float _hrj, float _vrj)
    {
        if (catchCursor)
        {
            catchCursor = false;
            cursorPosition = Input.GetAxis("Mouse X");
        }
        if (Input.GetAxis("Mouse X") == cursorPosition)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = visibleCursorTimer;
                Cursor.visible = false;
                catchCursor = true;
            }

            if (_hrj != 0 || _vrj != 0)
            {
                Vector3 tdirection = new Vector3(_hrj, 0, _vrj);
                transform.LookAt(tdirection + Vector3.up * transform.position.y);
            }
        }
        else
        {
            timeLeft = visibleCursorTimer;
            Cursor.visible = true;
            //Mouse
            //Sends a ray to where the mouse is pointing at.

            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Saves the information of the hit.
            RaycastHit hit;
            if (Physics.Raycast(cursorRay, out hit))
            {
                //Player is not taken into account due to weird behaviours.
                if (hit.transform.tag != "Player")
                {
                    mousePosition = hit.point;
                }
            }

            transform.LookAt(mousePosition + Vector3.up * transform.position.y);
        }

        this.transform.position = commander.transform.position;

    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {

        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


}