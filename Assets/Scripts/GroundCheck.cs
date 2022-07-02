using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
   
    [SerializeField]private LayerMask plateformLayerMask;
    public bool isGrounded;
   private void OnTriggerStay2D(Collider2D collider){
       isGrounded = collider!=null && (((1 << collider.gameObject.layer) & plateformLayerMask ) != 0);

   }
   private void OnTriggerExit2D(Collider2D collider){
       isGrounded = false;
   }
}
