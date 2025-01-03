//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/TowerMergeTD/Scripts/Game/Gameplay/Player/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""eee6abcf-2999-4416-9728-ebdaf20e411e"",
            ""actions"": [
                {
                    ""name"": ""Delta"",
                    ""type"": ""Value"",
                    ""id"": ""15ca3dd3-08d0-4e98-aecb-697fcb207c2e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftButton"",
                    ""type"": ""Button"",
                    ""id"": ""07cf1000-84ec-4f71-8d9f-ebae833fb847"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""dedad9e6-3487-43ad-96d3-e50cc6cb6b24"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""709d92ae-d950-45d2-a9d8-a41c940d7309"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2deb3f59-d02e-4a12-b674-58ece872459d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""LeftButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81ce92d0-42d1-4a86-b870-495374cab40c"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TouchScreen"",
            ""id"": ""5fd84b2b-63ec-41ed-a0a8-698f64bef4d4"",
            ""actions"": [
                {
                    ""name"": ""PrimaryTouchPressed"",
                    ""type"": ""Button"",
                    ""id"": ""59a2a8c9-6158-45f9-a0bd-77f6631db1f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryTouchDelta"",
                    ""type"": ""Value"",
                    ""id"": ""f9035d5f-ad20-41f0-98a7-17e3f6d49f72"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondaryTouchPressed"",
                    ""type"": ""Button"",
                    ""id"": ""80242c03-8668-49bd-811b-57e229434b31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryTouchDelta"",
                    ""type"": ""Value"",
                    ""id"": ""45d5ee5d-d8b5-4456-b5fc-3754a4c4a1fa"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5c288ac9-5b84-4f72-90f4-e30811255e38"",
                    ""path"": ""<Touchscreen>/touch1/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""SecondaryTouchDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""829bc0c4-c655-41be-9cde-3f7cb49bf727"",
                    ""path"": ""<Touchscreen>/touch1/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""SecondaryTouchPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c9cd335-397b-4cff-9a38-97d1cc522611"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""PrimaryTouchPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ce4927d-5859-4022-ae55-29ec1abfc1f8"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""PrimaryTouchDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Desktop"",
            ""bindingGroup"": ""Desktop"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_Delta = m_Mouse.FindAction("Delta", throwIfNotFound: true);
        m_Mouse_LeftButton = m_Mouse.FindAction("LeftButton", throwIfNotFound: true);
        m_Mouse_Scroll = m_Mouse.FindAction("Scroll", throwIfNotFound: true);
        // TouchScreen
        m_TouchScreen = asset.FindActionMap("TouchScreen", throwIfNotFound: true);
        m_TouchScreen_PrimaryTouchPressed = m_TouchScreen.FindAction("PrimaryTouchPressed", throwIfNotFound: true);
        m_TouchScreen_PrimaryTouchDelta = m_TouchScreen.FindAction("PrimaryTouchDelta", throwIfNotFound: true);
        m_TouchScreen_SecondaryTouchPressed = m_TouchScreen.FindAction("SecondaryTouchPressed", throwIfNotFound: true);
        m_TouchScreen_SecondaryTouchDelta = m_TouchScreen.FindAction("SecondaryTouchDelta", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Mouse
    private readonly InputActionMap m_Mouse;
    private List<IMouseActions> m_MouseActionsCallbackInterfaces = new List<IMouseActions>();
    private readonly InputAction m_Mouse_Delta;
    private readonly InputAction m_Mouse_LeftButton;
    private readonly InputAction m_Mouse_Scroll;
    public struct MouseActions
    {
        private @PlayerInputActions m_Wrapper;
        public MouseActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Delta => m_Wrapper.m_Mouse_Delta;
        public InputAction @LeftButton => m_Wrapper.m_Mouse_LeftButton;
        public InputAction @Scroll => m_Wrapper.m_Mouse_Scroll;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void AddCallbacks(IMouseActions instance)
        {
            if (instance == null || m_Wrapper.m_MouseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MouseActionsCallbackInterfaces.Add(instance);
            @Delta.started += instance.OnDelta;
            @Delta.performed += instance.OnDelta;
            @Delta.canceled += instance.OnDelta;
            @LeftButton.started += instance.OnLeftButton;
            @LeftButton.performed += instance.OnLeftButton;
            @LeftButton.canceled += instance.OnLeftButton;
            @Scroll.started += instance.OnScroll;
            @Scroll.performed += instance.OnScroll;
            @Scroll.canceled += instance.OnScroll;
        }

        private void UnregisterCallbacks(IMouseActions instance)
        {
            @Delta.started -= instance.OnDelta;
            @Delta.performed -= instance.OnDelta;
            @Delta.canceled -= instance.OnDelta;
            @LeftButton.started -= instance.OnLeftButton;
            @LeftButton.performed -= instance.OnLeftButton;
            @LeftButton.canceled -= instance.OnLeftButton;
            @Scroll.started -= instance.OnScroll;
            @Scroll.performed -= instance.OnScroll;
            @Scroll.canceled -= instance.OnScroll;
        }

        public void RemoveCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMouseActions instance)
        {
            foreach (var item in m_Wrapper.m_MouseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MouseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MouseActions @Mouse => new MouseActions(this);

    // TouchScreen
    private readonly InputActionMap m_TouchScreen;
    private List<ITouchScreenActions> m_TouchScreenActionsCallbackInterfaces = new List<ITouchScreenActions>();
    private readonly InputAction m_TouchScreen_PrimaryTouchPressed;
    private readonly InputAction m_TouchScreen_PrimaryTouchDelta;
    private readonly InputAction m_TouchScreen_SecondaryTouchPressed;
    private readonly InputAction m_TouchScreen_SecondaryTouchDelta;
    public struct TouchScreenActions
    {
        private @PlayerInputActions m_Wrapper;
        public TouchScreenActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryTouchPressed => m_Wrapper.m_TouchScreen_PrimaryTouchPressed;
        public InputAction @PrimaryTouchDelta => m_Wrapper.m_TouchScreen_PrimaryTouchDelta;
        public InputAction @SecondaryTouchPressed => m_Wrapper.m_TouchScreen_SecondaryTouchPressed;
        public InputAction @SecondaryTouchDelta => m_Wrapper.m_TouchScreen_SecondaryTouchDelta;
        public InputActionMap Get() { return m_Wrapper.m_TouchScreen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchScreenActions set) { return set.Get(); }
        public void AddCallbacks(ITouchScreenActions instance)
        {
            if (instance == null || m_Wrapper.m_TouchScreenActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TouchScreenActionsCallbackInterfaces.Add(instance);
            @PrimaryTouchPressed.started += instance.OnPrimaryTouchPressed;
            @PrimaryTouchPressed.performed += instance.OnPrimaryTouchPressed;
            @PrimaryTouchPressed.canceled += instance.OnPrimaryTouchPressed;
            @PrimaryTouchDelta.started += instance.OnPrimaryTouchDelta;
            @PrimaryTouchDelta.performed += instance.OnPrimaryTouchDelta;
            @PrimaryTouchDelta.canceled += instance.OnPrimaryTouchDelta;
            @SecondaryTouchPressed.started += instance.OnSecondaryTouchPressed;
            @SecondaryTouchPressed.performed += instance.OnSecondaryTouchPressed;
            @SecondaryTouchPressed.canceled += instance.OnSecondaryTouchPressed;
            @SecondaryTouchDelta.started += instance.OnSecondaryTouchDelta;
            @SecondaryTouchDelta.performed += instance.OnSecondaryTouchDelta;
            @SecondaryTouchDelta.canceled += instance.OnSecondaryTouchDelta;
        }

        private void UnregisterCallbacks(ITouchScreenActions instance)
        {
            @PrimaryTouchPressed.started -= instance.OnPrimaryTouchPressed;
            @PrimaryTouchPressed.performed -= instance.OnPrimaryTouchPressed;
            @PrimaryTouchPressed.canceled -= instance.OnPrimaryTouchPressed;
            @PrimaryTouchDelta.started -= instance.OnPrimaryTouchDelta;
            @PrimaryTouchDelta.performed -= instance.OnPrimaryTouchDelta;
            @PrimaryTouchDelta.canceled -= instance.OnPrimaryTouchDelta;
            @SecondaryTouchPressed.started -= instance.OnSecondaryTouchPressed;
            @SecondaryTouchPressed.performed -= instance.OnSecondaryTouchPressed;
            @SecondaryTouchPressed.canceled -= instance.OnSecondaryTouchPressed;
            @SecondaryTouchDelta.started -= instance.OnSecondaryTouchDelta;
            @SecondaryTouchDelta.performed -= instance.OnSecondaryTouchDelta;
            @SecondaryTouchDelta.canceled -= instance.OnSecondaryTouchDelta;
        }

        public void RemoveCallbacks(ITouchScreenActions instance)
        {
            if (m_Wrapper.m_TouchScreenActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITouchScreenActions instance)
        {
            foreach (var item in m_Wrapper.m_TouchScreenActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TouchScreenActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TouchScreenActions @TouchScreen => new TouchScreenActions(this);
    private int m_DesktopSchemeIndex = -1;
    public InputControlScheme DesktopScheme
    {
        get
        {
            if (m_DesktopSchemeIndex == -1) m_DesktopSchemeIndex = asset.FindControlSchemeIndex("Desktop");
            return asset.controlSchemes[m_DesktopSchemeIndex];
        }
    }
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IMouseActions
    {
        void OnDelta(InputAction.CallbackContext context);
        void OnLeftButton(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
    }
    public interface ITouchScreenActions
    {
        void OnPrimaryTouchPressed(InputAction.CallbackContext context);
        void OnPrimaryTouchDelta(InputAction.CallbackContext context);
        void OnSecondaryTouchPressed(InputAction.CallbackContext context);
        void OnSecondaryTouchDelta(InputAction.CallbackContext context);
    }
}
