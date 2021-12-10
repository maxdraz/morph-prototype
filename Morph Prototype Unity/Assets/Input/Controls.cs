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
                    ""name"": ""LimbLightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""a18d460f-0190-4782-ab8d-e33c15e38a55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""LimbHeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""826ab441-3610-4644-84e8-74605e456f10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.5)""
                },
                {
                    ""name"": ""TailLightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""d8810b6c-b12a-4e2d-8c4e-ad330b666585"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""TailHeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""803bf0a7-fa27-4d29-a19b-7a6875aaede3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""MouthLightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""d680de1d-ba18-4602-8deb-ad476e1f6cf7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""MouthHeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""5d774c7e-f119-42ed-8227-6b1e16b5d79a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c474d31b-e2f3-44bb-9cbe-d82c0f9500de"",
                    ""expectedControlType"": ""Vector2"",
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
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""3866b583-b167-45a7-9820-273f73c50bc2"",
                    ""expectedControlType"": ""Vector2"",
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
                    ""action"": ""LimbLightAttack"",
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
                    ""action"": ""LimbLightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b2209fa-df7f-47f8-9935-b68f91c1137f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LimbLightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88251a03-a231-4369-8769-54ab9d455524"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LimbHeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab53a7fc-cdee-4fc9-93d3-0e83fa6d6ece"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LimbHeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86a1b3ab-1e7f-44b1-95d5-1ef8334a1d00"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LimbHeavyAttack"",
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
                    ""action"": ""TailLightAttack"",
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
                    ""action"": ""TailLightAttack"",
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
                    ""action"": ""TailHeavyAttack"",
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
                    ""action"": ""TailHeavyAttack"",
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
                    ""action"": ""MouthLightAttack"",
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
                    ""action"": ""MouthHeavyAttack"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""e4d0f91f-6ad1-4b5b-8a2e-70d232f901c3"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.1,y=0.1)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74371166-e62c-4db1-9d3b-c21878d2fecd"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=2,y=2)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae961383-80fd-4d24-9c77-8e5989e6cc2a"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8afecc14-a5f4-4283-8a43-27f1f95e42ce"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b2736b8c-2352-459a-817a-dc22352058ec"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ddf5d152-d1fc-4122-9883-9c19f4eaa4b0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cd1dc4f0-692c-487f-bf02-799e3fddac13"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dd2dccbf-b30d-43bc-bda7-4c8077a6d96a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_LimbLightAttack = m_Gameplay.FindAction("LimbLightAttack", throwIfNotFound: true);
        m_Gameplay_LimbHeavyAttack = m_Gameplay.FindAction("LimbHeavyAttack", throwIfNotFound: true);
        m_Gameplay_TailLightAttack = m_Gameplay.FindAction("TailLightAttack", throwIfNotFound: true);
        m_Gameplay_TailHeavyAttack = m_Gameplay.FindAction("TailHeavyAttack", throwIfNotFound: true);
        m_Gameplay_MouthLightAttack = m_Gameplay.FindAction("MouthLightAttack", throwIfNotFound: true);
        m_Gameplay_MouthHeavyAttack = m_Gameplay.FindAction("MouthHeavyAttack", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Stealth = m_Gameplay.FindAction("Stealth", throwIfNotFound: true);
        m_Gameplay_Intimidation = m_Gameplay.FindAction("Intimidation", throwIfNotFound: true);
        m_Gameplay_Mobility = m_Gameplay.FindAction("Mobility", throwIfNotFound: true);
        m_Gameplay_Look = m_Gameplay.FindAction("Look", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_LimbLightAttack;
    private readonly InputAction m_Gameplay_LimbHeavyAttack;
    private readonly InputAction m_Gameplay_TailLightAttack;
    private readonly InputAction m_Gameplay_TailHeavyAttack;
    private readonly InputAction m_Gameplay_MouthLightAttack;
    private readonly InputAction m_Gameplay_MouthHeavyAttack;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Stealth;
    private readonly InputAction m_Gameplay_Intimidation;
    private readonly InputAction m_Gameplay_Mobility;
    private readonly InputAction m_Gameplay_Look;
    public struct GameplayActions
    {
        private @Controls m_Wrapper;
        public GameplayActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LimbLightAttack => m_Wrapper.m_Gameplay_LimbLightAttack;
        public InputAction @LimbHeavyAttack => m_Wrapper.m_Gameplay_LimbHeavyAttack;
        public InputAction @TailLightAttack => m_Wrapper.m_Gameplay_TailLightAttack;
        public InputAction @TailHeavyAttack => m_Wrapper.m_Gameplay_TailHeavyAttack;
        public InputAction @MouthLightAttack => m_Wrapper.m_Gameplay_MouthLightAttack;
        public InputAction @MouthHeavyAttack => m_Wrapper.m_Gameplay_MouthHeavyAttack;
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Stealth => m_Wrapper.m_Gameplay_Stealth;
        public InputAction @Intimidation => m_Wrapper.m_Gameplay_Intimidation;
        public InputAction @Mobility => m_Wrapper.m_Gameplay_Mobility;
        public InputAction @Look => m_Wrapper.m_Gameplay_Look;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @LimbLightAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbLightAttack;
                @LimbLightAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbLightAttack;
                @LimbLightAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbLightAttack;
                @LimbHeavyAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbHeavyAttack;
                @LimbHeavyAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbHeavyAttack;
                @LimbHeavyAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLimbHeavyAttack;
                @TailLightAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailLightAttack;
                @TailLightAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailLightAttack;
                @TailLightAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailLightAttack;
                @TailHeavyAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailHeavyAttack;
                @TailHeavyAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailHeavyAttack;
                @TailHeavyAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTailHeavyAttack;
                @MouthLightAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthLightAttack;
                @MouthLightAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthLightAttack;
                @MouthLightAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthLightAttack;
                @MouthHeavyAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthHeavyAttack;
                @MouthHeavyAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthHeavyAttack;
                @MouthHeavyAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouthHeavyAttack;
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
                @Look.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LimbLightAttack.started += instance.OnLimbLightAttack;
                @LimbLightAttack.performed += instance.OnLimbLightAttack;
                @LimbLightAttack.canceled += instance.OnLimbLightAttack;
                @LimbHeavyAttack.started += instance.OnLimbHeavyAttack;
                @LimbHeavyAttack.performed += instance.OnLimbHeavyAttack;
                @LimbHeavyAttack.canceled += instance.OnLimbHeavyAttack;
                @TailLightAttack.started += instance.OnTailLightAttack;
                @TailLightAttack.performed += instance.OnTailLightAttack;
                @TailLightAttack.canceled += instance.OnTailLightAttack;
                @TailHeavyAttack.started += instance.OnTailHeavyAttack;
                @TailHeavyAttack.performed += instance.OnTailHeavyAttack;
                @TailHeavyAttack.canceled += instance.OnTailHeavyAttack;
                @MouthLightAttack.started += instance.OnMouthLightAttack;
                @MouthLightAttack.performed += instance.OnMouthLightAttack;
                @MouthLightAttack.canceled += instance.OnMouthLightAttack;
                @MouthHeavyAttack.started += instance.OnMouthHeavyAttack;
                @MouthHeavyAttack.performed += instance.OnMouthHeavyAttack;
                @MouthHeavyAttack.canceled += instance.OnMouthHeavyAttack;
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
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnLimbLightAttack(InputAction.CallbackContext context);
        void OnLimbHeavyAttack(InputAction.CallbackContext context);
        void OnTailLightAttack(InputAction.CallbackContext context);
        void OnTailHeavyAttack(InputAction.CallbackContext context);
        void OnMouthLightAttack(InputAction.CallbackContext context);
        void OnMouthHeavyAttack(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnStealth(InputAction.CallbackContext context);
        void OnIntimidation(InputAction.CallbackContext context);
        void OnMobility(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}
