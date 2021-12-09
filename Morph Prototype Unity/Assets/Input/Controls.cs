// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""8818c64b-5281-4a27-b209-75a02d6862f6"",
            ""actions"": [
                {
                    ""name"": ""LimbWeaponLight"",
                    ""type"": ""Button"",
                    ""id"": ""a18d460f-0190-4782-ab8d-e33c15e38a55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""LimbWeaponHeavy"",
                    ""type"": ""Button"",
                    ""id"": ""826ab441-3610-4644-84e8-74605e456f10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""TailWeaponLight"",
                    ""type"": ""Button"",
                    ""id"": ""d8810b6c-b12a-4e2d-8c4e-ad330b666585"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""TailWeaponHeavy"",
                    ""type"": ""Button"",
                    ""id"": ""803bf0a7-fa27-4d29-a19b-7a6875aaede3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""MouthWeaponLight"",
                    ""type"": ""Button"",
                    ""id"": ""d680de1d-ba18-4602-8deb-ad476e1f6cf7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""MouthWeaponHeavy"",
                    ""type"": ""Button"",
                    ""id"": ""5d774c7e-f119-42ed-8227-6b1e16b5d79a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""c474d31b-e2f3-44bb-9cbe-d82c0f9500de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Stealth"",
                    ""type"": ""Button"",
                    ""id"": ""d5f5885c-ae37-4491-ae68-86e801a59263"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Intimidation"",
                    ""type"": ""Button"",
                    ""id"": ""a0753cff-c89d-499d-b838-9c48dbe5d5ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mobility"",
                    ""type"": ""Button"",
                    ""id"": ""cdce540a-0eb5-4306-a251-47f6602cc187"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6384cc5f-73a4-499b-be08-8d5aab2f118e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LimbWeaponLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cbdcb72-3319-4465-8ee8-e15e05db89e3"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LimbWeaponLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88251a03-a231-4369-8769-54ab9d455524"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LimbWeaponHeavy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab53a7fc-cdee-4fc9-93d3-0e83fa6d6ece"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LimbWeaponHeavy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6e9403d-b0f3-43f5-983b-53a862ea9cbb"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TailWeaponLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b661f4e4-02d2-4eb1-a04b-2e6e615f6731"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TailWeaponLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16663329-930f-40e8-a4b3-e0722c7c6e96"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TailWeaponHeavy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed75a0ce-e66d-4013-98d3-3f0f16dbfc0a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TailWeaponHeavy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c10d46f-8092-4091-ab84-14bb2585be9b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouthWeaponLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58232f2f-3483-48b3-8e0c-edff9fd59ecf"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouthWeaponHeavy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8ee35fe-18d0-4c1c-8d45-ffa252350553"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3eda000a-2b81-495e-a076-1cd587dea311"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stealth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ce3cbf1-4ad8-4d1a-9fd5-8787795ce53d"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stealth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7af9834-55cf-4a34-9d41-e4e9326f3445"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Intimidation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f5663de-eebf-4344-b71d-791d7c4a3098"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Intimidation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8f47510-3452-4cd2-97e4-b2dcf0600a4a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mobility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a34efd29-118c-43ec-9a32-5c5374b59a0e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mobility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_LimbWeaponLight = m_Gameplay.FindAction("LimbWeaponLight", throwIfNotFound: true);
        m_Gameplay_LimbWeaponHeavy = m_Gameplay.FindAction("LimbWeaponHeavy", throwIfNotFound: true);
        m_Gameplay_TailWeaponLight = m_Gameplay.FindAction("TailWeaponLight", throwIfNotFound: true);
        m_Gameplay_TailWeaponHeavy = m_Gameplay.FindAction("TailWeaponHeavy", throwIfNotFound: true);
        m_Gameplay_MouthWeaponLight = m_Gameplay.FindAction("MouthWeaponLight", throwIfNotFound: true);
        m_Gameplay_MouthWeaponHeavy = m_Gameplay.FindAction("MouthWeaponHeavy", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Stealth = m_Gameplay.FindAction("Stealth", throwIfNotFound: true);
        m_Gameplay_Intimidation = m_Gameplay.FindAction("Intimidation", throwIfNotFound: true);
        m_Gameplay_Mobility = m_Gameplay.FindAction("Mobility", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_LimbWeaponLight;
    private readonly InputAction m_Gameplay_LimbWeaponHeavy;
    private readonly InputAction m_Gameplay_TailWeaponLight;
    private readonly InputAction m_Gameplay_TailWeaponHeavy;
    private readonly InputAction m_Gameplay_MouthWeaponLight;
    private readonly InputAction m_Gameplay_MouthWeaponHeavy;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Stealth;
    private readonly InputAction m_Gameplay_Intimidation;
    private readonly InputAction m_Gameplay_Mobility;
    public struct GameplayActions
    {
        private @Controls m_Wrapper;
        public GameplayActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LimbWeaponLight => m_Wrapper.m_Gameplay_LimbWeaponLight;
        public InputAction @LimbWeaponHeavy => m_Wrapper.m_Gameplay_LimbWeaponHeavy;
        public InputAction @TailWeaponLight => m_Wrapper.m_Gameplay_TailWeaponLight;
        public InputAction @TailWeaponHeavy => m_Wrapper.m_Gameplay_TailWeaponHeavy;
        public InputAction @MouthWeaponLight => m_Wrapper.m_Gameplay_MouthWeaponLight;
        public InputAction @MouthWeaponHeavy => m_Wrapper.m_Gameplay_MouthWeaponHeavy;
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Stealth => m_Wrapper.m_Gameplay_Stealth;
        public InputAction @Intimidation => m_Wrapper.m_Gameplay_Intimidation;
        public InputAction @Mobility => m_Wrapper.m_Gameplay_Mobility;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @LimbWeaponLight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbWeaponLight;
                @LimbWeaponLight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbWeaponLight;
                @LimbWeaponLight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbWeaponLight;
                @LimbWeaponHeavy.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbWeaponHeavy;
                @LimbWeaponHeavy.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbWeaponHeavy;
                @LimbWeaponHeavy.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbWeaponHeavy;
                @TailWeaponLight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailWeaponLight;
                @TailWeaponLight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailWeaponLight;
                @TailWeaponLight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailWeaponLight;
                @TailWeaponHeavy.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailWeaponHeavy;
                @TailWeaponHeavy.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailWeaponHeavy;
                @TailWeaponHeavy.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailWeaponHeavy;
                @MouthWeaponLight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthWeaponLight;
                @MouthWeaponLight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthWeaponLight;
                @MouthWeaponLight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthWeaponLight;
                @MouthWeaponHeavy.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthWeaponHeavy;
                @MouthWeaponHeavy.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthWeaponHeavy;
                @MouthWeaponHeavy.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthWeaponHeavy;
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Stealth.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStealth;
                @Stealth.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStealth;
                @Stealth.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStealth;
                @Intimidation.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnIntimidation;
                @Intimidation.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnIntimidation;
                @Intimidation.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnIntimidation;
                @Mobility.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMobility;
                @Mobility.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMobility;
                @Mobility.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMobility;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LimbWeaponLight.started += instance.OnLimbWeaponLight;
                @LimbWeaponLight.performed += instance.OnLimbWeaponLight;
                @LimbWeaponLight.canceled += instance.OnLimbWeaponLight;
                @LimbWeaponHeavy.started += instance.OnLimbWeaponHeavy;
                @LimbWeaponHeavy.performed += instance.OnLimbWeaponHeavy;
                @LimbWeaponHeavy.canceled += instance.OnLimbWeaponHeavy;
                @TailWeaponLight.started += instance.OnTailWeaponLight;
                @TailWeaponLight.performed += instance.OnTailWeaponLight;
                @TailWeaponLight.canceled += instance.OnTailWeaponLight;
                @TailWeaponHeavy.started += instance.OnTailWeaponHeavy;
                @TailWeaponHeavy.performed += instance.OnTailWeaponHeavy;
                @TailWeaponHeavy.canceled += instance.OnTailWeaponHeavy;
                @MouthWeaponLight.started += instance.OnMouthWeaponLight;
                @MouthWeaponLight.performed += instance.OnMouthWeaponLight;
                @MouthWeaponLight.canceled += instance.OnMouthWeaponLight;
                @MouthWeaponHeavy.started += instance.OnMouthWeaponHeavy;
                @MouthWeaponHeavy.performed += instance.OnMouthWeaponHeavy;
                @MouthWeaponHeavy.canceled += instance.OnMouthWeaponHeavy;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Stealth.started += instance.OnStealth;
                @Stealth.performed += instance.OnStealth;
                @Stealth.canceled += instance.OnStealth;
                @Intimidation.started += instance.OnIntimidation;
                @Intimidation.performed += instance.OnIntimidation;
                @Intimidation.canceled += instance.OnIntimidation;
                @Mobility.started += instance.OnMobility;
                @Mobility.performed += instance.OnMobility;
                @Mobility.canceled += instance.OnMobility;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnLimbWeaponLight(InputAction.CallbackContext context);
        void OnLimbWeaponHeavy(InputAction.CallbackContext context);
        void OnTailWeaponLight(InputAction.CallbackContext context);
        void OnTailWeaponHeavy(InputAction.CallbackContext context);
        void OnMouthWeaponLight(InputAction.CallbackContext context);
        void OnMouthWeaponHeavy(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnStealth(InputAction.CallbackContext context);
        void OnIntimidation(InputAction.CallbackContext context);
        void OnMobility(InputAction.CallbackContext context);
    }
}
