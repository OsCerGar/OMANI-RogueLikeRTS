using Rewired;
using Rewired.ControllerExtensions;
using UnityEngine;

public class PlayerInputInterface : MonoBehaviour
{

    //Rewired
    public Rewired.Player inputs; // The Rewired Player
    public int playerId = 0;
    public IDualShock4Extension ds4;


    //Input variables
    //Movement
    Vector2 movementAxis, movementAxisController, lookAxis, robotQuickSelection;
    bool laser, summon, radialMenu, dash;

    public Vector2 MovementAxis { get => movementAxis; set => movementAxis = value; }
    public Vector2 MovementAxisController { get => movementAxisController; set => movementAxisController = value; }
    public bool Laser { get => laser; set => laser = value; }
    public bool Dash { get => dash; set => dash = value; }
    public Vector2 LookAxis { get => lookAxis; set => lookAxis = value; }
    public bool Summon { get => summon; set => summon = value; }
    public bool RadialMenu { get => radialMenu; set => radialMenu = value; }
    public Vector2 RobotQuickSelection { get => robotQuickSelection; set => robotQuickSelection = value; }

    //Pointer

    //Actions


    // Start is called before the first frame update
    void OnEnable()
    {
        if (inputs == null)
        {
            inputs = ReInput.players.GetPlayer(playerId);
        }
        else { enabled = false; }
        if (inputs.controllers.Joysticks.Count > 0)
        {
            ds4 = inputs.controllers.Joysticks[0].GetExtension<IDualShock4Extension>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputs = ReInput.players.GetPlayer(playerId);

        RestartControllerAxis();
        ControllerLookAxis();
        Movement();
        Actions();
        laser = inputs.GetButton("FireLaser");
        dash = inputs.GetButton("Dash");
    }

    private void RobotSelection()
    {
        robotQuickSelection.x = inputs.GetAxis("RobotSelectionHorizontal");
        robotQuickSelection.y = inputs.GetAxis("RobotSelectionVertical");
    }
    private void Actions()
    {
        RobotSelection();
    }

    private void Movement()
    {
        movementAxisController.x = inputs.GetAxis("HorizontalGamepad");
        movementAxisController.y = inputs.GetAxis("VerticalGamepad");
        movementAxis.x = inputs.GetAxis("HorizontalKeyboard");
        movementAxis.y = inputs.GetAxis("VerticalKeyboard");
    }

    void RestartControllerAxis()
    {
        movementAxisController = new Vector2(0, 0);
        movementAxis = new Vector2(0, 0);
        robotQuickSelection = new Vector2(0, 0);
        lookAxis = new Vector2(0, 0);
        laser = false;
        summon = false;
        dash = false;
    }

    void ControllerLookAxis()
    {
        lookAxis.x = inputs.GetAxis("HorizontalRightGamepad");
        lookAxis.y = inputs.GetAxis("VerticalRightGamepad");
    }

    public void SetVibration(int _motor, float _amount, float _time, bool _stops)
    {
        if (inputs.controllers.Joysticks.Count > 0 && inputs.controllers.Joysticks[0].supportsVibration)
        {
            if (_motor < 2)
            {
                inputs.SetVibration(_motor, _amount, _time);
            }
            inputs.SetVibration(_motor, _amount, _time);

            if (_motor == 2)
            {
                inputs.SetVibration(0, _amount, _time);
                inputs.SetVibration(1, _amount, _time);

            }
        }
    }

    public void SetDS4Lights(Color _color)
    {
        if (ds4 != null)
        {
            ds4.SetLightColor(_color);
        }
    }
}
