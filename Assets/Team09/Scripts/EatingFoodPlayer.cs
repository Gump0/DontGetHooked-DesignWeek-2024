using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace team09
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EatingFoodPlayer : MonoBehaviour
    {

        public AudioSource spawnSFX;
        public AudioSource eatSFX;

        

       
        private bool spawned;
        public Animator textanim;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("GameController"))
            {
                Debug.Log("eat");

                FindObjectOfType<ScoreTracking>().currentPlayerScore += 50;
                textanim.Play("Flash");


                if(eatSFX.isPlaying == false)
                {
                    eatSFX.Play();
                }


                
                FindObjectOfType<PlayerSwim>().playerFishMoveSpeed += 0.001f;
                Quaternion newRotation = Quaternion.Euler(0, 0, 0);
                if (spawned == false)
                {
                    GameObject newObject = Instantiate(this.gameObject, new Vector2(Random.Range(-5f, 5.5f), Random.Range(-4.5f, 4)), newRotation);
                    spawned = true;
                }
                Destroy(this.gameObject);

            }
        }

       
    }
}
