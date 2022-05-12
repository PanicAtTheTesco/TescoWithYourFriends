// GENERATED AUTOMATICALLY FROM 'Assets/Shahil World/UI_Controller.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UI_Controller : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UI_Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UI_Controller"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""e5b9517e-504e-4cfe-8b7a-2a54ae6ff6ee"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""32c46f98-2e54-490c-89be-38a0f6d0919f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeaderBoard"",
                    ""type"": ""Button"",
                    ""id"": ""5bb8cf08-a0f4-444a-86e1-9eac15e455e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SliderActivate"",
                    ""type"": ""Button"",
                    ""id"": ""419d2826-e1c0-4edb-b4c7-a7aab9cc0223"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec050aa5-37ee-458f-ad99-9cee7060ea8c"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""UI"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b806ee31-66f3-42aa-947c-ee6efa209f78"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""UI"",
                    ""action"": ""LeaderBoard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d76d5b7-61d8-4cba-bff8-ead282ff0089"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SliderActivate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""UI"",
            ""bindingGroup"": ""UI"",
            ""devices"": []
        }
    ]
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Pause = m_UI.FindAction("Pause", throwIfNotFound: true);
        m_UI_LeaderBoard = m_UI.FindAction("LeaderBoard", throwIfNotFound: true);
        m_UI_SliderActivate = m_UI.FindAction("SliderActivate", throwIfNotFound: true);
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

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Pause;
    private readonly InputAction m_UI_LeaderBoard;
    private readonly InputAction m_UI_SliderActivate;
    public struct UIActions
    {
        private @UI_Controller m_Wrapper;
        public UIActions(@UI_Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_UI_Pause;
        public InputAction @LeaderBoard => m_Wrapper.m_UI_LeaderBoard;
        public InputAction @SliderActivate => m_Wrapper.m_UI_SliderActivate;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @LeaderBoard.started -= m_Wrapper.m_UIActionsCallbackInterface.OnLeaderBoard;
                @LeaderBoard.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnLeaderBoard;
                @LeaderBoard.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnLeaderBoard;
                @SliderActivate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSliderActivate;
                @SliderActivate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSliderActivate;
                @SliderActivate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSliderActivate;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @LeaderBoard.started += instance.OnLeaderBoard;
                @LeaderBoard.performed += instance.OnLeaderBoard;
                @LeaderBoard.canceled += instance.OnLeaderBoard;
                @SliderActivate.started += instance.OnSliderActivate;
                @SliderActivate.performed += instance.OnSliderActivate;
                @SliderActivate.canceled += instance.OnSliderActivate;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_UISchemeIndex = -1;
    public InputControlScheme UIScheme
    {
        get
        {
            if (m_UISchemeIndex == -1) m_UISchemeIndex = asset.FindControlSchemeIndex("UI");
            return asset.controlSchemes[m_UISchemeIndex];
        }
    }
    public interface IUIActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnLeaderBoard(InputAction.CallbackContext context);
        void OnSliderActivate(InputAction.CallbackContext context);
    }
}
