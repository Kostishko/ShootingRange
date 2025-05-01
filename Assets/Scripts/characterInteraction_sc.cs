using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterInteraction_sc : MonoBehaviour
{
    public Camera cam;
    //raycast
    // Perform a raycast from the camera's position forward
    private Ray detectRay;
    public RaycastHit detectHit;
    public float detectRayRange; // Range for detecting items
    [SerializeField]
    private LayerMask mask; // Layer mask for raycast filtering
    // Start is called before the first frame update

    public bool isInteracting = false; // Flag to check if the player is interacting with an object
    void Start()
    {
        
                           //raycast initialise
        detectRay = new Ray(cam.transform.position, cam.transform.forward );
    }

    // Update is called once per frame
    void Update()
    {

        //ray update
        detectRay.origin = cam.transform.position;
        detectRay.direction = cam.transform.forward;
        Debug.DrawRay(detectRay.origin, detectRay.direction * detectRayRange, Color.red);

        if (Physics.Raycast(detectRay, out detectHit, detectRayRange, mask))
        {
            if (detectHit.collider.GetComponent<interactable_sc>() != null)
            {
                isInteracting = true;
                Debug.Log("Hit: " + detectHit.collider.GetComponent<interactable_sc>().promtMessage);
            }

        }
        else
        {
            isInteracting = false;
        }

    }
}
