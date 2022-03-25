using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.Touch;

namespace AG
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Config.")]
        public float forceMagnitude;
        public float maxVelocity;
        public float rotationSpeed;
        
        [Header("Refs.")]
        [ReadOnlyInspector] public Camera mainCamera;
        [ReadOnlyInspector] public Rigidbody p_rb;

        [Header("Decision Values.")]
        [ReadOnlyInspector] public float fixedDelta;
        [ReadOnlyInspector] public Vector3 curTouchWorldPos;

        [Header("Status.")]
        [ReadOnlyInspector] public Vector2 playerViewPos;
        [ReadOnlyInspector] public Vector3 moveDirection;

        #region Privates.
        Vector3 vector3Zero = new Vector3(0, 0, 0);
        Transform mTransform;
        #endregion

        #region Callbacks.
        private void Start()
        {
            mainCamera = Camera.main;
            mTransform = transform;

            p_rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            GetMoveDirection();
            KeepPlayerOnScreen();
            RotateToFaceVelocity();
        }

        private void FixedUpdate()
        {
            UpdateFixedDelta();
            MovePlayerByForce();
        }
        #endregion

        #region Tick.
        void GetMoveDirection()
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                curTouchWorldPos = mainCamera.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());

                /// Move away from the user's touch
                moveDirection = mTransform.position - curTouchWorldPos;
                moveDirection.z = 0;
                moveDirection.Normalize();
            }
            else
            {
                moveDirection = vector3Zero;
            }
        }

        void KeepPlayerOnScreen()
        {
            Vector3 newPosition = mTransform.position;

            /// Get Player Viewport pos
            playerViewPos = mainCamera.WorldToViewportPoint(mTransform.position);

            /// Wrap Player inside Screen
            if (playerViewPos.x < 0)
            {
                newPosition.x = -newPosition.x - 0.1f;
            }
            else if (playerViewPos.x > 1)
            {
                newPosition.x = -newPosition.x + 0.1f;
            }

            if (playerViewPos.y < 0)
            {
                newPosition.y = -newPosition.y - 0.1f;
            }
            else if (playerViewPos.y > 1)
            {
                newPosition.y = -newPosition.y + 0.1f;
            }

            /// Set it as the new transform
            mTransform.position = newPosition;
        }

        void RotateToFaceVelocity()
        {
            if (p_rb.velocity != vector3Zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(p_rb.velocity, Vector3.back);
                mTransform.rotation = Quaternion.Slerp(mTransform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }
        #endregion

        #region Fixed Tick.
        void UpdateFixedDelta()
        {
            fixedDelta = Time.fixedDeltaTime;
        }

        void MovePlayerByForce()
        {
            if (moveDirection != vector3Zero)
            {
                p_rb.AddForce(moveDirection * forceMagnitude, ForceMode.Force);
                p_rb.velocity = Vector3.ClampMagnitude(p_rb.velocity, maxVelocity);
            }
        }
        #endregion
    }
}