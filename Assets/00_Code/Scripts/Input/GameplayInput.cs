// Author: Ignacio María Muñoz Márquez

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MahjonggDimensions
{
    public class GameplayInput : MonoBehaviour
    {
        public delegate void ShootInputEvent();

        public event ShootInputEvent OnShootInput;
        private class PointerInfo
        {
            public int ID;
            public float TimePressed;
            public Vector2 LastScreenPosition;
            public float TotalMovement;
        }

        public float HorizontalInput { get { return _horizontalInput; } }
        public Vector2 PointerScreenPosition { get; private set; }

        [SerializeField]
        private float _timeToSelect;
        [SerializeField]
        private float _movementToDiscardSelect;
        [SerializeField]
        private float _multiplierMouseDrag;
        [SerializeField]
        private float _multiplierTouchDrag;
        [SerializeField]
        private InputAction _moveCameraAction;

        private float _horizontalInput;

        private List<PointerInfo> _pointers = new List<PointerInfo>();

#if UNITY_ANDROID || UNITY_IOS
        private void Awake()
        {
            UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
        }
#endif

        private void OnEnable()
        {
            _moveCameraAction.Enable();
        }

        private void OnDisable()
        {
            _moveCameraAction.Disable();
        }

        private void Update()
        {
            _horizontalInput = 0f;
            PointerScreenPosition = Vector2.zero;

// Using IF and IF instead of IF and ELIF to be able to use both the Unity Mobile Simulator and the normal Game View.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

            _horizontalInput = -_moveCameraAction.ReadValue<float>();
            PointerScreenPosition = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                PointerInfo pointerInfo = new PointerInfo();
                pointerInfo.ID = 0;
                pointerInfo.TimePressed = Time.time;
                pointerInfo.LastScreenPosition = Mouse.current.position.ReadValue();
                pointerInfo.TotalMovement = 0f;

                _pointers.Add(pointerInfo);
            }
            else if (Mouse.current.leftButton.isPressed)
            {
                PointerInfo pointerInfo = _pointers[0];

                Vector2 movement = Mouse.current.position.ReadValue() - pointerInfo.LastScreenPosition;
                movement *= _multiplierMouseDrag;

                pointerInfo.TotalMovement += movement.magnitude;
                pointerInfo.LastScreenPosition = Mouse.current.position.ReadValue();

                float timeSincePressed = Time.time - pointerInfo.TimePressed;

                if (timeSincePressed > _timeToSelect || pointerInfo.TotalMovement > _movementToDiscardSelect)
                {
                    _horizontalInput += movement.x;
                }
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                PointerInfo pointerInfo = _pointers[0];

                float timeSincePressed = Time.time - pointerInfo.TimePressed;
                float totalMovement = pointerInfo.TotalMovement;

                if (timeSincePressed <= _timeToSelect && totalMovement <= _movementToDiscardSelect)
                {
                    if (OnShootInput != null)
                    {
                        OnShootInput.Invoke();
                    }
                }

                _pointers.Remove(pointerInfo);
            }
#endif

// Using IF and IF instead of IF and ELIF to be able to use both the Unity Mobile Simulator and the normal Game View.
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS

            if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
            {
                PointerScreenPosition = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].screenPosition;

                for (int i = 0; i < UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count; ++i)
                {
                    UnityEngine.InputSystem.EnhancedTouch.Touch touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[i];

                    PointerInfo pointerInfo = _pointers.FirstOrDefault(p => p.ID == touch.touchId);

                    if (pointerInfo != null)
                    {
                        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended || touch.phase == UnityEngine.InputSystem.TouchPhase.Canceled)
                        {
                            float timeSincePressed = Time.time - pointerInfo.TimePressed;
                            float totalMovement = pointerInfo.TotalMovement;

                            if (timeSincePressed <= _timeToSelect && totalMovement <= _movementToDiscardSelect)
                            {
                                if (OnShootInput != null)
                                {
                                    OnShootInput.Invoke();
                                }
                            }

                            _pointers.Remove(pointerInfo);
                        }
                        else
                        {
                            Vector2 movement = touch.screenPosition - pointerInfo.LastScreenPosition;
                            movement *= _multiplierTouchDrag;

                            pointerInfo.TotalMovement += movement.magnitude;
                            pointerInfo.LastScreenPosition = touch.screenPosition;

                            float timeSincePressed = Time.time - pointerInfo.TimePressed;

                            if (timeSincePressed > _timeToSelect || pointerInfo.TotalMovement > _movementToDiscardSelect)
                            {
                                _horizontalInput += movement.x;
                            }
                        }
                    }
                    else
                    {
                        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
                        {
                            pointerInfo = new PointerInfo();
                            pointerInfo.ID = touch.touchId;
                            pointerInfo.TimePressed = Time.time;
                            pointerInfo.LastScreenPosition = touch.screenPosition;
                            pointerInfo.TotalMovement = 0f;

                            _pointers.Add(pointerInfo);
                        }
                    }
                }
            }
#endif
        }
    }
}
