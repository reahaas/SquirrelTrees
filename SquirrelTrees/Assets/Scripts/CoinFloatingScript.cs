using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFloatingScript : MonoBehaviour
{
    public float floatStrength = 1;
    public float speed = 0.5f;
    public float rotationSpeed = 20f;
    private Vector3 startPosition;
    


    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float y = startPosition.y + floatStrength * Mathf.Sin(Time.time * speed)/4;
        this.transform.position = new Vector3(startPosition.x, y, startPosition.z);

        this.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}