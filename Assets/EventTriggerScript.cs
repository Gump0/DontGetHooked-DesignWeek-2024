using team09;
using Unity.VisualScripting;
using UnityEngine;

namespace team99

{

    public class MyScript : MicrogameEvents

    {

        public GM gamemanager;
        public ScoreTracking scoretrack;
        protected override void OnGameStart()
        {

            // Code to execute when the microgame starts



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
            scoretrack.enabled = false;
            gamemanager.gameEnded = true;

        }


        private void Update()
        {
            if(gamemanager.alive == false)
            {
                ReportGameCompletedEarly();
                scoretrack.enabled = false;

            }
        }

    }

}
