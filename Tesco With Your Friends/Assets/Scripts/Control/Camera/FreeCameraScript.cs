using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 rotation = Vector2.zero;
    public float speed = 3f;
    public float Cameraspeed = 0.03f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            transform.eulerAngles = (Vector2)rotation * speed;
        }
       
      
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.forward * Cameraspeed * Input.GetAxis("Vertical"));
        }


        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * Cameraspeed * Input.GetAxis("Horizontal"));
        }
    }
}
