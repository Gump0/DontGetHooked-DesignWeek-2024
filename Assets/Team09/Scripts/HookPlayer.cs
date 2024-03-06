using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LOGIC THAT IS USED WHENEVER THE PLAYER COLLIDES WITH A HOOK
namespace team09{
    [RequireComponent(typeof(SphereCollider))]
         public class HookPlayer : MonoBehaviour{
        void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag ("Player")){
                StartCoroutine("CollideRoutine");
            }
        }

        private IEnumerator CollideRoutine(){
            return null;
        }
    }   
}

