//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/TowerMergeTD/Scripts/Game/Gameplay/Player/PlayerInputActions.inputactions
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
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""709d92ae-d950-45d2-a9d8-a41c940d7309"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""LeftButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_Delta = m_Mouse.FindAction("Delta", throwIfNotFound: true);
        m_Mouse_LeftButton = m_Mouse.FindAction("LeftButton", throwIfNotFound: true);
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
    public struct MouseActions
    {
        private @PlayerInputActions m_Wrapper;
        public MouseActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Delta => m_Wrapper.m_Mouse_Delta;
        public InputAction @LeftButton => m_Wrapper.m_Mouse_LeftButton;
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
        }

        private void UnregisterCallbacks(IMouseActions instance)
        {
            @Delta.started -= instance.OnDelta;
            @Delta.performed -= instance.OnDelta;
            @Delta.canceled -= instance.OnDelta;
            @LeftButton.started -= instance.OnLeftButton;
            @LeftButton.performed -= instance.OnLeftButton;
            @LeftButton.canceled -= instance.OnLeftButton;
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
    public interface IMouseActions
    {
        void OnDelta(InputAction.CallbackContext context);
        void OnLeftButton(InputAction.CallbackContext context);
    }
}