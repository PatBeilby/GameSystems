using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    public Rigidbody rigid;
    public float rayDistance = 1f;
    public LayerMask ignoreLayers;

    private bool isGrounded = false;

    void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);
    }

    bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(groundRay, out hit, rayDistance, ~ignoreLayers))
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal") * moveSpeed;
        float inputV = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 moveDir = new Vector3(inputH, 0f, inputV);

        Vector3 camEuler = Camera.main.transform.eulerAngles;
        moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;


        Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            force.y = jumpHeight;
        }

        rigid.velocity = force;

        if (moveDir.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }
}
