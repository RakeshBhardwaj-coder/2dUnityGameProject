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

    [SerializeField]
    private float playerSpeed;

    private Rigidbody2D rigidbody2D;

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

    //Health Bar Variables

    public int healthDamage;
    int playerMaxHealth = 100;
    [SerializeField] int playerHealth;
    public HealthBar healthBar;
    public BoxCollider2D boxCollider2D;
    public PolygonCollider2D polygonCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        //boxCollider getting here :-
        // boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        // polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();
        // polygonCollider2D.enabled = true;
        // boxCollider2D.enabled = true;
        //health bar managing:-
        playerHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
        // set the palyer health 100 when start the game
        // takeDamage = GameObjet.Find("").GetComponent<TakeDamage>();
        // takeDamage = saw.GetComponent<TakeDamage>();

        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();


        levelCompleted.SetActive(false);
        pausedMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        isPlayerHurts = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isGameOver)
        {

            Move(5);
            Slide();
            // Jump(5);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W))
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                playerAnimator.SetBool("isPlayerJump", true);
                isJump = false;
                playerAnimator.SetBool("isPlayerFall", false);

                // isJump = false;
                // playerAnimator.SetBool("isPlayerFall",isJump);



            }
            else if (!isJump)
            {
                playerAnimator.SetBool("isPlayerJump", false);


                //     isJump = true;
                playerAnimator.SetBool("isPlayerFall", true);
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

            PlayerHurts(20);
           
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
        // movementY = Input.GetAxis("Vertical");
        transform.position += new Vector3(playerSpeed,0,0) * Time.fixedDeltaTime * movementSpeed;
         if (!Mathf.Approximately(0, movementX))
         transform.position += movementX>0 ? new Vector3(playerSpeed, 0, 0) * Time.fixedDeltaTime * movementSpeed : new Vector3(-playerSpeed, 0, 0) * Time.fixedDeltaTime  * movementSpeed;

        if (!Mathf.Approximately(0, movementX))
            transform.rotation = movementX > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

        //--------animation parts below----------

        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(movementX ) * movementSpeed);

    }

    private void Slide()
    {
        if (Input.GetKey(KeyCode.S))
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
    public void PlayerHurts(int damage)
    {

        damageSound.Play();

        playerHealth -= damage;
        healthBar.SetHealth(playerHealth);
        playerAnimator.SetBool("isPlayerHurt",true);
         if (playerHealth <= 0)
            {   
                playerAnimator.SetBool("isPlayerHurt", false);
                
                playerAnimator.SetBool("isDead", true);
                

                isGameOver = true;
                return;
            }

    }
    void DoPlayerHurtAnimFalse()
    {
        playerAnimator.SetBool("isPlayerHurt", false);

    }
    void DoPlayerDeadAnimFalse()
    {
        playerAnimator.SetBool("isDead", false);

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Distroy()
    {
        Destroy(gameObject);
    }

    public void Quit()
    {
        Application.Quit(0);
    }

    public void EnableInvincible()
    {
        // boxCollider2D.enabled = true;
        // polygonCollider2D.enabled = true;
        PlayerHurts(1);
    }
    public void DisableInvincible()
    {
        // boxCollider2D.enabled = false;
        // polygonCollider2D.enabled = false;
        PlayerHurts(0);
    }
}
