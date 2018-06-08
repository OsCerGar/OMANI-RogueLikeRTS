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



    //NEW UI
    GameObject pointerDirection, pointerOrder, pointerSelection, headArm;
    Material materialpointerDirection, materialpointerOrder, materialpointerSelection;
    float alphaTarget = 0.111f;

    [SerializeField]
    bool modo;

    //sound
    AudioSource reclute, order;
    #endregion

    // Use this for initialization
    void Start()
    {
        commander = FindObjectOfType<Army>();
        reclute = this.GetComponent<AudioSource>();
        StartCoroutine("FindTargetsWithDelay", .05f);


        pointerDirection = this.transform.Find("PointerDirection").gameObject;
        materialpointerDirection = this.transform.Find("PointerDirection").GetComponent<MeshRenderer>().material;

        pointerOrder = this.transform.Find("OrderDirection").gameObject;
        materialpointerOrder = this.transform.Find("OrderDirection").GetComponent<MeshRenderer>().material;

        pointerSelection = this.transform.Find("SelectionDirection").gameObject;
        materialpointerSelection = this.transform.Find("SelectionDirection").GetComponent<MeshRenderer>().material;

        headArm = this.transform.Find("HeadArm").gameObject;

    }

    void Update()
    {
        // LOOK
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
        UI_Hologram();

    }

    private void UI_Hologram()
    {
        #region SelectionUI
        GUI_SelectionUI();

        #endregion

        #region OrderUI
        if (modo == false)
        {
            if (closestEnemyTarget == null && closestBUTarget == null)
            {
                GUI_RegularPointer();

            }

            else
            {
                if (selectedTypeList.Count > 0 && selectedTypeInt < selectedTypeList.Count)
                {
                    GUI_BUOrder();
                }

                else
                {
                    GUI_RegularPointer();
                    materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), 0, 0.6f));
                }
            }
        }

        else
        {
            if (closestEnemyTarget == null && closestBUTarget == null)
            {
                GUI_SpecialPointer();

            }

            else
            {
                if (selectedTypeList.Count > 0 && selectedTypeInt < selectedTypeList.Count)
                {
                    GUI_BUOrder();
                }

                else
                {
                    GUI_SpecialPointer();
                    materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), 0, 0.6f));
                }
            }
        }

    }

    private void GUI_SelectionUI()
    {
        if (closestTarget != null)
        {
            pointerSelection.transform.position = closestTarget.ui_information.transform.position;
            materialpointerSelection.SetFloat("_Alpha", Mathf.Lerp(materialpointerSelection.GetFloat("_Alpha"), alphaTarget, 0.4f));
        }

        else if (closestBUTarget != null && closestBUTarget.numberOfWorkers > 0)
        {
            pointerSelection.transform.position = closestBUTarget.ui_information.transform.position;
            materialpointerSelection.SetFloat("_Alpha", Mathf.Lerp(materialpointerSelection.GetFloat("_Alpha"), alphaTarget, 0.4f));
        }

        else
        {
            materialpointerSelection.SetFloat("_Alpha", Mathf.Lerp(materialpointerSelection.GetFloat("_Alpha"), 0, 0.4f));
        }
    }

    private void GUI_RegularPointer()
    {
        pointerDirection.transform.position = Vector3.Lerp(pointerDirection.transform.position, this.transform.position + (this.transform.forward * (viewRadius / 2)), 0.4f);
        headArm.transform.position = Vector3.Lerp(headArm.transform.position, new Vector3(commander.transform.position.x, 4, commander.transform.position.z) + (this.transform.forward * (viewRadius / 20)), 0.4f);

        headArm.transform.LookAt(this.transform.position + (this.transform.forward * (viewRadius / 2)));
        //point order material and position reset.
        materialpointerDirection.SetFloat("_Alpha", Mathf.Lerp(materialpointerDirection.GetFloat("_Alpha"), alphaTarget, 0.2f));
        materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), 0, 0.9f));

        //Doesnt return, for now.

        //pointerOrder.transform.position = this.transform.position;
    }
    private void GUI_SpecialPointer()
    {
        pointerDirection.transform.position = miradaposition;
        headArm.transform.position = Vector3.Lerp(headArm.transform.position, new Vector3(commander.transform.position.x, 4, commander.transform.position.z) + (this.transform.forward * (viewRadius / 20)), 0.4f);

        headArm.transform.LookAt(this.transform.position + (this.transform.forward * (viewRadius / 2)));
        //point order material and position reset.
        materialpointerDirection.SetFloat("_Alpha", Mathf.Lerp(materialpointerDirection.GetFloat("_Alpha"), alphaTarget, 0.2f));
        materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), 0, 0.9f));

        //Doesnt return, for now.

        //pointerOrder.transform.position = this.transform.position;
    }

    private void GUI_BUOrder()
    {
        if (closestEnemyTarget != null)
        {
            pointerDirection.transform.position = Vector3.Lerp(pointerDirection.transform.position, closestEnemyTarget.transform.position, 0.6f);

            headArm.transform.LookAt(closestEnemyTarget.transform);

            pointerOrder.transform.position = closestEnemyTarget.ui_information.transform.position;
            pointerOrder.transform.localScale = closestEnemyTarget.ui_information.transform.localScale;

            materialpointerDirection.SetFloat("_Alpha", Mathf.Lerp(materialpointerDirection.GetFloat("_Alpha"), 0, 0.3f));
            materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), alphaTarget, 0.4f));
        }

        //^OrderTarget
        //If building close and Worker selected.
        else if (closestBUTarget != null)
        {
            if (closestBUTarget.notOnlyWorkers == true || selectedTypeList[selectedTypeInt] == "Worker")
            {
                if (closestBUTarget.ui_information != null)
                {
                    pointerDirection.transform.position = Vector3.Lerp(pointerDirection.transform.position, closestBUTarget.transform.position, 0.4f);

                    headArm.transform.LookAt(closestBUTarget.transform);

                    pointerOrder.transform.position = closestBUTarget.ui_information.transform.position;
                    pointerOrder.transform.localScale = closestBUTarget.ui_information.transform.localScale;

                    materialpointerDirection.SetFloat("_Alpha", Mathf.Lerp(materialpointerDirection.GetFloat("_Alpha"), 0, 0.3f));
                    materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), alphaTarget, 0.4f));
                }
                else
                {
                    materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), 0, 0.6f));
                }
            }
            else
            {
                materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), 0, 0.6f));
            }
        }

        else
        {
            materialpointerOrder.SetFloat("_Alpha", Mathf.Lerp(materialpointerOrder.GetFloat("_Alpha"), 0, 0.6f));
        }
    }
    #endregion


    private void SelectedType()
    {
        if (selectedTypeList.Count > 0)
        {
            // When you have a selectable unit
            if (selectedTypeInt < selectedTypeList.Count)
            {
                commander.GUI_ActivateCircle(selectedTypeList[selectedTypeInt]);
            }
            else if (selectedTypeInt > selectedTypeList.Count - 1)
            {
                selectedTypeInt = selectedTypeList.Count - 1;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown("joystick button 7")) // forward
            {
                if (selectedTypeInt + 1 > selectedTypeList.Count - 1)
                {
                    selectedTypeInt = 0;
                }

                else
                {
                    selectedTypeInt += 1;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown("joystick button 6")) // backwards
            {

                if (selectedTypeInt - 1 < 0)
                {
                    selectedTypeInt = selectedTypeList.Count - 1;
                }

                else
                {
                    selectedTypeInt -= 1;
                }
            }
        }
    }

    private void Order()
    {
        #region Orders
        if (selectedTypeList.Count > 0)
        {
            //The boy will stop following you
            if (Input.GetKeyDown("joystick button 0") || Input.GetMouseButtonDown(2))
            {
                commander.Order(selectedTypeList[selectedTypeInt], this.transform.position);
            }

            #region Direct Order
            if (Input.GetKeyDown("joystick button 5") || Input.GetMouseButtonDown(1))
            {
                orderCounter = 0;
            }

            if (Input.GetKey("joystick button 5") || Input.GetMouseButton(1))
            {
                orderCounter += Time.deltaTime;

                if (orderCounter > 0.5f)
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
                            commander.Order(selectedTypeList[selectedTypeInt], pointerDirection.transform.position);
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
            }
            if (Input.GetKeyUp("joystick button 5") || Input.GetMouseButtonUp(1))
            {
                //Normal order
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
                            commander.Order(selectedTypeList[selectedTypeInt], pointerDirection.transform.position);
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

                orderInOrder = false;
            }

            #region SpecialOrder
            if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("q"))
            {
                commander.ArmyChargedOrder(selectedTypeList[selectedTypeInt]);
            }
            #endregion

            if (commander.ListSize(selectedTypeList[selectedTypeInt]) < 1)
            {
                selectedTypeList.Remove(selectedTypeList[selectedTypeInt]);
            }


            #endregion

        }

        else
        {
            commander.GUI_ListDisableCircle();
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
                reclute.Play();
            }

            else if (closestBUTarget != null)
            {
                closestBUTarget.RemoveWorker();
                reclute.Play();
            }
        }

        if (Input.GetKey("joystick button 4") || Input.GetMouseButton(0))
        {
            orderCounter += Time.deltaTime;

            if (orderCounter > 0.5f)
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
                    if (colBU != null)
                    {
                        Transform target = col.transform;

                        // Check if its inside the selection angle.
                        Vector3 dirToTarget = (target.position - transform.position).normalized;

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
                miradaposition = this.transform.position + (tdirection) * viewRadius/2;
                transform.LookAt(miradaposition);
            }
        }
        else
        {
            timeLeft = visibleCursorTimer;
            Cursor.visible = false;
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

            //un miradaposition = mousePosition;
            Vector3 centerPosition = this.transform.position; //center of *black circle*
            float distance = Vector3.Distance(mousePosition, centerPosition); //distance from ~green object~ to *black circle*

            if (distance > viewRadius) //If the distance is less than the radius, it is already within the circle.
            {
                Vector3 fromOriginToObject = mousePosition - centerPosition; //~GreenPosition~ - *BlackCenter*
                fromOriginToObject *= viewRadius / distance; //Multiply by radius //Divide by Distance
                miradaposition = centerPosition + fromOriginToObject; //*BlackCenter* + all that Math
            }
            else
            {
                miradaposition = mousePosition;
            }

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(miradaposition, 1);
    }
}