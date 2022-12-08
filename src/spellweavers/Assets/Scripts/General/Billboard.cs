using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    protected Transform cam;

    protected void Awake()
    {
        cam = Camera.main.transform;
    }


    protected void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
