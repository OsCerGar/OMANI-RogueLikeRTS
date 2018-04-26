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
    private GameObject miradaPositionObject;
    float visibleCursorTimer = 10.0f, timeLeft;
    float cursorPosition;
    bool catchCursor = true;



    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    //Gameplay
    public Army commander;
    public NPC closestTarget, closestEnemyTarget;
    public BU closestBUTarget;

    public LayerMask targetMask, obstacleMask;
    private int terrain = 1 << 8;
    private float orderCounter;
    private bool orderInOrder;
    private NPC boyInCharge;
    public GameObject orderPosition;
    private RaycastHit hit;

    public List<string> selectedTypeList;
    public int selectedTypeInt;
    #endregion

    // Use this for initialization
    void Start()
    {
        commander = FindObjectOfType<Army>();
        miradaPositionObject = new GameObject();

        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    void Update()
    {

        // LOOK
        miradaPositionObject.transform.position = miradaposition;

        #region Inputs
        //RightJoystick
        float hrj = Input.GetAxis("HorizontalRightJoystick");
        float vrj = Input.GetAxis("VerticalRightJoystick");
        #endregion

        if (!orderInOrder)
        {
            LookAt(hrj, vrj);
        }
        else
        {
            LookAtFromTargetPoint(hrj, vrj, boyInCharge.gameObject);
        }

        SelectedType();
        Order();
    }

    private void LateUpdate()
    {
        //Show an outline of the closest boy in town.

        if (closestTarget != null)
        {
            closestTarget.EnableCircle();
        }

        else if (closestBUTarget != null && closestBUTarget.numberOfWorkers > 0)
        {
            closestBUTarget.EnableWhiteCircle();
        }


        //^OrderTarget
        if (selectedTypeList.Count > 0)
        {
            if (closestBUTarget != null)
            {
                closestBUTarget.EnableCircle();

            }

            if (closestEnemyTarget != null)
            {
                closestEnemyTarget.EnableCircle();
            }
        }
    }

    private void SelectedType()
    {

        // When you have a selectable unity
        if (selectedTypeInt < selectedTypeList.Count)
        {
            if (selectedTypeList[selectedTypeInt] != null)
            {
                commander.GUI_ActivateCircle(selectedTypeList[selectedTypeInt]);

            }
        }

        if (selectedTypeList.Count > 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown("joystick button 7")) // forward
            {
                selectedTypeInt += 1;
                if (selectedTypeInt > selectedTypeList.Count - 1)
                {
                    selectedTypeInt = 0;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown("joystick button 6")) // backwards
            {
                selectedTypeInt -= 1;

                if (selectedTypeInt < 0)
                {
                    selectedTypeInt = selectedTypeList.Count - 1;
                }

            }
        }
    }

    private void Order()
    {
        #region Order
        if (selectedTypeList.Count > 0)
        {
            //The boy will stop following you
            if (Input.GetKeyDown("joystick button 0") || Input.GetMouseButtonDown(2))
            {
                commander.Order(selectedTypeList[selectedTypeInt], this.transform.position);

            }

            if (Input.GetKeyDown("joystick button 5") || Input.GetMouseButtonDown(1))
            {
                orderCounter = 0;
            }

            if (Input.GetKey("joystick button 5") || Input.GetMouseButton(1))
            {
                orderCounter += Time.deltaTime;

                if (orderCounter > 0.2f)
                {
                    boyInCharge = commander.GetBoyArmy(selectedTypeList[selectedTypeInt]);
                    boyInCharge.ChargedOrder(miradaPositionObject);
                    orderInOrder = true;

                }
            }
            if (Input.GetKeyUp("joystick button 5") || Input.GetMouseButtonUp(1))
            {
                if (orderCounter < 0.2f)
                {
                    //Checks if there is an objective for the order, if not, it goes to a place.
                    if (closestBUTarget == null && closestEnemyTarget == null)
                    {
                        Debug.DrawRay(transform.position, this.transform.forward * viewRadius, Color.yellow, 5f);

                        // You can't order through walls.
                        if (Physics.Raycast(transform.position, this.transform.forward, out hit, viewRadius, obstacleMask))
                        {
                            commander.Order(selectedTypeList[selectedTypeInt], hit.point);
                        }
                        else
                        {
                            commander.Order(selectedTypeList[selectedTypeInt], this.transform.position + (this.transform.forward * viewRadius));
                        }
                    }
                    else
                    {
                        if (closestBUTarget != null)
                        {
                            commander.OrderDirect(selectedTypeList[selectedTypeInt], closestBUTarget.direction);
                        }
                        else if (closestEnemyTarget != null)
                        {
                            commander.OrderDirect(selectedTypeList[selectedTypeInt], closestEnemyTarget.gameObject);
                        }
                    }

                }
                else
                {
                    GameObject orderPositionVar = Instantiate(orderPosition);

                    Debug.DrawRay(transform.position, this.transform.forward * Mathf.Clamp(orderCounter * 5, 0.2f, 20f), Color.yellow, 5f);

                    if (Physics.Raycast(transform.position, this.transform.forward, out hit, Mathf.Clamp(orderCounter * 5, 0.2f, 20f), obstacleMask))
                    {
                        orderPositionVar.transform.position = hit.point;
                    }
                    else
                    {
                        //Encargado de saber la distancia en la que se lanzará la orden cargada.
                        orderPositionVar.transform.position = this.transform.position + (this.transform.forward * Mathf.Clamp(orderCounter * 5, 0.2f, 20f));
                    }


                    orderPositionVar.GetComponent<OrderPositionObject>().NPC = boyInCharge.gameObject;
                    boyInCharge.ChargedOrderFullfilled(orderPositionVar);
                    commander.RemoveFromList(boyInCharge);
                    boyInCharge = null;

                }

                orderInOrder = false;

                if (commander.ListSize(selectedTypeList[selectedTypeInt]) < 1)
                {
                    selectedTypeList.Remove(selectedTypeList[selectedTypeInt]);
                }

            }
        }

        #endregion

        #region Reclute

        if (Input.GetKeyDown("joystick button 4") || Input.GetMouseButtonDown(0))
        {
            if (closestTarget != null)
            {

                if (commander.ListSize(closestTarget.boyType) < 1)
                {
                    selectedTypeList.Add(closestTarget.boyType);
                }

                commander.Reclute(closestTarget);
            }

            else if (closestBUTarget != null)
            {
                closestBUTarget.RemoveWorker();
            }
        }
        #endregion
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
        if (closestTarget != null)
        {
            closestTarget.DisableCircle();
        }

        if (closestBUTarget != null)
        {
            closestBUTarget.DisableCircle();
        }

        if (closestEnemyTarget != null)
        {
            closestEnemyTarget.DisableCircle();
        }

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
            BU colBU;

            if (col.gameObject != commander.gameObject)
            {
                // Checks if its the player or if its people.
                if (col.CompareTag("People"))
                {
                    colNPC = col.GetComponent<NPC>();

                    // Disables Outline by default
                    colNPC.DisableCircle();

                    Transform target = col.transform;

                    // Check if its inside the selection angle.
                    Vector3 dirToTarget = (target.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                    {
                        //Distance to target
                        float dstToTarget = Vector3.Distance(transform.position, target.position);

                        //Check if its following already.
                        if (colNPC.AI_GetState() != "Follow")
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

                else if (col.CompareTag("Building"))
                {
                    colBU = col.GetComponent<BU>();

                    // Disables Outline by default
                    colBU.DisableCircle();
                    Transform target = col.transform;

                    // Check if its inside the selection angle.
                    Vector3 dirToTarget = (target.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                    {
                        closestBUTarget = colBU;
                    }
                }

                else if (col.CompareTag("Enemy"))
                {
                    colEnemy = col.GetComponent<NPC>();
                    // Disables Outline by default
                    colEnemy.DisableCircle();

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

        // if there is a building in the range, enemies wont be selected for order.
        if (closestEnemyTarget != null)
        {
            closestBUTarget = null;
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
                miradaposition = this.transform.position + (tdirection);
                transform.LookAt(miradaposition);
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
            if (Physics.Raycast(cursorRay, out hit, Mathf.Infinity, terrain))
            {
                //Player is not taken into account due to weird behaviours.
                if (hit.transform.tag != "Player")
                {
                    mousePosition = hit.point;
                }
            }

            miradaposition = mousePosition;

            transform.LookAt(miradaposition);
        }

        this.transform.position = commander.transform.position;

    }
    void LookAtFromTargetPoint(float _hrj, float _vrj, GameObject _TargetPoint)
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
                miradaposition = this.transform.position + (tdirection);
                transform.LookAt(miradaposition);
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
            miradaposition = mousePosition;
            transform.LookAt(miradaposition);
        }

        this.transform.position = _TargetPoint.transform.position;

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