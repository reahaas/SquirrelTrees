using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using System;

public class PlayerTouchMovement : MonoBehaviour{
    public float speed;
    public FixedJoystick fixedJoystick;
    public Color color;
    public Rigidbody rb;
    private int score = 0;

 
    void Update(){
        if (Math.Abs(fixedJoystick.Vertical) >= 0.2f || Math.Abs(fixedJoystick.Horizontal) >= 0.2f){
            Vector3 direction = Vector3.forward * fixedJoystick.Vertical + 
                                Vector3.right * fixedJoystick.Horizontal;
            transform.position = transform.position + direction * speed * Time.deltaTime;
            transform.forward = direction;
        }

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))  // other.CompareTag("Collectable"))
        {
            this.score++;
            Destroy(other.gameObject);
            Debug.Log("Score: " + score);
        }
        
    }
}
