using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Health Bar Variables
    
    public int healthDamage;
    int playerMaxHealth = 100;
    int playerHealth;
    public HealthBar healthBar;

    [SerializeField]
    private Animator playerAnimator;
    private float movementX;
    private float movementY;

    public Rigidbody2D rigidBody2D;

    [SerializeField]
    private GameObject levelCompleted, pausedMenu, gameOverMenu;

    [SerializeField] private GameObject saw,spike;
    public Animator doorAnimator;

    [SerializeField]
    private AudioSource damageSound, slidingDoorSound;



    bool isGetDoor;
    public bool isGameOver;
    public bool isPlayerHurts;
    bool isPaused = false;

    bool isJump = true;
    float jumpForce = 5;

    private TakeDamage takeDamage;

    private BoxCollider2D boxCollider2D;
    PolygonCollider2D polygonCollider2D;


    // Start is called before the first frame update
    void Start()
    {

        //boxCollider getting here :-
        // boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        // // polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();
        // polygonCollider2D.enabled = true;
        // boxCollider2D.enabled = true;

        //health bar managing:-
        playerHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);

        // set the palyer health 100 when start the game
        // takeDamage = GameObjet.Find("").GetComponent<TakeDamage>();
        takeDamage = saw.GetComponent<TakeDamage>();
        playerHealth = playerMaxHealth;

        levelCompleted.SetActive(false);
        pausedMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        isPlayerHurts = false;
        isGameOver = false;
        // StartCoroutine(waitSomeTime());
    }

    // Update is called once per frame

    //     IEnumerator waitSomeTime() {
    //      yield return new WaitForSeconds(3);
    //    }
    void Update()
    {
        if (!isGameOver)
        {

            Move(5);
            Slide();
            // Jump(5);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
                playerAnimator.SetBool("isPlayerJump", true);
                isJump = false;
                playerAnimator.SetBool("isPlayerFall", false);

                // StartCoroutine(waitSomeTime());

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
        else if (other.CompareTag("Enemy"))
        {
            // rigidBody2D.velocity = new Vector2(0f, 0f);
            // isPlayerHurts = true;
            // PlayerHurts(20, true);
            // playerAnimator.SetBool("isPlayerHurt",true); 
            // PlayerHurts(20);
            // isPlayerHurts = false;

            if (playerHealth <= 0)
            {
                isPlayerHurts = false;
                playerAnimator.SetBool("isDead", true);
                playerAnimator.SetBool("isPlayerHurt", false);
            }
            // StartCoroutine(waitSomeTime());

        }
        else
        {
            isPlayerHurts = false;
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
        playerAnimator.SetBool("isPlayerHurt", false);

    }
    public void PlayerHurts(int isHurt)
    {

        // TakeDamage(GiveDamage());
        playerHealth -= takeDamage.damage;
        healthBar.SetHealth(playerHealth);
        damageSound.Play();
        if(isHurt == 0){
        playerAnimator.SetBool("isPlayerHurt", false);


        }
        else{
            playerAnimator.SetBool("isPlayerHurt",true);
        }
        // isPlayerHurts = true;
        if (playerHealth <= 0)
        {
            isGameOver = true;
        }

        if (!isPlayerHurts)
        {

            // playerAnimator.SetBool("isPlayerHurt", isHurt);
            // isPlayerHurts = false;

        }
        else
        {
            // playerAnimator.SetBool("isPlayerHurt", isHurt);
        }



    }
    // enemy.TakeDamage(player.GiveDamage()); This is the Concept.

    int GiveDamage()
    {

        return healthDamage;
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
        boxCollider2D.enabled = true;
        polygonCollider2D.enabled = true;

        PlayerHurts(1);

    }
    public void DisableInvincible()
    {
        boxCollider2D.enabled = false;
        polygonCollider2D.enabled = false;

        PlayerHurts(0);




    }


}
