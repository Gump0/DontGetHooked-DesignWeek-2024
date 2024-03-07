using System.Collections;
using System.Collections.Generic;
using team99;
using UnityEngine;

//LOGIC THAT IS USED WHENEVER THE PLAYER COLLIDES WITH A HOOK
namespace team09{
    [RequireComponent(typeof(CircleCollider2D))]
         public class HookPlayer : MonoBehaviour{

        public GM gameManager;


        void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag ("Player")){
                if (gameManager.canHook == true)
                {
                    StartCoroutine("CollideRoutine");
                }
            }
            
        }

        private IEnumerator CollideRoutine(){
            


                GetComponent<PlayerSwim>().enabled = false;

                gameManager.alive = false;
                gameManager.gameEnded = true;
                return null;
            
        }
    }   
}

