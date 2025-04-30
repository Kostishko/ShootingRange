using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_sc : MonoBehaviour
{

    public float moveSpeed = 5f; // Movement speed
    public float mouseSensitivity = 100f; // Mouse sensitivity

    private CharacterController characterController;
    private float verticalRotation = 0f; // For looking up and down

    public Transform cameraTransform; // Assign the camera in the Inspector

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limit vertical rotation
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }


}
