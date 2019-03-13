using UnityEngine;

public class RadialMenu_GUI : MonoBehaviour
{
    Army army;
    LookDirectionsAndOrder lookD;
    // Radial Menu Stuff
    RadialMenu_GUI_BASE[] radialPart = new RadialMenu_GUI_BASE[4];
    Canvas radialCanvas, backgroundCanvas, robotsCanvas;
    private Vector2 Mouseposition;
    public Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    public Vector2 centercircle = new Vector2(0.5f, 0.5f);
    public Vector2 toVector2M;
    private int menuItems;
    private int curMenuItem;
    private int oldMenuItem;

    bool enabled;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        radialPart[0] = transform.GetChild(0).Find("Base").GetComponent<RadialMenu_GUI_BASE>();
        radialPart[1] = transform.GetChild(0).Find("2Base").GetComponent<RadialMenu_GUI_BASE>();
        radialPart[2] = transform.GetChild(0).Find("3Base").GetComponent<RadialMenu_GUI_BASE>();
        radialPart[3] = transform.GetChild(0).Find("4Base").GetComponent<RadialMenu_GUI_BASE>();
        army = FindObjectOfType<Army>();
        lookD = FindObjectOfType<LookDirectionsAndOrder>();
        radialCanvas = transform.GetChild(0).GetComponent<Canvas>();
        backgroundCanvas = transform.GetChild(1).GetComponent<Canvas>();
        robotsCanvas = transform.GetChild(2).GetComponent<Canvas>();
        menuItems = radialPart.Length;
    }

    private void Update()
    {
        if (enabled)
        {
            GetCurrentMenuItem();
        }
    }
    public void PopUp()
    {
        radialCanvas.enabled = true;
        backgroundCanvas.enabled = true;
        robotsCanvas.enabled = true;
        enabled = true;
    }

    public int PopDown()
    {
        GetCurrentMenuItem();
        radialCanvas.enabled = false;
        backgroundCanvas.enabled = false;
        robotsCanvas.enabled = false;
        enabled = false;
        return curMenuItem;
    }
    public void GetCurrentMenuItem()
    {
        float angle = 0;

        if (lookD.hrj != 0 || lookD.vrj != 0)
        {
            angle = Mathf.Atan2(-lookD.vrj, lookD.hrj);
            angle = angle * Mathf.Rad2Deg;
        }
        else
        {
            Mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            toVector2M = new Vector2(Mouseposition.x / Screen.width, Mouseposition.y / Screen.height);
            angle = Mathf.Atan2(fromVector2M.y - centercircle.y, fromVector2M.x - centercircle.x) - Mathf.Atan2(toVector2M.y - centercircle.y, toVector2M.x - centercircle.x) * Mathf.Rad2Deg;
        }

        if (angle < 0) { angle += 360; }

        curMenuItem = (int)(angle / (360 / menuItems));

        if (curMenuItem != oldMenuItem)
        {
            CurrentButtonVisualFeedback();
        }

    }

    public int RadialMenuButtonAction()
    {
        //ButtonPressed Feedback
        return curMenuItem;
    }

    public void UpdateState()
    {
        for (int i = 0; i < radialPart.Length; i++)
        {
            radialPart[i].UISetRobot(army.getCells()[i].getRobotType()); // sets robot type
            radialPart[i].UISetAmountOfRobots(army.getCells()[i].getRobotQuantity());
        }
    }

    private void CurrentButtonVisualFeedback()
    {
        radialPart[curMenuItem].VisualFeedBack();
        radialPart[oldMenuItem].noVisualFeedBack();
        oldMenuItem = curMenuItem;
    }
}
