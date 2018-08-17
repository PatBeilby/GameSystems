using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public bool cameraCollision = true;
    public float camRadius = .4f;
    public LayerMask ignoreLayers;

    private Vector3 originalOffset; //Private variable to store the offset distance between the player and camera
    private float distance; // Final offset
    private float rayDistance = 1000f;

    private float x = 0.0f;
    private float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        originalOffset = transform.position - target.position;
        rayDistance = originalOffset.magnitude;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void FixedUpdate()
    {
        if (cameraCollision)
        {
            Ray camRay = new Ray(target.position, -transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(camRay, camRadius, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))
            {
                distance = hit.distance;
                return;
            }
        }

        distance = originalOffset.magnitude;
    }

    void Update()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;
        }
    }

    void LateUpdate()
    {
        if (target)
        {   
            transform.position = target.position + -transform.forward * distance;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}