using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirectionsAndOrder : MonoBehaviour
{
    #region Variables
    //Movement Related
    //Look variables
    //Vector3 that keeps track of the LookPositions

    private Vector3 mousePosition, direction, tpoint;
    public Vector3 miradaposition;
    float visibleCursorTimer = 10.0f, timeLeft;
    float cursorPosition;
    bool catchCursor = true;

    public float hrj, vrj;

    public float viewRadius, mouseRadius = 1;
    [Range(0, 360)]
    public float viewAngle;
    private float lookTimer;

    //Gameplay
    public Army commander;
    public Transform alternativeCenter;
    public NPC closestTarget, closestEnemyTarget, latestClosestTarget;
    public BU_Cabin closestBUTarget, latestclosestBUTarget;

    public LayerMask targetMask, obstacleMask;
    private int terrain = 1 << 8;
    private float orderCounter;
    private bool orderInOrder;
    private NPC boyInCharge;
    public GameObject orderPosition;
    private RaycastHit hit;

    public List<string> selectedTypeList;
    public int selectedTypeInt;

    public bool playingOnController;

    //NEW UI
    public GameObject UISelectionSpawned;
    [SerializeField]
    public UI_PointerDirection pointerDirection;
    UI_PointerSelection pointerSelection;
    GameObject pointerOrder;
    float alphaTarget = 0.111f;

    [SerializeField]
    bool controllerLookModel;

    //sound
    AudioSource reclute, order;
    #endregion

    // Use this for initialization
    void Awake()
    {
        commander = FindObjectOfType<Army>();
        reclute = GetComponent<AudioSource>();
        StartCoroutine("FindTargetsWithDelay", .05f);
        pointerOrder = transform.Find("OrderDirection").gameObject;
    }
    void Update()
    {
        // LOOK
        #region Inputs
        //RightJoystick
        //restart
        ControllerLookAxis();
        #endregion

        LookAt(hrj, vrj);
        if (Input.GetButtonDown("FreeMode")) { ControllerFreeMode(); }
    }

    private void ControllerFreeMode()
    {
        if (controllerLookModel)
        {
            controllerLookModel = false;
        }
        else
        {
            controllerLookModel = true;
        }
    }

    void ControllerLookAxis()
    {
        hrj = Input.GetAxis("HorizontalRightJoystick");
        vrj = Input.GetAxis("VerticalRightJoystick");
    }

    private void LateUpdate()
    {
        GUI();
    }
    #region GUI
    private void GUI()
    {
        //Mouse on top of things friendly unselected units
        #region SelectionUI
        GUI_SelectionUI();

        #endregion

        //UI for orders when unit selected.
        #region OrderUI
        //UI FOR CONTROLLER
        if (playingOnController)
        {
            // If controller in regular mode.
            if (!controllerLookModel)
            {
                GUI_RegularPointer();
            }
            // Pointer when precision mode is enabled.
            else
            {
                GUI_SpecialPointer();
            }
        }
        //UI FOR MOUSE
        else
        {
            GUI_MousePointer();
        }
        #endregion
    }
    private void GUI_SelectionUI()
    {
        if (closestTarget != null)
        {
            if (closestTarget != latestClosestTarget)
            {
                closestTarget.GUI_Activate();
                UISelectionSpawned = closestTarget.GUI;
            }

            latestClosestTarget = closestTarget;
        }
    }
    private void GUI_RegularPointer()
    {
        if (pointerDirection.enabled)
        {
            pointerDirection.transform.position = Vector3.Lerp(pointerDirection.transform.position,
                transform.position + (transform.forward * (viewRadius / 1.5f)), 0.4f);
        }
    }
    private void GUI_MousePointer()
    {
        if (pointerDirection.enabled)
        {
            pointerDirection.transform.position = miradaposition;
        }
    }
    private void GUI_SpecialPointer()
    {
        pointerDirection.transform.position = miradaposition;
    }
    #endregion
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
        if (playingOnController)
        {
            targetsOnController();
        }
        else
        {
            targetsOnMouse();
        }
    }
    private void targetsOnMouse()
    {
        closestTarget = null;
        closestBUTarget = null;
        closestEnemyTarget = null;

        //Each collider hitted by the Sphere in the TargetMask
        Collider[] targetsInViewRadius = Physics.OverlapSphere(miradaposition, 1, targetMask);
        foreach (Collider col in targetsInViewRadius)
        {
            // Save the col as an NPC
            NPC colNPC;
            NPC colEnemy;
            BU_Cabin colBU;

            if (col.gameObject != commander.gameObject)
            {
                // Checks if its the player or if its people.
                if (col.CompareTag("People"))
                {
                    colNPC = col.GetComponent<NPC>();

                    Transform target = col.transform;

                    //Distance to target
                    float dstToTarget = Vector3.Distance(miradaposition, target.position);

                    //Check if its following already.
                    if (colNPC.getState() != "Follow")
                    {
                        //If the closestTarget is null he is the closest target.
                        // If the distance is smaller than the distance to the closestTarget.
                        if (closestTarget == null || dstToTarget < Vector3.Distance(miradaposition, target.position))
                        {
                            closestTarget = colNPC;
                        }
                    }
                }

                else if (col.CompareTag("Interactible"))
                {
                    colBU = col.GetComponent<BU_Cabin>();

                    if (colBU != null)
                    {
                        if (closestBUTarget != null)
                        {
                            closestBUTarget.GUI_Disabled();
                        }
                        closestBUTarget = colBU;
                    }
                }

                else if (col.CompareTag("Enemy"))
                {
                    colEnemy = col.GetComponent<NPC>();
                    if (colEnemy != null)
                    {

                        Transform target = col.transform;

                        //Distance to target
                        float dstToTarget = Vector3.Distance(transform.position, target.position);

                        //If the closestTarget is null he is the closest target.
                        // If the distance is smaller than the distance to the closestTarget.
                        if (closestEnemyTarget == null || dstToTarget < Vector3.Distance(transform.position, closestEnemyTarget.transform.position))
                        {
                            closestEnemyTarget = colEnemy;
                        }
                    }
                }


            }

        }




        // if there is a building in the range, enemies wont be selected for order.
        if (closestEnemyTarget != null)
        {
            closestBUTarget = null;
        }
    }
    private void targetsOnController()
    {
        closestTarget = null;
        closestBUTarget = null;
        closestEnemyTarget = null;

        //Each collider hitted by the Sphere in the TargetMask
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        foreach (Collider col in targetsInViewRadius)
        {
            // Save the col as an NPC
            NPC colNPC;
            NPC colEnemy;
            BU_Cabin colBU;

            if (col.gameObject != commander.gameObject)
            {
                // Checks if its the player or if its people.
                if (col.CompareTag("People"))
                {
                    colNPC = col.GetComponent<NPC>();

                    Transform target = col.transform;

                    // Check if its inside the selection angle.
                    Vector3 dirToTarget = (target.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                    {
                        //Distance to target
                        float dstToTarget = Vector3.Distance(transform.position, target.position);

                        //Check if its following already.
                        if (colNPC.getState() != "Follow")
                        {
                            //If the closestTarget is null he is the closest target.
                            // If the distance is smaller than the distance to the closestTarget.
                            if (closestTarget == null || dstToTarget < Vector3.Distance(transform.position, closestTarget.transform.position))
                            {
                                closestTarget = colNPC;
                            }
                        }
                    }
                }

                else if (col.CompareTag("Interactible"))
                {
                    colBU = col.GetComponent<BU_Cabin>();
                    if (colBU != null)
                    {
                        Transform target = col.transform;

                        // Check if its inside the selection angle.
                        Vector3 dirToTarget = (target.position - transform.position).normalized;

                        if (closestBUTarget != null)
                        {
                            closestBUTarget.GUI_Disabled();
                        }
                        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                        {
                            closestBUTarget = colBU;
                        }
                    }
                }

                else if (col.CompareTag("Enemy"))
                {
                    colEnemy = col.GetComponent<NPC>();
                    if (colEnemy != null)
                    {

                        Transform target = col.transform;

                        // Check if its inside the selection angle.
                        Vector3 dirToTarget = (target.position - transform.position).normalized;

                        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                        {
                            //Distance to target
                            float dstToTarget = Vector3.Distance(transform.position, target.position);

                            //If the closestTarget is null he is the closest target.
                            // If the distance is smaller than the distance to the closestTarget.
                            if (closestEnemyTarget == null || dstToTarget < Vector3.Distance(transform.position, closestEnemyTarget.transform.position))
                            {
                                closestEnemyTarget = colEnemy;
                            }

                        }
                    }
                }
            }


        }

        // if there is a building in the range, enemies wont be selected for order.
        if (closestEnemyTarget != null)
        {
            closestBUTarget = null;
        }
    }
    #endregion
    void LookAt(float _hrj, float _vrj)
    {
        Cursor.visible = false;

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
                catchCursor = true;
            }

            if (!playingOnController)
            {
                // If the mouse is not moving, the cursor follows the camera movement.
                Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(cursorRay, out hit, Mathf.Infinity, terrain))
                {
                    //Player is not taken into account due to weird behaviours.
                    if (hit.transform.tag != "Player")
                    {
                        mousePosition = hit.point;
                    }

                    miradaposition = new Vector3(mousePosition.x, mousePosition.y + 0.5f, mousePosition.z);
                    transform.LookAt(miradaposition);

                }
            }

            if (_hrj != 0 || _vrj != 0)
            {
                Vector3 tdirection = new Vector3(_hrj, 0, _vrj);
                if (!controllerLookModel)
                {
                    tdirection = tdirection.normalized;
                }
                miradaposition = transform.position + (tdirection) * viewRadius / 2;
                transform.LookAt(miradaposition);

                playingOnController = true;
            }
        }

        else
        {
            playingOnController = false;
            timeLeft = visibleCursorTimer;
            //Mouse
            //Sends a ray to where the mouse is pointing at.

            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Saves the information of the hit.
            RaycastHit hit;
            if (Physics.Raycast(cursorRay, out hit, Mathf.Infinity, terrain))
            {
                //Player is not taken into account due to weird behaviours.
                if (hit.transform.tag != "Player")
                {
                    mousePosition = hit.point;
                }
            }

            miradaposition = new Vector3(mousePosition.x, mousePosition.y + 0.5f, mousePosition.z);
            transform.LookAt(miradaposition);
        }
        if (alternativeCenter == null)
        {
            transform.position = commander.transform.position;
        }
        else
        {
            transform.position = alternativeCenter.transform.position;
        }
    }
    public void LookAtWhileMoving(float _playerHrj, float _playerVrj)
    {
        playingOnController = true;

        if (hrj == 0 && vrj == 0)
        {
            lookTimer += Time.deltaTime;
            if (lookTimer > 1.5f)
            {
                Vector3 tdirection = new Vector3(_playerHrj, 0, _playerVrj);
                miradaposition = transform.position + (tdirection) * viewRadius / 2;
                transform.LookAt(miradaposition);
            }
        }
        else { lookTimer = 0; }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {

        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        // Gizmos.DrawSphere(miradaposition, 1);
        Gizmos.DrawSphere(orderPosition.transform.position, 1);
    }

    public void AlternativeCenter(Transform _alternative)
    {
        alternativeCenter = _alternative;
    }
}