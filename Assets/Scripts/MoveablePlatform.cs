using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    public GameObject topToDownObject,leftToRightObject;

    public GameObject downPoint,topPoint;
    public GameObject leftPoint,rightPoint;
    float maxSpeed = 1;
    public bool topKeyHit = false;
    public bool leftKeyHit = false;

    // Start is called before the first frame update
    void Start()
    {
        topToDownObject.transform.position = topPoint.transform.position;
        leftToRightObject.transform.position = leftPoint.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        TopToDown();
        LeftToRight();

    }
    //Moving object from Top to Down.
    void TopToDown()
    {
        if (topToDownObject.transform.position == topPoint.transform.position)
        {
            topKeyHit = true;

        }
        if (topKeyHit == true)
        {
            var change = maxSpeed * Time.deltaTime;
            topToDownObject.transform.position = Vector3.MoveTowards(topToDownObject.transform.position, downPoint.transform.position, change);
        }
        if (topToDownObject.transform.position == downPoint.transform.position)
        {
            topKeyHit = false;
        }
        if (topKeyHit == false)
        {
            var change = maxSpeed * Time.deltaTime;
            topToDownObject.transform.position = Vector3.MoveTowards(topToDownObject.transform.position, topPoint.transform.position, change);
        }
    }
    //Moving Oject form left to right.
     void LeftToRight()
    {
        if (leftToRightObject.transform.position == leftPoint.transform.position)
        {
            leftKeyHit = true;

        }
        if (leftKeyHit == true)
        {
            var change = maxSpeed * Time.deltaTime;
            leftToRightObject.transform.position = Vector3.MoveTowards(leftToRightObject.transform.position, rightPoint.transform.position, change);
        }
        if (leftToRightObject.transform.position == rightPoint.transform.position)
        {
            leftKeyHit = false;
        }
        if (leftKeyHit == false)
        {
            var change = maxSpeed * Time.deltaTime;
            leftToRightObject.transform.position = Vector3.MoveTowards(leftToRightObject.transform.position, leftPoint.transform.position, change);
        }
    }
}
