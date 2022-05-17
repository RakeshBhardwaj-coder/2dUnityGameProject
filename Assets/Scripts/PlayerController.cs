using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float rateOfAcceleration = .05f;
    public float acceleration;
    public Rigidbody2D rigidBody2D;
    public GameObject uiMenu;
    public GameObject pausedMenu;
    public GameObject gameOverMenu;
    public Animator doorAnimator;



    bool isGetDoor;
    public bool isGameOver;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        uiMenu.SetActive(false);
        pausedMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        isGameOver = false;



    }

    // Update is called once per frame
    void FixedUpdate()
    {



        if (Input.GetKey(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
                pausedMenu.SetActive(true);
            }
        }

        if (isGameOver)
        {
            rigidBody2D.velocity = new Vector2(0f, 0f);

            return;
        }

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
        if (other.tag == "CheckPoint")
        {

            Debug.Log("Level Completed!!!");

            uiMenu.SetActive(true);
            isGameOver = true;


        }else if(other.tag=="Door"){
        doorAnimator.SetBool("playerChecked",true); // if player checked is true door will open.

        }
        else if (other.tag == "Enemy")
        {
            // rigidBody2D.velocity = new Vector2(0f, 0f);
            GameOver();

        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausedMenu.SetActive(true);

    }
    public void Resume()
    {
        Time.timeScale = 1;

    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        isGameOver = true;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {

        Application.Quit(0);
    }
}
