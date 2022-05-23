using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class clampRot : MonoBehaviour
{

    public float h_rotSpeed;
    public float v_rotSpeed;

    public float hInput;
    public float vInput;
    
    public float h_minRot;
    public float h_maxRot;
    public float v_minRot;
    public float v_maxRot;
    
    
    void Update()
    {
        hInput += Input.GetAxis("Mouse X") * -h_rotSpeed;
        hInput = Mathf.Clamp(hInput, h_minRot, h_maxRot);
        vInput += Input.GetAxis("Mouse Y") * v_rotSpeed;
        vInput = Mathf.Clamp(vInput, v_minRot, v_maxRot);
        
        transform.rotation = Quaternion.Euler(vInput, hInput,0);

    }
}
