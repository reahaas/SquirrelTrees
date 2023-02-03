using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
public class PlayerTouchMovement : MonoBehaviour{
    public float speed;
    public FixedJoystick fixedJoystick;
    public Color color;
    public Rigidbody rb;

    void Update(){
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + 
                            Vector3.right * fixedJoystick.Horizontal;
        
        transform.position = transform.position + direction * speed * Time.deltaTime;
        transform.forward = direction;
    }
}
