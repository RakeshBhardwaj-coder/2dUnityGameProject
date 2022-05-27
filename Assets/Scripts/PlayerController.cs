using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;
    private float movementX;
    private float movementY;

    public Rigidbody2D rigidBody2D;

    [SerializeField]
    private GameObject levelCompleted, pausedMenu, gameOverMenu;
    public Animator doorAnimator;

    [SerializeField]
    private AudioSource damageSound, slidingDoorSound;



    bool isGetDoor;
    public bool isGameOver;
    public bool isPlayerHurts;
    bool isPaused = false;

    bool isJump = true;
                 float jumpForce = 5;

    // Start is called before the first frame update
    void Start()
    {
        levelCompleted.SetActive(false);
        pausedMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        isPlayerHurts = false;
        StartCoroutine(waitSomeTime());
    }

    // Update is called once per frame

    IEnumerator waitSomeTime() {
     yield return new WaitForSeconds(3);
   }
    void Update()
    {
        if (!isPlayerHurts)
        {

            Move(5);
            Slide();
            // Jump(5);

             if(Input.GetKeyDown(KeyCode.Space)){
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
            playerAnimator.SetBool("isPlayerJump",true);
            isJump = false;
            playerAnimator.SetBool("isPlayerFall",false);
            StartCoroutine(waitSomeTime());
            Debug.Log("kud diya lawda"); 

            // isJump = false;
            // playerAnimator.SetBool("isPlayerFall",isJump);



            }
            else if(!isJump){
            playerAnimator.SetBool("isPlayerJump",false);

                
            //     isJump = true;
            playerAnimator.SetBool("isPlayerFall",true);
            // isJump = false;
            // playerAnimator.SetBool("isPlayerJump",isJump);

                
            }
            
            if (Input.GetKey(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    Pause();
                    pausedMenu.SetActive(true);
                }
            }

            if (Input.GetKey("space"))
            {

            }
        }
        else
        {
            Move(0);
            transform.rotation = Quaternion.identity;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CheckPoint")
        {

            Debug.Log("Level Completed!!!");

            levelCompleted.SetActive(true);
            // isPlayerHurts = true;


        }
        else if (other.tag == "Door")
        {
            doorAnimator.SetBool("DoorColorYellow", true);
            doorAnimator.SetBool("DoorOpen", true); // if player checked is true door will open.

            //-------Audio--------
            slidingDoorSound.Play();
        }
        else if (other.tag == "Enemy")
        {
            // rigidBody2D.velocity = new Vector2(0f, 0f);
            PlayerHurts();
            // StartCoroutine(waitSomeTime());

        }
    }
    private void Rotate(float rotate, float rotationSpeed)
    {

        transform.Rotate(0, 0, -rotate * rotationSpeed);
        // Rotaion in 3d
        // Vector3 vector = new Vector3(0f, rotate * rotationSpeed, 0f);
        // Quternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        // rigidBody3D.MoveRotation(rigidBody3D.rotation * deltaRotation);
    }

    private void Move(float movementSpeed)
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");

        transform.position += new Vector3(movementX, movementY, 0) * Time.deltaTime * movementSpeed;

        if (!Mathf.Approximately(0, movementX))
            transform.rotation = movementX > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

        //--------animation parts below----------

        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(movementX + movementY) * movementSpeed);

    }
    private void Jump(float jumpForce)
    {
    //    if(Input.GetKeyDown(KeyCode.Space)){
    //         rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
    //         playerAnimator.SetBool("isPlayerJump",true);

    //         }
            // playerAnimator.SetBool("isPlayerFall",true);


    }

    private void Slide()
    {
        if (Input.GetKey(KeyCode.C))
        {
            playerAnimator.SetBool("isSlide", true);
        }
        else
        {
            playerAnimator.SetBool("isSlide", false);

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
        isGameOver = true;
        damageSound.Play();
        gameOverMenu.SetActive(true);
        playerAnimator.SetBool("isPlayerHurt", isPlayerHurts);

    }
    public void PlayerHurts()
    {

        isPlayerHurts = true;
        damageSound.Play();

        playerAnimator.SetBool("isPlayerHurt", isPlayerHurts);

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
