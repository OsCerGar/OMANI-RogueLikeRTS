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
    float visibleCursorTimer = 10.0f, timeLeft = 1;
    float cursorPosition;
    bool catchCursor = true;

    float hrj, vrj;

    public float viewRadius, mouseRadius = 1;
    [Range(0, 360)]
    public float viewAngle;

    //Gameplay
    public Army commander;
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
    UI_PointerDirection pointerDirection;
    UI_PointerSelection pointerSelection;
    GameObject pointerOrder, headArm;
    float alphaTarget = 0.111f;

    [SerializeField]
    bool controllerLookModel;

    //sound
    AudioSource reclute, order;
    #endregion

    // Use this for initialization
    void Start()
    {
        commander = FindObjectOfType<Army>();
        reclute = this.GetComponent<AudioSource>();
        StartCoroutine("FindTargetsWithDelay", .05f);


        pointerDirection = this.transform.Find("PointerDirection").GetComponent<UI_PointerDirection>();

        pointerOrder = this.transform.Find("OrderDirection").gameObject;
        headArm = this.transform.Find("HeadArm").gameObject;
    }
    void Update()
    {
        // LOOK
        #region Inputs
        //RightJoystick
        //restart
        hrj = 0;
        vrj = 0;

        hrj = Input.GetAxis("HorizontalRightJoystick");
        vrj = Input.GetAxis("VerticalRightJoystick");
        #endregion

        LookAt(hrj, vrj);

        // Look At Mode
        if (Input.GetKeyDown("joystick button 11"))
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

        SelectedType();
        Order();
        UI_Hologram();
    }
    private void LateUpdate()
    {
    }
    #region GUI
    private void UI_Hologram()
    {
        #region SelectionUI
        GUI_SelectionUI();

        #endregion

        #region OrderUI
        //UI FOR CONTROLLER
        if (playingOnController)
        {
            if (controllerLookModel == false)
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
                    }
                }
            }
            else
            {
                GUI_SpecialPointer();
            }
        }
        //UI FOR MOUSE
        else
        {
            GUI_MousePointer();

            if (selectedTypeList.Count > 0 && selectedTypeInt < selectedTypeList.Count)
            {
                GUI_BUMouseOrder();
            }

        }
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

        else
        {
            //pointerSelection.NotOnTop();
        }
    }
    private void GUI_RegularPointer()
    {
        pointerDirection.transform.position = Vector3.Lerp(pointerDirection.transform.position, this.transform.position + (this.transform.forward * (viewRadius / 2)), 0.4f);
        //pointerSelection.gameObject.SetActive(true);
        pointerDirection.gameObject.SetActive(true);

        headArm.transform.position = Vector3.Lerp(headArm.transform.position, new Vector3(commander.transform.position.x, 4, commander.transform.position.z) + (this.transform.forward * (viewRadius / 20)), 0.4f);

        headArm.transform.LookAt(this.transform.position + (this.transform.forward * (viewRadius / 2)));
        pointerOrder.SetActive(false);
    }
    private void GUI_MousePointer()
    {
        pointerDirection.transform.position = miradaposition;

        //pointerSelection.gameObject.SetActive(true);
        pointerDirection.gameObject.SetActive(true);

        headArm.transform.position = Vector3.Lerp(headArm.transform.position, new Vector3(commander.transform.position.x, 4, commander.transform.position.z) + (this.transform.forward * (viewRadius / 20)), 0.4f);

        headArm.transform.LookAt(this.transform.position + (this.transform.forward * (viewRadius / 2)));

        if (selectedTypeList.Count > 0 && selectedTypeInt < selectedTypeList.Count)
        {
            if (closestEnemyTarget != null)
            {
                headArm.transform.LookAt(closestEnemyTarget.transform);

                pointerOrder.transform.position = closestEnemyTarget.transform.position;

                pointerOrder.transform.localScale = closestEnemyTarget.ui_information.transform.localScale;
                //pointerSelection.gameObject.SetActive(false);
                pointerOrder.SetActive(true);
            }
            else
            {
                pointerOrder.SetActive(false);
            }
        }
        else
        {
            pointerOrder.SetActive(false);
        }

    }
    private void GUI_SpecialPointer()
    {
        pointerDirection.transform.position = miradaposition;
        headArm.transform.position = Vector3.Lerp(headArm.transform.position, new Vector3(commander.transform.position.x, 4, commander.transform.position.z) + (this.transform.forward * (viewRadius / 20)), 0.4f);

        headArm.transform.LookAt(this.transform.position + (this.transform.forward * (viewRadius / 2)));
    }
    private void GUI_BUOrder()
    {
        if (closestEnemyTarget != null)
        {
            pointerDirection.transform.position = Vector3.Lerp(pointerDirection.transform.position, closestEnemyTarget.transform.position, 0.6f);

            headArm.transform.LookAt(closestEnemyTarget.transform);

            pointerOrder.transform.position = closestEnemyTarget.transform.position;

            pointerOrder.transform.localScale = closestEnemyTarget.ui_information.transform.localScale;
            pointerSelection.gameObject.SetActive(false);
            pointerDirection.gameObject.SetActive(false);
            pointerOrder.SetActive(true);
        }

        //^OrderTarget
        //If building close and Worker selected.
        else if (closestBUTarget != null)
        {
            if (closestBUTarget.workerInside == false || selectedTypeList[selectedTypeInt] == "Worker")
            {
                // UI ENABLED
                closestBUTarget.GUI_Enabled();
                latestclosestBUTarget = closestBUTarget;
            }
        }
        else
        {
            if (latestclosestBUTarget != null)
            {
                latestclosestBUTarget.GUI_Disabled();
            }
        }
    }
    private void GUI_BUMouseOrder()
    {
        if (closestEnemyTarget != null)
        {
            pointerOrder.transform.position = closestEnemyTarget.transform.position;
            pointerOrder.transform.localScale = closestEnemyTarget.ui_information.transform.localScale;
            pointerOrder.SetActive(true);
        }

        //^OrderTarget
        //If building close and Worker selected.
        else if (closestBUTarget != null)
        {
            if (closestBUTarget.workerInside == false || selectedTypeList[selectedTypeInt] == "Worker")
            {
                // UI ENABLED
                closestBUTarget.GUI_Enabled();
                latestclosestBUTarget = closestBUTarget;
            }
        }
        else
        {
            if (latestclosestBUTarget != null)
            {
                latestclosestBUTarget.GUI_Disabled();
            }
        }
    }

    #endregion
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
        //Animation start
        if (Input.GetKeyDown("joystick button 5") || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            pointerDirection.Click();
        }

        if (selectedTypeList.Count > 0)
        {
            //The boy will stop following you
            if (Input.GetKeyDown("joystick button 0") || Input.GetMouseButtonDown(2))
            {
                commander.Order(selectedTypeList[selectedTypeInt], this.transform.position);
            }

            #region Direct Order
            if (controllerLookModel == false)
            {
                if (Input.GetKeyDown("joystick button 5") || Input.GetMouseButtonDown(1))
                {
                    orderCounter = 0;
                }
                if (Input.GetKey("joystick button 5") || Input.GetMouseButton(1))
                {
                    orderCounter += Time.deltaTime;

                    if (orderCounter > 0.75f)
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
            }

            else
            {
                if (Input.GetKeyDown("joystick button 5") || Input.GetMouseButtonDown(1))
                {
                    orderCounter = 0;
                }
                if (Input.GetKey("joystick button 5") || Input.GetMouseButton(1))
                {
                    orderCounter += Time.deltaTime;

                    if (orderCounter > 0.75f)
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
                }
                if (Input.GetKeyUp("joystick button 5") || Input.GetMouseButtonUp(1))
                {
                    //Normal order
                    if (orderCounter < 0.2f)
                    {
                        pointerDirection.Click();

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
        #endregion
        #region Reclute
        if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            if (Input.GetKeyDown("joystick button 4") || Input.GetMouseButtonDown(0))
            {
                if (closestTarget != null)
                {

                    if (commander.ListSize(closestTarget.boyType) < 1)
                    {
                        selectedTypeList.Add(closestTarget.boyType);
                    }

                    commander.Reclute(closestTarget);
                    //pointerSelection.Selected();
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
                    if (colNPC.AI_GetState() != "Follow")
                    {
                        //If the closestTarget is null he is the closest target.
                        // If the distance is smaller than the distance to the closestTarget.
                        if (closestTarget == null || dstToTarget < Vector3.Distance(miradaposition, target.position))

                            closestTarget = colNPC;
                    }
                }

                else if (col.CompareTag("Interactible"))
                {
                    colBU = col.GetComponent<BU_Cabin>();

                    if (colBU != null)
                    {
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

                else if (col.CompareTag("Interactible"))
                {
                    colBU = col.GetComponent<BU_Cabin>();
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
                    //transform.LookAt(new Vector3(miradaposition.x, miradaposition.y, miradaposition.z));
                }
            }

            if (_hrj != 0 || _vrj != 0)
            {
                Vector3 tdirection = new Vector3(_hrj, 0, _vrj);
                miradaposition = this.transform.position + (tdirection) * viewRadius / 2;
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
            transform.LookAt(new Vector3(miradaposition.x, miradaposition.y, miradaposition.z));
        }

        this.transform.position = commander.transform.position;

    }
    public void LookAtWhileMoving(float _playerHrj, float _playerVrj)
    {
        playingOnController = true;

        if (hrj == 0 && vrj == 0)
        {
            Vector3 tdirection = new Vector3(_playerHrj, 0, _playerVrj);
            miradaposition = this.transform.position + (tdirection) * viewRadius / 2;
            transform.LookAt(miradaposition);
        }
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