using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float rateOfAcceleration = .05f;
    public float acceleration;
    public Rigidbody2D rigidBody2D;
    public GameObject uiMenu;
    public GameObject pausedMenu;

    bool isGetDoor;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        uiMenu.SetActive(false);
       pausedMenu.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    //     if (isPaused == false)
    //      {
    //          Time.timeScale = 1;
    //      }
         
    //  else 
    //      {
    //          Time.timeScale = 0;
    //      }
         
         
    //  if (Input.GetKey(KeyCode.P))
    //      {
    //          if (isPaused == true)
    //          {
    //              isPaused = false;
    //              pausedMenu.SetActive(true);
    //          }
             
    //      else
    //          {
    //              isPaused = true;
    //              pausedMenu.SetActive(false);

    //          }
    //      }

        if(isGetDoor)
            return;
        
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
            uiMenu.SetActive(true);
            isGetDoor = true;

        }
    }
   void Pause(){
       Time.timeScale = 0;
       pausedMenu.SetActive(true);

   }
    void Resume(){
       Time.timeScale = 1;

   }
}
