using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    public float amplitude = 0.1f;
    public float speed = 2f;
    private float reference_y;
    // Start is called before the first frame update
    void Start()
    {
          this.reference_y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float new_y_value = this.reference_y + amplitude * Mathf.Sin(speed * Time.time);
        Vector3 new_pos = new Vector3(transform.position.x, new_y_value, transform.position.z);
        transform.position = new_pos;
    }

    public void set_reference_y(float new_reference_y){
        this.reference_y = new_reference_y;

    }
}
