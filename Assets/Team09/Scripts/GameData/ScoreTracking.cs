using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Needed Library

namespace team09{
    public class ScoreTracking : MicrogameInputEvents{
        [SerializeField] private TMP_Text scoreDisplay, scoreText; // Reference to UI element
        private float currentPlayerScore, scoreIncreaseInterval = 10f; // SCORE TRACKING STUFF
        [SerializeField] private bool hasGameStarted = false;
        void Start(){
            GameObject scoreDisplayObject = GameObject.Find("ScoreNumber");
            scoreDisplay = scoreDisplayObject.GetComponent<TMP_Text>();
            if(scoreDisplay == null){
                Debug.LogWarning("Score UI Object Reference Is Null");
                scoreDisplay.enabled = false;
            }
        }
        void Update(){
            if(hasGameStarted){
                DisplayScore();
            }
        }
        void DisplayScore(){
            //Calculate Score
            float increment = scoreIncreaseInterval * Time.deltaTime;
            currentPlayerScore += increment;
            int displayCurrentScore = Mathf.CeilToInt(currentPlayerScore); // Had to use this instead of mathf round since TMP doesn't seem to like it :P

            //Display Text
            scoreDisplay.text = displayCurrentScore.ToString();
        }
        protected override void OnGameStart(){
            hasGameStarted = true;
            scoreDisplay.enabled = true;
            scoreText.enabled = true;
        }
    }
}

