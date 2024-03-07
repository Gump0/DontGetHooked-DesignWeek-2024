using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using team99;
using UnityEngine;

//LOGIC THAT IS USED WHENEVER THE PLAYER COLLIDES WITH A HOOK
namespace team09
{

    public class GM : MonoBehaviour
    {
        public bool gameLaunched;
        public bool gameStarted;
        public bool gameEnded;

        public bool alive = true;

        public Animator fishAnimator;
        public bool endAnimationPlayed;

        


        private string gameName = "Player";

        // Start is called before the first frame update
        void Start()
        {
            endAnimationPlayed = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(gameEnded == true && alive == false)
            {
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











    }

}