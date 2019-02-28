// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Gameplay/OMANINPUT.inputactions'

using System;
using UnityEngine;
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
        m_PLAYER_LASERZONE = m_PLAYER.GetAction("LASERZONE");
        m_PLAYER_LASERSTRONGPREPARATION = m_PLAYER.GetAction("LASERSTRONGPREPARATION");
        m_PLAYER_LASERSTRONG = m_PLAYER.GetAction("LASERSTRONG");
        m_PLAYER_Order = m_PLAYER.GetAction("Order");
        m_PLAYER_WASD = m_PLAYER.GetAction("WASD");
        m_PLAYER_Joystick = m_PLAYER.GetAction("Joystick");
        m_PLAYER_RadialMenuUp = m_PLAYER.GetAction("RadialMenuUp");
        m_PLAYER_RadialMenuDown = m_PLAYER.GetAction("RadialMenuDown");
        m_PLAYER_LASERZONERELEASE = m_PLAYER.GetAction("LASERZONERELEASE");
        m_PLAYER_HEARTHSTONE = m_PLAYER.GetAction("HEARTHSTONE");
        m_PLAYER_RightStick = m_PLAYER.GetAction("RightStick");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_PLAYER = null;
        m_PLAYER_LASERZONE = null;
        m_PLAYER_LASERSTRONGPREPARATION = null;
        m_PLAYER_LASERSTRONG = null;
        m_PLAYER_Order = null;
        m_PLAYER_WASD = null;
        m_PLAYER_Joystick = null;
        m_PLAYER_RadialMenuUp = null;
        m_PLAYER_RadialMenuDown = null;
        m_PLAYER_LASERZONERELEASE = null;
        m_PLAYER_HEARTHSTONE = null;
        m_PLAYER_RightStick = null;
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
    private InputAction m_PLAYER_LASERZONE;
    private InputAction m_PLAYER_LASERSTRONGPREPARATION;
    private InputAction m_PLAYER_LASERSTRONG;
    private InputAction m_PLAYER_Order;
    private InputAction m_PLAYER_WASD;
    private InputAction m_PLAYER_Joystick;
    private InputAction m_PLAYER_RadialMenuUp;
    private InputAction m_PLAYER_RadialMenuDown;
    private InputAction m_PLAYER_LASERZONERELEASE;
    private InputAction m_PLAYER_HEARTHSTONE;
    private InputAction m_PLAYER_RightStick;
    public struct PLAYERActions
    {
        private OMANINPUT m_Wrapper;
        public PLAYERActions(OMANINPUT wrapper) { m_Wrapper = wrapper; }
        public InputAction @LASERZONE { get { return m_Wrapper.m_PLAYER_LASERZONE; } }
        public InputAction @LASERSTRONGPREPARATION { get { return m_Wrapper.m_PLAYER_LASERSTRONGPREPARATION; } }
        public InputAction @LASERSTRONG { get { return m_Wrapper.m_PLAYER_LASERSTRONG; } }
        public InputAction @Order { get { return m_Wrapper.m_PLAYER_Order; } }
        public InputAction @WASD { get { return m_Wrapper.m_PLAYER_WASD; } }
        public InputAction @Joystick { get { return m_Wrapper.m_PLAYER_Joystick; } }
        public InputAction @RadialMenuUp { get { return m_Wrapper.m_PLAYER_RadialMenuUp; } }
        public InputAction @RadialMenuDown { get { return m_Wrapper.m_PLAYER_RadialMenuDown; } }
        public InputAction @LASERZONERELEASE { get { return m_Wrapper.m_PLAYER_LASERZONERELEASE; } }
        public InputAction @HEARTHSTONE { get { return m_Wrapper.m_PLAYER_HEARTHSTONE; } }
        public InputAction @RightStick { get { return m_Wrapper.m_PLAYER_RightStick; } }
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
}
