using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWithGun : MonoBehaviour{

public float bulletSpeed;
PlayerController playerController;
[SerializeField]
private GameObject player;



void Start(){

    playerController = player.GetComponent<PlayerController>();

}
void Update(){
    RotateTowardsTarget();
}



 private void RotateTowardsTarget()
 {
     var offset = 90f;
     Vector2 direction = player.transform.position - transform.position;
     direction.Normalize();
     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
     transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
 }
}
