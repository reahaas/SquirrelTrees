using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerTouchMovement : MonoBehaviour{
    public float speed;
    public GameObject playerPanel;
    public TextMeshProUGUI textScore;
    public FixedJoystick fixedJoystick;

    public Color color;
    public Rigidbody rb;
    private int score = 0;

    void Start(){
        this.textScore = playerPanel.GetComponentInChildren<TextMeshProUGUI>();
        this.fixedJoystick = playerPanel.GetComponentInChildren<FixedJoystick>();
        SetScoresText();
    }

    void Update(){
        if (Math.Abs(fixedJoystick.Vertical) >= 0.2f || Math.Abs(fixedJoystick.Horizontal) >= 0.2f){
            Vector3 direction = Vector3.forward * fixedJoystick.Vertical + 
                                Vector3.right * fixedJoystick.Horizontal;
            transform.position = transform.position + direction * speed * Time.deltaTime;
            transform.forward = direction;
        }

    }

    private void SetScoresText(){
        textScore.text = "Score: " + this.score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))  // other.CompareTag("Collectable"))
        {
            this.score++;
            SetScoresText();
            Destroy(other.gameObject);
            Debug.Log("Score: " + score);
        }
        
    }
}
