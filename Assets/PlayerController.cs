using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float rateOfAcceleration = .05f;
    public float acceleration;
    public Rigidbody2D rigidBody2D;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            acceleration *= acceleration * rateOfAcceleration;
            speed += acceleration * Time.deltaTime;
            rigidBody2D.velocity = new Vector2(speed, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            acceleration *= acceleration * rateOfAcceleration;

            speed += acceleration * Time.deltaTime;
            rigidBody2D.velocity = new Vector2(-speed, 0f);

        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            acceleration *= acceleration * rateOfAcceleration;

            speed += acceleration * Time.deltaTime;
            rigidBody2D.velocity = new Vector2(0f, -speed);

        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            acceleration *= acceleration * rateOfAcceleration;

            speed += acceleration * Time.deltaTime;
            rigidBody2D.velocity = new Vector2(0f, speed);

        }
        else if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
        {
            rigidBody2D.velocity = new Vector2(0f, 0f);

        }
        if (Input.GetKey("space"))
        {
            rigidBody2D.velocity = new Vector2(0f, 0f);

        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "door")
        {
            Debug.Log("Level Completed!!!");
        }
    }
}
