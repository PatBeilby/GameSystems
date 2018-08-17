using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    // Highly modified version of the Smooth LookAt Script 
    // So you can change the object your looking at by clicking a button 	
    public Transform[] LookObjects;
    public int CamNum;
    public int MaxCam = 1;
    // all of these int's just change the size and position of the GUI button 
    public int x = 1;
    public int y = 1;
    public int b = 1;
    public int c = 1;

    public float damping = 6;
    public bool smooth = true;

    private Transform target;
    
    void Start()
    {
        MaxCam = LookObjects.Length - 1;
    }

    void LateUpdate()
    {
        target = LookObjects[CamNum];

        if (target)
        {
            if (smooth)
            {
                // Look at and dampen the rotation
                var rotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                // Just lookat
                transform.LookAt(target);
            }
        }
    }

    // Use this for initialization
    void CamSwap()
    {
        if (CamNum >= MaxCam)
        {
            CamNum = 0;
        }
        else
        {
            CamNum++;
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10 * x, 70 * y, 50 * b, 30 * c), "Swap"))
        {
            CamSwap();
        }
    }
}