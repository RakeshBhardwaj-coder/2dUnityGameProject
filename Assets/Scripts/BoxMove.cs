using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
     public CharacterController2D characterController2D;
     float horizontalMove = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal")*200;
    }
      void FixedUpdate(){
        characterController2D.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
