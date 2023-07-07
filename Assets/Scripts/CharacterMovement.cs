using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpSpeed = 5f;
    public float gravity = 0.5f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isJumping;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the move direction based on the input and the character's transform
        moveDirection = new Vector3(moveX, 0f, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        // Check if the character is on the ground and should jump
        if (characterController.isGrounded)
        {
            isJumping = false;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
                isJumping = true;
            }
        }

        // Apply gravity to the move direction
        moveDirection.y -= gravity;

        // Move the character controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Reset vertical velocity if not jumping
        if (!isJumping)
            moveDirection.y = 0f;
    }
}
