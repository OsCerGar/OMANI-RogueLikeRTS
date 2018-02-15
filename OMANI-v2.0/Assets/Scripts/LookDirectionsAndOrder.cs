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
    public GameObject closestTarget;
    public LayerMask targetMask, obstacleMask;
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

    private void SelectedType()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            selectedTypeInt += 1;
            if (selectedTypeInt > selectedTypeList.Count - 1)
            {
                selectedTypeInt = 0;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            selectedTypeInt -= 1;

            if (selectedTypeInt < 0)
            {
                selectedTypeInt = selectedTypeList.Count - 1;
            }

        }

    }

    private void Order()
    {

        #region Order
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
                Debug.DrawRay(transform.position, this.transform.forward * viewRadius, Color.yellow, 5f);

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

        #endregion

        #region Reclute
        if (Input.GetKeyDown("joystick button 4") || Input.GetMouseButtonDown(0))
        {
            if (closestTarget != null)
            {
                NPC closestTargetNPC = closestTarget.GetComponent<NPC>();
                if (commander.ListSize(closestTargetNPC.boyType) < 1)
                {
                    selectedTypeList.Add(closestTargetNPC.boyType);
                }

                commander.Reclute(closestTargetNPC);
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

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        foreach (Collider col in targetsInViewRadius)
        {
            NPC colNPC;
            if (colNPC = col.gameObject.GetComponent<NPC>())
            {
                Transform target = col.transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    // This needs a fix
                    if (colNPC.AI_GetState() != "Follow")
                    {

                        if (closestTarget == null || dstToTarget < Vector3.Distance(transform.position, closestTarget.transform.position))
                        {
                            closestTarget = col.gameObject;
                        }
                    }

                }
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
                miradaposition = this.transform.position + (tdirection + Vector3.up * transform.position.y);
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
            miradaposition = mousePosition + Vector3.up * transform.position.y;
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
                miradaposition = this.transform.position + (tdirection + Vector3.up * transform.position.y);
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
            miradaposition = mousePosition + Vector3.up * transform.position.y;
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