using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_sc : MonoBehaviour
{

    public float moveSpeed = 5f; // Movement speed
    public float mouseSensitivity = 1000f; // Mouse sensitivity

    private CharacterController characterController;
    private float verticalRotation = 0f; // For looking up and down

    public Transform cameraTransform; // Assign the camera in the Inspector

    //raycast
    // Perform a raycast from the camera's position forward
    private Ray detectRay;
    RaycastHit detectHit;
    public float detectRayRange; // Range for detecting items
    public LayerMask mask; // Layer mask for raycast filtering


    void Start()
    {
        characterController = GetComponent<CharacterController>();        

        //raycast initialise
        detectRay = new Ray(cameraTransform.position, cameraTransform.forward);
    }

    void Update()
    {
        if (gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay ||
            gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting)
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

            //ray update
            detectRay.origin = cameraTransform.position;
            detectRay.direction = cameraTransform.forward;
            if (Physics.Raycast(detectRay, out detectHit, detectRayRange, mask))
            {
                if(detectHit.collider.GetComponent<interactable_sc>()!=null)
                {
                   

                }

            }
        }
        
    }


}
