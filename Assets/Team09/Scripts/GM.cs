using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using team09;
using UnityEngine;

//LOGIC THAT IS USED WHENEVER THE PLAYER COLLIDES WITH A HOOK
namespace team09
{

    public class GM : MicrogameEvents
    {
        public bool gameLaunched;
        public bool gameStarted;
        public bool gameEnded;

        public bool canHook;
        public bool alive = true;

        public Animator fishAnimator;
        public bool endAnimationPlayed;

        public AudioSource music;

        private float startTime;
        private string gameName = "Player";

        // Start is called before the first frame update
        void Start()
        {
            alive = true;
            endAnimationPlayed = false;
            canHook = false;
            gameEnded = false;

            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("GameController");
            foreach (GameObject objectWithTag in objectsWithTag)
            {
                SpriteRenderer sr = objectWithTag.GetComponent<PlayerSwim>().sr;
                sr.enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            startTime += Time.deltaTime;
            if(startTime > 5)
            {
                canHook = true;
            }

            if(gameEnded == true && alive == false)
            {
                music.Stop();

                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(gameName);
                foreach (GameObject objectWithTag in objectsWithTag)
                {
                    Destroy(objectWithTag);
                }

                if(endAnimationPlayed == false)
                {
                    fishAnimator.Play("Hooked");
                    endAnimationPlayed=true;
                }



            }else if(gameEnded == true && alive == true)
            {
                
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(gameName);
                foreach (GameObject objectWithTag in objectsWithTag)
                {
                    Destroy(objectWithTag);
                }

                if (endAnimationPlayed == false)
                {
                    fishAnimator.Play("Ending");
                    endAnimationPlayed = true;
                }


            }
        }

        protected override void OnGameStart()
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("GameController");
            foreach(GameObject objectWithTag in objectsWithTag)
            {
                SpriteRenderer sr = objectWithTag.GetComponent<PlayerSwim>().sr;
                sr.enabled = true;
            }
        }









    }

}