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
    private float playerSpeed, jumpForce = 70, jumpHeight = 2f;



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

    //Jumping variables
    [Header("Jump")]
    bool isJump;
    bool isFall;
    bool isGround;

    // Wall Jump
   [Header("Wall Jump")]
   public float wallJumpTime = .2f;
   public float wallSlideTime = .2f;
   public float wallDistance = .2f;
   bool isWallSliding = false;
   bool isFaceRight;
   RaycastHit2D wallRaycastCheckHit;
   public LayerMask wallLayer;
   float jumpTime;


    //Health Bar Variables

    public int healthDamage;
    int playerMaxHealth = 100;
    [SerializeField] int playerHealth;
    public HealthBar healthBar;
    BoxCollider2D boxCollider2D;
    public PolygonCollider2D polygonCollider2D;

    public LayerMask platformLayerMask;

    bool moveLeft, moveRight = true;

    float? lastGroundedTime;
    float? jumpBtnPressedTime;

    public float jumpBtnGracePeriod;

    // Start is called before the first frame update
    void Start()
    {

        //boxCollider getting here :-
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
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
        // Debug.Log(gameObject.transform.position.y);
        if (!isGameOver)
        {

            if(!wallRaycastCheckHit){
                Move(10);
            }
            Slide();
            Jump();



            if (Input.GetKey(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    Pause();
                    pausedMenu.SetActive(true);
                }
            }


            //Wall Jump
            if(isFaceRight){
                wallRaycastCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance,0),wallDistance,platformLayerMask);
                Debug.DrawRay(transform.position, new Vector2(wallDistance,0),Color.red);
            }else{
                wallRaycastCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance,0),wallDistance,platformLayerMask);
                Debug.DrawRay(transform.position, new Vector2(-wallDistance,0),Color.red);

            }
            if(wallRaycastCheckHit){
                Move(0);
            }

            if(wallRaycastCheckHit && !IsGrounded()){
                isWallSliding = true;
                Debug.Log("isWallSlideing is ture borm");
                jumpTime = Time.time + wallJumpTime;
                               
            }else if (jumpTime<Time.time){
                isWallSliding  = false;

            }
            if(isWallSliding){
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,Mathf.Clamp(rigidbody2D.velocity.y, wallSlideTime,float.MaxValue));
            }

        }
        else
        {
            Move(0);
            transform.rotation = Quaternion.identity;
        }

    }

    public bool IsGrounded()
    {
        RaycastHit2D rayHitCast2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 1f, platformLayerMask);
        Debug.Log(rayHitCast2D.collider);
        return rayHitCast2D.collider != null;
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

        if (Input.GetKey(KeyCode.D))
        {
            // rigidbody2D.AddForce(Vector2.right * move * Time.deltaTime);
            transform.position += new Vector3(movementSpeed, 0, 0) * Time.fixedDeltaTime;
            isFaceRight = true;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-movementSpeed, 0, 0) * Time.fixedDeltaTime;
            isFaceRight = false;

        }

 
        movementX = Input.GetAxis("Horizontal");

        if (!Mathf.Approximately(0, movementX))
            transform.rotation = movementX > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

        //--------animation parts below----------

        // playerAnimator.SetFloat("playerSpeed", Mathf.Abs(movementX) * movementSpeed);

    }
     void Jump()
    {
          if(Input.GetKeyDown(KeyCode.Space)){
                rigidbody2D.velocity = new Vector2(0,1f) * jumpForce * Time.fixedDeltaTime;
            }

        // else if (gameObject.transform.position.y >= jumpHeight)
        // { // *jumpHeight*
        //     isJump = false;
        //     // playerAnimator.SetBool("isPlayerJump", false);
        //     isFall = true;
        //     // playerAnimator.SetBool("isPlayerFall", true);
        // }
        // else if (isFall)
        // {
        //     // playerAnimator.SetBool("isGrounded", true);
        //     isGround = true;
        //     // playerAnimator.SetBool("isPlayerFall", false);
        // }
    }

    private void Slide()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // playerAnimator.SetBool("isSlide", true);
        }
        else
        {
            // playerAnimator.SetBool("isSlide", false);

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
        // playerAnimator.SetBool("isPlayerHurt", isPlayerHurts);

    }
    public void PlayerHurts(int damage)
    {

        damageSound.Play();

        playerHealth -= damage;
        healthBar.SetHealth(playerHealth);
        // playerAnimator.SetBool("isPlayerHurt", true);
        if (playerHealth <= 0)
        {
            // playerAnimator.SetBool("isPlayerHurt", false);

            // playerAnimator.SetBool("isDead", true);


            isGameOver = true;
            return;
        }

    }
    void DoPlayerHurtAnimFalse()
    {
        // playerAnimator.SetBool("isPlayerHurt", false);

    }
    void DoPlayerDeadAnimFalse()
    {
        // playerAnimator.SetBool("isDead", false);

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
