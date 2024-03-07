using System;
using team09;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace team09

{

    public class EventTriggerScript : MicrogameEvents

    {

        public GM gamemanager;
        public ScoreTracking scoretrack;
        public FoodSpawner foodspawn;
        public GameObject eventManager;

        private bool gameEndCalled = false;
        protected override void OnGameStart()
        {

            // Code to execute when the microgame starts
            if(gamemanager.music.isPlaying == false)
            {
                gamemanager.music.Play();

            }
            foodspawn.gameStarted = true;
            gamemanager.gameEnded = false;


        }


        protected override void OnFifteenSecondsLeft()
        {

            // Code to execute when there are 15 seconds left in the game

        }

        protected override void OnTenSecondsLeft()
        {

            // Code to execute when there are 10 seconds left in the game

        }

        protected override void OnFiveSecondsLeft()
        {

            // Code to execute when there are 5 seconds left in the game

        }

        protected override void OnTimesUp()
        {
            scoretrack.hasGameStarted = false;
            gamemanager.gameEnded = true;

        }


        public void Update()
        {
            if(gamemanager.alive == false && !gameEndCalled)
            {
                gamemanager.gameEnded = true;
                scoretrack.hasGameStarted = false;
                OnTimesUp();
                gameEnd();
            }
        }

        public void gameEnd()
        {
            ReportGameCompletedEarly();
            gameEndCalled = true;

        }


    }

}
