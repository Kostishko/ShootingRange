using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetInfoReciever_sc : MonoBehaviour
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
    public target_sc currentTarget; // Reference to the target script

    void Start()
    {

        //raycast initialise
        detectRay = new Ray(cam.transform.position, cam.transform.forward);
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
            if (detectHit.collider.GetComponentInParent<target_sc>() != null)
            {
                currentTarget = detectHit.collider.GetComponentInParent<target_sc>();
                if(currentTarget!=null)
                {
                    isInteracting = true;
                }
                             
            }
            else
            {
                isInteracting = false;
            }

        }
    }
}
