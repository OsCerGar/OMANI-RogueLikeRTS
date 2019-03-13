// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Gameplay/OMANINPUT.inputactions'

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;


[Serializable]
public class OMANINPUT : InputActionAssetReference
{
    public OMANINPUT()
    {
    }
    public OMANINPUT(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // PLAYER
        m_PLAYER = asset.GetActionMap("PLAYER");
        m_PLAYER_SUMMON = m_PLAYER.GetAction("SUMMON");
        if (m_PLAYERSUMMONActionStarted != null)
            m_PLAYER_SUMMON.started += m_PLAYERSUMMONActionStarted.Invoke;
        if (m_PLAYERSUMMONActionPerformed != null)
            m_PLAYER_SUMMON.performed += m_PLAYERSUMMONActionPerformed.Invoke;
        if (m_PLAYERSUMMONActionCancelled != null)
            m_PLAYER_SUMMON.cancelled += m_PLAYERSUMMONActionCancelled.Invoke;
        m_PLAYER_OrderLaser = m_PLAYER.GetAction("OrderLaser");
        if (m_PLAYEROrderLaserActionStarted != null)
            m_PLAYER_OrderLaser.started += m_PLAYEROrderLaserActionStarted.Invoke;
        if (m_PLAYEROrderLaserActionPerformed != null)
            m_PLAYER_OrderLaser.performed += m_PLAYEROrderLaserActionPerformed.Invoke;
        if (m_PLAYEROrderLaserActionCancelled != null)
            m_PLAYER_OrderLaser.cancelled += m_PLAYEROrderLaserActionCancelled.Invoke;
        m_PLAYER_WASD = m_PLAYER.GetAction("WASD");
        if (m_PLAYERWASDActionStarted != null)
            m_PLAYER_WASD.started += m_PLAYERWASDActionStarted.Invoke;
        if (m_PLAYERWASDActionPerformed != null)
            m_PLAYER_WASD.performed += m_PLAYERWASDActionPerformed.Invoke;
        if (m_PLAYERWASDActionCancelled != null)
            m_PLAYER_WASD.cancelled += m_PLAYERWASDActionCancelled.Invoke;
        m_PLAYER_Joystick = m_PLAYER.GetAction("Joystick");
        if (m_PLAYERJoystickActionStarted != null)
            m_PLAYER_Joystick.started += m_PLAYERJoystickActionStarted.Invoke;
        if (m_PLAYERJoystickActionPerformed != null)
            m_PLAYER_Joystick.performed += m_PLAYERJoystickActionPerformed.Invoke;
        if (m_PLAYERJoystickActionCancelled != null)
            m_PLAYER_Joystick.cancelled += m_PLAYERJoystickActionCancelled.Invoke;
        m_PLAYER_RadialMenuUp = m_PLAYER.GetAction("RadialMenuUp");
        if (m_PLAYERRadialMenuUpActionStarted != null)
            m_PLAYER_RadialMenuUp.started += m_PLAYERRadialMenuUpActionStarted.Invoke;
        if (m_PLAYERRadialMenuUpActionPerformed != null)
            m_PLAYER_RadialMenuUp.performed += m_PLAYERRadialMenuUpActionPerformed.Invoke;
        if (m_PLAYERRadialMenuUpActionCancelled != null)
            m_PLAYER_RadialMenuUp.cancelled += m_PLAYERRadialMenuUpActionCancelled.Invoke;
        m_PLAYER_RadialMenuDown = m_PLAYER.GetAction("RadialMenuDown");
        if (m_PLAYERRadialMenuDownActionStarted != null)
            m_PLAYER_RadialMenuDown.started += m_PLAYERRadialMenuDownActionStarted.Invoke;
        if (m_PLAYERRadialMenuDownActionPerformed != null)
            m_PLAYER_RadialMenuDown.performed += m_PLAYERRadialMenuDownActionPerformed.Invoke;
        if (m_PLAYERRadialMenuDownActionCancelled != null)
            m_PLAYER_RadialMenuDown.cancelled += m_PLAYERRadialMenuDownActionCancelled.Invoke;
        m_PLAYER_LASERZONERELEASE = m_PLAYER.GetAction("LASERZONERELEASE");
        if (m_PLAYERLASERZONERELEASEActionStarted != null)
            m_PLAYER_LASERZONERELEASE.started += m_PLAYERLASERZONERELEASEActionStarted.Invoke;
        if (m_PLAYERLASERZONERELEASEActionPerformed != null)
            m_PLAYER_LASERZONERELEASE.performed += m_PLAYERLASERZONERELEASEActionPerformed.Invoke;
        if (m_PLAYERLASERZONERELEASEActionCancelled != null)
            m_PLAYER_LASERZONERELEASE.cancelled += m_PLAYERLASERZONERELEASEActionCancelled.Invoke;
        m_PLAYER_HEARTHSTONE = m_PLAYER.GetAction("HEARTHSTONE");
        if (m_PLAYERHEARTHSTONEActionStarted != null)
            m_PLAYER_HEARTHSTONE.started += m_PLAYERHEARTHSTONEActionStarted.Invoke;
        if (m_PLAYERHEARTHSTONEActionPerformed != null)
            m_PLAYER_HEARTHSTONE.performed += m_PLAYERHEARTHSTONEActionPerformed.Invoke;
        if (m_PLAYERHEARTHSTONEActionCancelled != null)
            m_PLAYER_HEARTHSTONE.cancelled += m_PLAYERHEARTHSTONEActionCancelled.Invoke;
        m_PLAYER_RightStick = m_PLAYER.GetAction("RightStick");
        if (m_PLAYERRightStickActionStarted != null)
            m_PLAYER_RightStick.started += m_PLAYERRightStickActionStarted.Invoke;
        if (m_PLAYERRightStickActionPerformed != null)
            m_PLAYER_RightStick.performed += m_PLAYERRightStickActionPerformed.Invoke;
        if (m_PLAYERRightStickActionCancelled != null)
            m_PLAYER_RightStick.cancelled += m_PLAYERRightStickActionCancelled.Invoke;
        m_PLAYER_ControllerFree = m_PLAYER.GetAction("ControllerFree");
        if (m_PLAYERControllerFreeActionStarted != null)
            m_PLAYER_ControllerFree.started += m_PLAYERControllerFreeActionStarted.Invoke;
        if (m_PLAYERControllerFreeActionPerformed != null)
            m_PLAYER_ControllerFree.performed += m_PLAYERControllerFreeActionPerformed.Invoke;
        if (m_PLAYERControllerFreeActionCancelled != null)
            m_PLAYER_ControllerFree.cancelled += m_PLAYERControllerFreeActionCancelled.Invoke;
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_PLAYER = null;
        m_PLAYER_SUMMON = null;
        if (m_PLAYERSUMMONActionStarted != null)
            m_PLAYER_SUMMON.started -= m_PLAYERSUMMONActionStarted.Invoke;
        if (m_PLAYERSUMMONActionPerformed != null)
            m_PLAYER_SUMMON.performed -= m_PLAYERSUMMONActionPerformed.Invoke;
        if (m_PLAYERSUMMONActionCancelled != null)
            m_PLAYER_SUMMON.cancelled -= m_PLAYERSUMMONActionCancelled.Invoke;
        m_PLAYER_OrderLaser = null;
        if (m_PLAYEROrderLaserActionStarted != null)
            m_PLAYER_OrderLaser.started -= m_PLAYEROrderLaserActionStarted.Invoke;
        if (m_PLAYEROrderLaserActionPerformed != null)
            m_PLAYER_OrderLaser.performed -= m_PLAYEROrderLaserActionPerformed.Invoke;
        if (m_PLAYEROrderLaserActionCancelled != null)
            m_PLAYER_OrderLaser.cancelled -= m_PLAYEROrderLaserActionCancelled.Invoke;
        m_PLAYER_WASD = null;
        if (m_PLAYERWASDActionStarted != null)
            m_PLAYER_WASD.started -= m_PLAYERWASDActionStarted.Invoke;
        if (m_PLAYERWASDActionPerformed != null)
            m_PLAYER_WASD.performed -= m_PLAYERWASDActionPerformed.Invoke;
        if (m_PLAYERWASDActionCancelled != null)
            m_PLAYER_WASD.cancelled -= m_PLAYERWASDActionCancelled.Invoke;
        m_PLAYER_Joystick = null;
        if (m_PLAYERJoystickActionStarted != null)
            m_PLAYER_Joystick.started -= m_PLAYERJoystickActionStarted.Invoke;
        if (m_PLAYERJoystickActionPerformed != null)
            m_PLAYER_Joystick.performed -= m_PLAYERJoystickActionPerformed.Invoke;
        if (m_PLAYERJoystickActionCancelled != null)
            m_PLAYER_Joystick.cancelled -= m_PLAYERJoystickActionCancelled.Invoke;
        m_PLAYER_RadialMenuUp = null;
        if (m_PLAYERRadialMenuUpActionStarted != null)
            m_PLAYER_RadialMenuUp.started -= m_PLAYERRadialMenuUpActionStarted.Invoke;
        if (m_PLAYERRadialMenuUpActionPerformed != null)
            m_PLAYER_RadialMenuUp.performed -= m_PLAYERRadialMenuUpActionPerformed.Invoke;
        if (m_PLAYERRadialMenuUpActionCancelled != null)
            m_PLAYER_RadialMenuUp.cancelled -= m_PLAYERRadialMenuUpActionCancelled.Invoke;
        m_PLAYER_RadialMenuDown = null;
        if (m_PLAYERRadialMenuDownActionStarted != null)
            m_PLAYER_RadialMenuDown.started -= m_PLAYERRadialMenuDownActionStarted.Invoke;
        if (m_PLAYERRadialMenuDownActionPerformed != null)
            m_PLAYER_RadialMenuDown.performed -= m_PLAYERRadialMenuDownActionPerformed.Invoke;
        if (m_PLAYERRadialMenuDownActionCancelled != null)
            m_PLAYER_RadialMenuDown.cancelled -= m_PLAYERRadialMenuDownActionCancelled.Invoke;
        m_PLAYER_LASERZONERELEASE = null;
        if (m_PLAYERLASERZONERELEASEActionStarted != null)
            m_PLAYER_LASERZONERELEASE.started -= m_PLAYERLASERZONERELEASEActionStarted.Invoke;
        if (m_PLAYERLASERZONERELEASEActionPerformed != null)
            m_PLAYER_LASERZONERELEASE.performed -= m_PLAYERLASERZONERELEASEActionPerformed.Invoke;
        if (m_PLAYERLASERZONERELEASEActionCancelled != null)
            m_PLAYER_LASERZONERELEASE.cancelled -= m_PLAYERLASERZONERELEASEActionCancelled.Invoke;
        m_PLAYER_HEARTHSTONE = null;
        if (m_PLAYERHEARTHSTONEActionStarted != null)
            m_PLAYER_HEARTHSTONE.started -= m_PLAYERHEARTHSTONEActionStarted.Invoke;
        if (m_PLAYERHEARTHSTONEActionPerformed != null)
            m_PLAYER_HEARTHSTONE.performed -= m_PLAYERHEARTHSTONEActionPerformed.Invoke;
        if (m_PLAYERHEARTHSTONEActionCancelled != null)
            m_PLAYER_HEARTHSTONE.cancelled -= m_PLAYERHEARTHSTONEActionCancelled.Invoke;
        m_PLAYER_RightStick = null;
        if (m_PLAYERRightStickActionStarted != null)
            m_PLAYER_RightStick.started -= m_PLAYERRightStickActionStarted.Invoke;
        if (m_PLAYERRightStickActionPerformed != null)
            m_PLAYER_RightStick.performed -= m_PLAYERRightStickActionPerformed.Invoke;
        if (m_PLAYERRightStickActionCancelled != null)
            m_PLAYER_RightStick.cancelled -= m_PLAYERRightStickActionCancelled.Invoke;
        m_PLAYER_ControllerFree = null;
        if (m_PLAYERControllerFreeActionStarted != null)
            m_PLAYER_ControllerFree.started -= m_PLAYERControllerFreeActionStarted.Invoke;
        if (m_PLAYERControllerFreeActionPerformed != null)
            m_PLAYER_ControllerFree.performed -= m_PLAYERControllerFreeActionPerformed.Invoke;
        if (m_PLAYERControllerFreeActionCancelled != null)
            m_PLAYER_ControllerFree.cancelled -= m_PLAYERControllerFreeActionCancelled.Invoke;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // PLAYER
    private InputActionMap m_PLAYER;
    private InputAction m_PLAYER_SUMMON;
    [SerializeField] private ActionEvent m_PLAYERSUMMONActionStarted;
    [SerializeField] private ActionEvent m_PLAYERSUMMONActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERSUMMONActionCancelled;
    private InputAction m_PLAYER_OrderLaser;
    [SerializeField] private ActionEvent m_PLAYEROrderLaserActionStarted;
    [SerializeField] private ActionEvent m_PLAYEROrderLaserActionPerformed;
    [SerializeField] private ActionEvent m_PLAYEROrderLaserActionCancelled;
    private InputAction m_PLAYER_WASD;
    [SerializeField] private ActionEvent m_PLAYERWASDActionStarted;
    [SerializeField] private ActionEvent m_PLAYERWASDActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERWASDActionCancelled;
    private InputAction m_PLAYER_Joystick;
    [SerializeField] private ActionEvent m_PLAYERJoystickActionStarted;
    [SerializeField] private ActionEvent m_PLAYERJoystickActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERJoystickActionCancelled;
    private InputAction m_PLAYER_RadialMenuUp;
    [SerializeField] private ActionEvent m_PLAYERRadialMenuUpActionStarted;
    [SerializeField] private ActionEvent m_PLAYERRadialMenuUpActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERRadialMenuUpActionCancelled;
    private InputAction m_PLAYER_RadialMenuDown;
    [SerializeField] private ActionEvent m_PLAYERRadialMenuDownActionStarted;
    [SerializeField] private ActionEvent m_PLAYERRadialMenuDownActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERRadialMenuDownActionCancelled;
    private InputAction m_PLAYER_LASERZONERELEASE;
    [SerializeField] private ActionEvent m_PLAYERLASERZONERELEASEActionStarted;
    [SerializeField] private ActionEvent m_PLAYERLASERZONERELEASEActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERLASERZONERELEASEActionCancelled;
    private InputAction m_PLAYER_HEARTHSTONE;
    [SerializeField] private ActionEvent m_PLAYERHEARTHSTONEActionStarted;
    [SerializeField] private ActionEvent m_PLAYERHEARTHSTONEActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERHEARTHSTONEActionCancelled;
    private InputAction m_PLAYER_RightStick;
    [SerializeField] private ActionEvent m_PLAYERRightStickActionStarted;
    [SerializeField] private ActionEvent m_PLAYERRightStickActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERRightStickActionCancelled;
    private InputAction m_PLAYER_ControllerFree;
    [SerializeField] private ActionEvent m_PLAYERControllerFreeActionStarted;
    [SerializeField] private ActionEvent m_PLAYERControllerFreeActionPerformed;
    [SerializeField] private ActionEvent m_PLAYERControllerFreeActionCancelled;
    public struct PLAYERActions
    {
        private OMANINPUT m_Wrapper;
        public PLAYERActions(OMANINPUT wrapper) { m_Wrapper = wrapper; }
        public InputAction @SUMMON { get { return m_Wrapper.m_PLAYER_SUMMON; } }
        public ActionEvent SUMMONStarted { get { return m_Wrapper.m_PLAYERSUMMONActionStarted; } }
        public ActionEvent SUMMONPerformed { get { return m_Wrapper.m_PLAYERSUMMONActionPerformed; } }
        public ActionEvent SUMMONCancelled { get { return m_Wrapper.m_PLAYERSUMMONActionCancelled; } }
        public InputAction @OrderLaser { get { return m_Wrapper.m_PLAYER_OrderLaser; } }
        public ActionEvent OrderLaserStarted { get { return m_Wrapper.m_PLAYEROrderLaserActionStarted; } }
        public ActionEvent OrderLaserPerformed { get { return m_Wrapper.m_PLAYEROrderLaserActionPerformed; } }
        public ActionEvent OrderLaserCancelled { get { return m_Wrapper.m_PLAYEROrderLaserActionCancelled; } }
        public InputAction @WASD { get { return m_Wrapper.m_PLAYER_WASD; } }
        public ActionEvent WASDStarted { get { return m_Wrapper.m_PLAYERWASDActionStarted; } }
        public ActionEvent WASDPerformed { get { return m_Wrapper.m_PLAYERWASDActionPerformed; } }
        public ActionEvent WASDCancelled { get { return m_Wrapper.m_PLAYERWASDActionCancelled; } }
        public InputAction @Joystick { get { return m_Wrapper.m_PLAYER_Joystick; } }
        public ActionEvent JoystickStarted { get { return m_Wrapper.m_PLAYERJoystickActionStarted; } }
        public ActionEvent JoystickPerformed { get { return m_Wrapper.m_PLAYERJoystickActionPerformed; } }
        public ActionEvent JoystickCancelled { get { return m_Wrapper.m_PLAYERJoystickActionCancelled; } }
        public InputAction @RadialMenuUp { get { return m_Wrapper.m_PLAYER_RadialMenuUp; } }
        public ActionEvent RadialMenuUpStarted { get { return m_Wrapper.m_PLAYERRadialMenuUpActionStarted; } }
        public ActionEvent RadialMenuUpPerformed { get { return m_Wrapper.m_PLAYERRadialMenuUpActionPerformed; } }
        public ActionEvent RadialMenuUpCancelled { get { return m_Wrapper.m_PLAYERRadialMenuUpActionCancelled; } }
        public InputAction @RadialMenuDown { get { return m_Wrapper.m_PLAYER_RadialMenuDown; } }
        public ActionEvent RadialMenuDownStarted { get { return m_Wrapper.m_PLAYERRadialMenuDownActionStarted; } }
        public ActionEvent RadialMenuDownPerformed { get { return m_Wrapper.m_PLAYERRadialMenuDownActionPerformed; } }
        public ActionEvent RadialMenuDownCancelled { get { return m_Wrapper.m_PLAYERRadialMenuDownActionCancelled; } }
        public InputAction @LASERZONERELEASE { get { return m_Wrapper.m_PLAYER_LASERZONERELEASE; } }
        public ActionEvent LASERZONERELEASEStarted { get { return m_Wrapper.m_PLAYERLASERZONERELEASEActionStarted; } }
        public ActionEvent LASERZONERELEASEPerformed { get { return m_Wrapper.m_PLAYERLASERZONERELEASEActionPerformed; } }
        public ActionEvent LASERZONERELEASECancelled { get { return m_Wrapper.m_PLAYERLASERZONERELEASEActionCancelled; } }
        public InputAction @HEARTHSTONE { get { return m_Wrapper.m_PLAYER_HEARTHSTONE; } }
        public ActionEvent HEARTHSTONEStarted { get { return m_Wrapper.m_PLAYERHEARTHSTONEActionStarted; } }
        public ActionEvent HEARTHSTONEPerformed { get { return m_Wrapper.m_PLAYERHEARTHSTONEActionPerformed; } }
        public ActionEvent HEARTHSTONECancelled { get { return m_Wrapper.m_PLAYERHEARTHSTONEActionCancelled; } }
        public InputAction @RightStick { get { return m_Wrapper.m_PLAYER_RightStick; } }
        public ActionEvent RightStickStarted { get { return m_Wrapper.m_PLAYERRightStickActionStarted; } }
        public ActionEvent RightStickPerformed { get { return m_Wrapper.m_PLAYERRightStickActionPerformed; } }
        public ActionEvent RightStickCancelled { get { return m_Wrapper.m_PLAYERRightStickActionCancelled; } }
        public InputAction @ControllerFree { get { return m_Wrapper.m_PLAYER_ControllerFree; } }
        public ActionEvent ControllerFreeStarted { get { return m_Wrapper.m_PLAYERControllerFreeActionStarted; } }
        public ActionEvent ControllerFreePerformed { get { return m_Wrapper.m_PLAYERControllerFreeActionPerformed; } }
        public ActionEvent ControllerFreeCancelled { get { return m_Wrapper.m_PLAYERControllerFreeActionCancelled; } }
        public InputActionMap Get() { return m_Wrapper.m_PLAYER; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PLAYERActions set) { return set.Get(); }
    }
    public PLAYERActions @PLAYER
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PLAYERActions(this);
        }
    }
    private int m_keymouseSchemeIndex = -1;
    public InputControlScheme keymouseScheme
    {
        get

        {
            if (m_keymouseSchemeIndex == -1) m_keymouseSchemeIndex = asset.GetControlSchemeIndex("key+mouse");
            return asset.controlSchemes[m_keymouseSchemeIndex];
        }
    }
    private int m_ps4SchemeIndex = -1;
    public InputControlScheme ps4Scheme
    {
        get

        {
            if (m_ps4SchemeIndex == -1) m_ps4SchemeIndex = asset.GetControlSchemeIndex("ps4");
            return asset.controlSchemes[m_ps4SchemeIndex];
        }
    }
    private int m_XBOXSchemeIndex = -1;
    public InputControlScheme XBOXScheme
    {
        get

        {
            if (m_XBOXSchemeIndex == -1) m_XBOXSchemeIndex = asset.GetControlSchemeIndex("XBOX");
            return asset.controlSchemes[m_XBOXSchemeIndex];
        }
    }
    private int m_GenericControllerSchemeIndex = -1;
    public InputControlScheme GenericControllerScheme
    {
        get

        {
            if (m_GenericControllerSchemeIndex == -1) m_GenericControllerSchemeIndex = asset.GetControlSchemeIndex("GenericController");
            return asset.controlSchemes[m_GenericControllerSchemeIndex];
        }
    }
    private int m_ps4allSchemeIndex = -1;
    public InputControlScheme ps4allScheme
    {
        get

        {
            if (m_ps4allSchemeIndex == -1) m_ps4allSchemeIndex = asset.GetControlSchemeIndex("ps4all");
            return asset.controlSchemes[m_ps4allSchemeIndex];
        }
    }
    [Serializable]
    public class ActionEvent : UnityEvent<InputAction.CallbackContext>
    {
    }
}
