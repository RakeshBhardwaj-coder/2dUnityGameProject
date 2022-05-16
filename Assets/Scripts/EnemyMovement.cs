using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject pointB;
    public GameObject pointA;
    public GameObject enemy;
    public float maxSpeed = 3;
    public bool keyHit = false;
    public bool keyHit2 = false;
    public bool keyHit3 = false;
    // Start is called before the first frame update
    void Start()
    {
    enemy.transform.position = pointA.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            if (enemy.transform.position == pointA.transform.position)
        {
            keyHit = true;

        }
        if (keyHit == true)
        {
            keyHit2 = false;
            var change = maxSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, change);
        }
        if (enemy.transform.position == pointB.transform.position)
        {
            keyHit2 = true;
        }
        if (keyHit2 == true)
        {
            keyHit = false;
            var change2 = maxSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, change2);
        }
       }
    }
