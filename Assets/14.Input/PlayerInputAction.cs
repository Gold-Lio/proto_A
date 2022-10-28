// GENERATED AUTOMATICALLY FROM 'Assets/14.Input/PlayerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""6c4018eb-4eb0-4ac7-96e7-23620f2f4ce5"",
            ""actions"": [
                {
                    ""name"": ""InventoryOnOff"",
                    ""type"": ""Button"",
                    ""id"": ""11bb55ba-2cd1-4275-9d9c-c6f10fa168ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ItemDrop"",
                    ""type"": ""Button"",
                    ""id"": ""4b098a0e-aa84-4fa2-95ad-5e536b73dd40"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ItemPickUp"",
                    ""type"": ""Button"",
                    ""id"": ""41bd47c2-f46e-4802-8360-8ef4313a9d38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2c65db23-10d6-4ade-99e7-4a35277829cc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""ItemDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38b5d1b2-1044-4793-9656-c78a96de31ae"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""InventoryOnOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""598c6d2c-a820-427d-ae84-1845a315346b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""ItemPickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""K&M"",
            ""bindingGroup"": ""K&M"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_InventoryOnOff = m_UI.FindAction("InventoryOnOff", throwIfNotFound: true);
        m_UI_ItemDrop = m_UI.FindAction("ItemDrop", throwIfNotFound: true);
        m_UI_ItemPickUp = m_UI.FindAction("ItemPickUp", throwIfNotFound: true);
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
    private readonly InputAction m_UI_InventoryOnOff;
    private readonly InputAction m_UI_ItemDrop;
    private readonly InputAction m_UI_ItemPickUp;
    public struct UIActions
    {
        private @PlayerInputAction m_Wrapper;
        public UIActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @InventoryOnOff => m_Wrapper.m_UI_InventoryOnOff;
        public InputAction @ItemDrop => m_Wrapper.m_UI_ItemDrop;
        public InputAction @ItemPickUp => m_Wrapper.m_UI_ItemPickUp;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @InventoryOnOff.started -= m_Wrapper.m_UIActionsCallbackInterface.OnInventoryOnOff;
                @InventoryOnOff.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnInventoryOnOff;
                @InventoryOnOff.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnInventoryOnOff;
                @ItemDrop.started -= m_Wrapper.m_UIActionsCallbackInterface.OnItemDrop;
                @ItemDrop.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnItemDrop;
                @ItemDrop.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnItemDrop;
                @ItemPickUp.started -= m_Wrapper.m_UIActionsCallbackInterface.OnItemPickUp;
                @ItemPickUp.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnItemPickUp;
                @ItemPickUp.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnItemPickUp;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InventoryOnOff.started += instance.OnInventoryOnOff;
                @InventoryOnOff.performed += instance.OnInventoryOnOff;
                @InventoryOnOff.canceled += instance.OnInventoryOnOff;
                @ItemDrop.started += instance.OnItemDrop;
                @ItemDrop.performed += instance.OnItemDrop;
                @ItemDrop.canceled += instance.OnItemDrop;
                @ItemPickUp.started += instance.OnItemPickUp;
                @ItemPickUp.performed += instance.OnItemPickUp;
                @ItemPickUp.canceled += instance.OnItemPickUp;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KMSchemeIndex = -1;
    public InputControlScheme KMScheme
    {
        get
        {
            if (m_KMSchemeIndex == -1) m_KMSchemeIndex = asset.FindControlSchemeIndex("K&M");
            return asset.controlSchemes[m_KMSchemeIndex];
        }
    }
    public interface IUIActions
    {
        void OnInventoryOnOff(InputAction.CallbackContext context);
        void OnItemDrop(InputAction.CallbackContext context);
        void OnItemPickUp(InputAction.CallbackContext context);
    }
}
