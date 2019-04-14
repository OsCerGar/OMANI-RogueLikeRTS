using UnityEngine;

public class RadialMenu_GUI : MonoBehaviour
{
    Army army;
    PlayerInputInterface player;
    // Radial Menu Stuff
    RadialMenu_GUI_BASE[] radialPart = new RadialMenu_GUI_BASE[4];
    RadialMenu_Quickslot quickSlot;
    LookDirectionsAndOrder lookDAO;

    Canvas radialCanvas, backgroundCanvas, robotsCanvas, quickSlotCanvas;
    private Vector2 Mouseposition;
    public Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    public Vector2 centercircle = new Vector2(0.5f, 0.5f);
    public Vector2 toVector2M;
    private int menuItems;
    private int curMenuItem = 0;
    private int oldMenuItem;

    bool enabled;

    public Vector2 DPAD;
    float oldMouse;
    bool onMouse = false;


    [SerializeField]
    AudioSource openMenuAudio, closeMenuAudio, selectedAudio, selectedAudioEmpty;

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
        player = FindObjectOfType<PlayerInputInterface>();
        lookDAO = FindObjectOfType<LookDirectionsAndOrder>();

        radialCanvas = transform.Find("Canvas").GetComponent<Canvas>();
        backgroundCanvas = transform.Find("CanvasBackground").GetComponent<Canvas>();
        robotsCanvas = transform.Find("CanvasRobots").GetComponent<Canvas>();
        quickSlotCanvas = transform.Find("CanvasQuickslot").GetComponent<Canvas>();
        quickSlot = transform.Find("CanvasQuickslot").GetComponent<RadialMenu_Quickslot>();
        menuItems = radialPart.Length;
    }

    private void Update()
    {
        if (enabled)
        {
            GetCurrentMenuItem();
        }
        if (army.currentFighter != null)
        {
            quickSlot.background.sprite = quickSlot.backgroundSelected;
        }
        else
        {
            quickSlot.background.sprite = quickSlot.backgroundNormal;
        }

    }

    public void menuItem(int i)
    {
        if (oldMenuItem != curMenuItem)
        {
            if (radialPart[curMenuItem].GetRobotType() != null)
            {
                selectedAudio.Play();
            }
            else { selectedAudioEmpty.Play(); }
        }
        oldMenuItem = curMenuItem;
        curMenuItem = i;
        UpdateState();
        army.radialMenuPopDown(curMenuItem);
    }

    public void PopUp()
    {
        radialCanvas.enabled = true;
        backgroundCanvas.enabled = true;
        robotsCanvas.enabled = true;
        quickSlotCanvas.enabled = false;
        enabled = true;
        GetCurrentMenuItem();
        UpdateState();
        openMenuAudio.Play();
    }

    public int PopDown()
    {
        GetCurrentMenuItem();
        UpdateState();
        radialCanvas.enabled = false;
        backgroundCanvas.enabled = false;
        robotsCanvas.enabled = false;
        quickSlotCanvas.enabled = true;
        closeMenuAudio.Play();

        enabled = false;
        return curMenuItem;
    }

    public void GetCurrentMenuItem()
    {
        float angle = 0;
        if (!lookDAO.playingOnController)
        {
            Mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            toVector2M = new Vector2(Mouseposition.x / Screen.width, Mouseposition.y / Screen.height);
            if (toVector2M.x > 0.25f || toVector2M.x < -0.25f || toVector2M.y > 0.25f || toVector2M.y < -0.25f)
            {
                angle = Mathf.Atan2(fromVector2M.y - centercircle.y, fromVector2M.x - centercircle.x) - Mathf.Atan2(toVector2M.y - centercircle.y, toVector2M.x - centercircle.x) * Mathf.Rad2Deg;
                angle = AngleCalc(angle);
            }
        }

        if (player.LookAxis.x != 0 || player.LookAxis.y != 0)
        {
            angle = Mathf.Atan2(-player.LookAxis.y, player.LookAxis.x);
            angle = angle * Mathf.Rad2Deg;
            angle = AngleCalc(angle);
        }


    }

    private float AngleCalc(float angle)
    {
        angle += 135;
        if (angle < 0) { angle += 360; }

        curMenuItem = (int)(angle / (360 / menuItems));

        if (curMenuItem != oldMenuItem)
        {
            CurrentButtonVisualFeedback();
            //SelectedPlay
            if (radialPart[curMenuItem].GetRobotType() != null)
            {
                selectedAudio.Play();
            }
            else { selectedAudioEmpty.Play(); }
        }

        return angle;
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
        quickSlot.UISetRobot(radialPart[curMenuItem].GetRobotType());
    }

    private void CurrentButtonVisualFeedback()
    {
        radialPart[curMenuItem].VisualFeedBack();
        radialPart[oldMenuItem].noVisualFeedBack();
        oldMenuItem = curMenuItem;
    }
}
