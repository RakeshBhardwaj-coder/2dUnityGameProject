using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    PlayerController playerController;
    public GameObject pointB;
    public GameObject pointA;
    public GameObject player;
    public float maxSpeed = 3;
    public bool keyHit = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = pointA.transform.position;
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position == pointA.transform.position)
        {
            keyHit = true;

        }
        if (keyHit == true)
        {
            // keyHit2 = false;
            var change = maxSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, change);
        }
        if (gameObject.transform.position == pointB.transform.position)
        {
            // keyHit2 = true;
            keyHit = false;
        }
        if (keyHit == false)
        {
            // keyHit = false;
            var change2 = maxSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, change2);
        }

        // if(playerController.isGameOver){
        //     transform.position = new Vector3(0,player.transform.position.y,0);
        //     }
    }
}
