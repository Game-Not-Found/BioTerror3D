using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float walkSpeed = 5f;
    private float sprintSpeed = 10f;
    private float jumpForce = 5f;
    private float gravityModifier = 1f;
    private bool isOnGround = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    void FixedUpdate()
    {
        // Keyboard-based movement
        float moveX = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
        float moveZ = (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        playerRb.linearVelocity = moveDirection * currentSpeed + new Vector3(0, playerRb.linearVelocity.y, 0);

        // Jump Logic
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ground Check
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
