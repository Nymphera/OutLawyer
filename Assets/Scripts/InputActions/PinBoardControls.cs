// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputActions/PinBoardControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PinBoardControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PinBoardControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PinBoardControls"",
    ""maps"": [
        {
            ""name"": ""PinBoard"",
            ""id"": ""a8d95204-0452-4c7d-a274-326f513cc369"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""eeb4a843-cdba-4631-b218-46d810e8f836"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3efde2e3-8833-481b-8464-433d14318b78"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""9868803f-b28c-49cc-b2e5-a48ac1acfd42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""ad417137-1a9f-4dcd-b738-48fd825b61de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bacfcc87-96b7-42f7-9315-a59305ddbb30"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5687f3f4-d858-448e-a632-79817e78e990"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1a891e9-154a-49c3-81d4-c5f92a21b310"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b8bdf8a-f1ee-465c-855f-8143339d3c76"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PinBoard
        m_PinBoard = asset.FindActionMap("PinBoard", throwIfNotFound: true);
        m_PinBoard_Move = m_PinBoard.FindAction("Move", throwIfNotFound: true);
        m_PinBoard_Zoom = m_PinBoard.FindAction("Zoom", throwIfNotFound: true);
        m_PinBoard_MouseLeftClick = m_PinBoard.FindAction("MouseLeftClick", throwIfNotFound: true);
        m_PinBoard_MouseRightClick = m_PinBoard.FindAction("MouseRightClick", throwIfNotFound: true);
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

    // PinBoard
    private readonly InputActionMap m_PinBoard;
    private IPinBoardActions m_PinBoardActionsCallbackInterface;
    private readonly InputAction m_PinBoard_Move;
    private readonly InputAction m_PinBoard_Zoom;
    private readonly InputAction m_PinBoard_MouseLeftClick;
    private readonly InputAction m_PinBoard_MouseRightClick;
    public struct PinBoardActions
    {
        private @PinBoardControls m_Wrapper;
        public PinBoardActions(@PinBoardControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PinBoard_Move;
        public InputAction @Zoom => m_Wrapper.m_PinBoard_Zoom;
        public InputAction @MouseLeftClick => m_Wrapper.m_PinBoard_MouseLeftClick;
        public InputAction @MouseRightClick => m_Wrapper.m_PinBoard_MouseRightClick;
        public InputActionMap Get() { return m_Wrapper.m_PinBoard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PinBoardActions set) { return set.Get(); }
        public void SetCallbacks(IPinBoardActions instance)
        {
            if (m_Wrapper.m_PinBoardActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMove;
                @Zoom.started -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnZoom;
                @MouseLeftClick.started -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMouseLeftClick;
                @MouseLeftClick.performed -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMouseLeftClick;
                @MouseLeftClick.canceled -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMouseLeftClick;
                @MouseRightClick.started -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMouseRightClick;
                @MouseRightClick.performed -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMouseRightClick;
                @MouseRightClick.canceled -= m_Wrapper.m_PinBoardActionsCallbackInterface.OnMouseRightClick;
            }
            m_Wrapper.m_PinBoardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @MouseLeftClick.started += instance.OnMouseLeftClick;
                @MouseLeftClick.performed += instance.OnMouseLeftClick;
                @MouseLeftClick.canceled += instance.OnMouseLeftClick;
                @MouseRightClick.started += instance.OnMouseRightClick;
                @MouseRightClick.performed += instance.OnMouseRightClick;
                @MouseRightClick.canceled += instance.OnMouseRightClick;
            }
        }
    }
    public PinBoardActions @PinBoard => new PinBoardActions(this);
    public interface IPinBoardActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnMouseLeftClick(InputAction.CallbackContext context);
        void OnMouseRightClick(InputAction.CallbackContext context);
    }
}
