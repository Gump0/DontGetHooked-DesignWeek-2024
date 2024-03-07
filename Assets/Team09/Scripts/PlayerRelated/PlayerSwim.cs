using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace team09{
    public class PlayerSwim : MicrogameInputEvents{
        //Move Stuff
        private Vector2 swimDir; // Direction in which player is pushing stick
        public SpriteRenderer sr;
       

        [SerializeField] public float playerFishMoveSpeed; // Player movement speed
        void Update(){
            Swim();

            if (swimDir == new Vector2(1,0) || swimDir == new Vector2(1, 1) || swimDir == new Vector2(1, -1))
            {
                sr.flipX = false;
                
            }

            if (swimDir == new Vector2(-1, 0) || swimDir == new Vector2(-1, -1) || swimDir == new Vector2(-1, 1))
            {
                sr.flipX = true;
                
            }
        }
        public void Swim(){
            swimDir = stick.normalized; // (0, 0), (±1, 0), (0, ±1), (±0. 707, ±0.707)
            transform.Translate(swimDir * playerFishMoveSpeed);
            

            if(transform.position.x < -6)
            {
                transform.position = new Vector2(-6, transform.position.y);
            }
            if (transform.position.x > 6)
            {
                transform.position = new Vector2(6, transform.position.y);
            }
            if (transform.position.y < -5)
            {
                transform.position = new Vector2(transform.position.x, -5);
            }
            if (transform.position.y > 5)
            {
                transform.position = new Vector2(transform.position.x, 5);
            }

        }
    }
}

