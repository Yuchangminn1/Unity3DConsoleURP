namespace SF_FPSController
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]

    public class SF_FPSController : MonoBehaviour
    {
        public float walkingSpeed = 7.5f;
        public float runningSpeed = 11.5f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;
        public Camera playerCamera;
        public Camera playerAimCamera;

        public GameObject flashlightHolder;
        public float lookSpeed = 2.0f;
        float lookXLimit = 20.0f;

        Animator animator;


        CharacterController characterController;
        Vector3 moveDirection = Vector3.zero;
        float rotationX = 0;

        [HideInInspector]
        public bool canMove = true;

        public bool isJump = false;

        float jumptime = 0.1f;
        float jumptime22 = 0.1f;

        float curSpeedX;
        float curSpeedY;

        bool isAim = false;
        RifleAimingIK rifleAimingIK;
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            rifleAimingIK = GetComponentInChildren<RifleAimingIK>();
        }

        void Update()
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            if (isAim)
            {
                curSpeedX = canMove ? walkingSpeed * Input.GetAxis("Vertical") : 0;
                curSpeedY = canMove ? walkingSpeed * Input.GetAxis("Horizontal") : 0;
            }
            else
            {
                curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
                curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            }
            
            animator.SetFloat("InputX", curSpeedY);
            animator.SetFloat("InputY", curSpeedX);

            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);



            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                isJump = true;
                animator.SetBool("Jump", isJump);
                moveDirection.y = jumpSpeed;
                jumptime22 = Time.time;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
            if (Input.GetMouseButtonDown(1))
            {
                isAim = !isAim;
                animator.SetBool("OnAim", isAim);
                if (rifleAimingIK)
                {
                    rifleAimingIK.isRifleIK = isAim;
                }
                playerAimCamera.enabled = isAim;

            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                flashlightHolder.SetActive(!flashlightHolder.activeSelf);
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            else
            {
                if (isJump && jumptime22 + jumptime < Time.time)
                {
                    isJump = false;
                    animator.SetBool("Jump", isJump);

                }
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {

                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                playerAimCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
                animator.SetFloat("MouseY", rotationX);
            }
        }
    }
}
