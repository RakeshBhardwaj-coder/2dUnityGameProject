using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private float movementX;
    private float movementY;
    public float speed;
    float rateOfAcceleration = .05f;
    public float acceleration;
    public Rigidbody2D rigidBody2D;

    [SerializeField]
    private GameObject levelCompleted, pausedMenu, gameOverMenu;
    public Animator doorAnimator;

    public AudioSource damageSound;



    bool isGetDoor;
    public bool isGameOver;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        levelCompleted.SetActive(false);
        pausedMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        isGameOver = false;
        damageSound = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Move(movementX,movementY,5);
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



        if (Input.GetKey("space"))
        {
            rigidBody2D.velocity = new Vector2(0f,5f);

        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CheckPoint")
        {

            Debug.Log("Level Completed!!!");

            levelCompleted.SetActive(true);
            isGameOver = true;


        }
        else if (other.tag == "Door")
        {
            doorAnimator.SetBool("playerChecked", true); // if player checked is true door will open.

        }
        else if (other.tag == "Enemy")
        {
            // rigidBody2D.velocity = new Vector2(0f, 0f);
            GameOver();

        }
    }
     private void Rotate(float rotate, float rotationSpeed)
    {

        // rigidBody2D.velocity = new Vector2(0f,rotate * rotationSpeed);
        transform.Rotate(0, 0, -rotate * rotationSpeed);
        // Rotaion in 3d
        // Vector3 vector = new Vector3(0f, rotate * rotationSpeed, 0f);
        // Quternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        // rigidBody3D.MoveRotation(rigidBody3D.rotation * deltaRotation);
    }

    public void Move(float movementX, float movementY, float movementSpeed)
    {
        movementX = Input.GetAxis("Horizontal");


        movementY = Input.GetAxis("Vertical");

transform.position += new Vector3(movementX,movementY,0) * Time.deltaTime * movementSpeed;

if(!Mathf.Approximately(0,movementX))
    transform.rotation = movementX>0 ? Quaternion.Euler(0,0,0) : Quaternion.Euler(0,180,0);
        // if(movementX!=0){
        //     rigidBody2D.velocity = new Vector2(movementX*movementSpeed,0);
        // }
        // else if(movementY!=0){
        //     rigidBody2D.velocity = new Vector2(0, movementY*movementSpeed);
        // }
  
  

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
        damageSound.Play();
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
