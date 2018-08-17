using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //Public variable to store a reference to the player game object
    public bool cameraCollision = true;
    public float camRadius = 2f;
    public LayerMask ignoreLayers;

    private Vector3 originalOffset; //Private variable to store the offset distance between the player and camera
    private Vector3 offset; // Final offset
    private float rayDistance = 1000f;
    

    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        originalOffset = transform.position - target.position;
        rayDistance = originalOffset.magnitude;
    }

    void OnDrawGizmosSelected()
    {
        Ray camRay = new Ray(target.position, -transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(camRay.origin, camRay.origin + camRay.direction * 1000f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, camRadius);
    }

    void FixedUpdate()
    {
        if (cameraCollision)
        {
            Ray camRay = new Ray(target.position, -transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(camRay, camRadius, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))
            {
                offset = -transform.forward * hit.distance;
                return;
            }
        }

        offset = originalOffset;
    }

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = target.position + offset;
    }
}
