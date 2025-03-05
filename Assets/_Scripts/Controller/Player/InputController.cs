using System;
using Health;
using Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputController : MonoBehaviour
    {
        private InputSystem_Actions inputActions;
        private Vector2 moveInput;
        private bool jumpPressed = false;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform spriteTransform;
        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] public float jumpForce = 7f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private HealthController health;
        [SerializeField] private InventoryController inventoryController;


        private void Awake()
        {
            inputActions = new InputSystem_Actions();
            EnablePlayerControls();
        }

        private void OnEnable()
        {
            //Movement
            inputActions.Player.Move.performed += Move;
            inputActions.Player.Move.canceled += Idle;
            inputActions.Player.Jump.performed += Jump;
            //Debug Health
            inputActions.Player.Next.performed += TakeDamage;
            inputActions.Player.Previous.performed += Heal;
            // Inventory
            inputActions.Player.Inventory.performed += ToggleInventory;

            EnablePlayerControls();
        }

        private void OnDisable()
        {
            //Movement
            inputActions.Player.Move.performed -= Move;
            inputActions.Player.Move.canceled -= Idle;
            inputActions.Player.Jump.performed -= Jump;
            //Debug Health
            inputActions.Player.Next.performed -= TakeDamage;
            inputActions.Player.Previous.performed -= Heal;
            // Inventory
            inputActions.Player.Inventory.performed -= ToggleInventory;
            DisablePlayerControls();
        }

        private void ToggleInventory(InputAction.CallbackContext context)
        {
            inventoryController.OnInventoryToggle(context);
        }

        public void EnablePlayerControls() => inputActions.Player.Enable();
        public void DisablePlayerControls() => inputActions.Player.Disable();

        private void Heal(InputAction.CallbackContext context)
        {
            health.addHealth(4);
        }

        private void TakeDamage(InputAction.CallbackContext context)
        {
            health.subHealth(4);
        }


        private void Idle(InputAction.CallbackContext context)
        {
            moveInput = Vector2.zero;
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if(isGrounded())
                jumpPressed = true;
        }

        private void Move(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>().normalized;
        }

        private void Update()
        {
            applyMovement();
            handleAnimation();

        }

        private void applyMovement()
        {
            Vector3 movement = new Vector3(moveInput.x, 0, 0) * (moveSpeed * Time.deltaTime);
            transform.position += movement;

            if (moveInput.x > 0)
                spriteTransform.localScale = new Vector3(1, 1, 1);
            else if (moveInput.x < 0)
                spriteTransform.localScale = new Vector3(-1, 1, 1);
        }

        private void handleAnimation()
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
            animator.SetBool("isJumping", jumpPressed);
            animator.SetBool("isGrounded", isGrounded());
        }

        private bool isGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void FixedUpdate()
        {
            if(jumpPressed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
                jumpPressed = false;
            }
        }
    }
}