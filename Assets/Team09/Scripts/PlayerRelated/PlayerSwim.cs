using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09{
    public class PlayerSwim : MicrogameInputEvents{
        [SerializeField] private GameObject playerFishObj;
        //Move Stuff
        private Vector2 swimDir; // Direction in which player is pushing stick
        [SerializeField] private float playerFishMoveSpeed; // Player movement speed
        void Update(){
            Swim();
        }
        public void Swim(){
            swimDir = stick.normalized; // (0, 0), (±1, 0), (0, ±1), (±0. 707, ±0.707)

            transform.Translate(swimDir * playerFishMoveSpeed);
            Debug.Log(swimDir);
        }
    }
}

