using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace team09

{
    public class FoodSpawner : MonoBehaviour
    {
        public bool gameStarted;
        public bool foodSpawned;

        public GameObject food;

        // Start is called before the first frame update
        void Start()
        {
            foodSpawned = false;
            gameStarted = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(gameStarted == true && foodSpawned == false)
            {
                spawnFood();
            }
        }

        public void spawnFood()
        {
            
            Quaternion newRotation = Quaternion.Euler(0, 0, 0);
            GameObject newObject = Instantiate(food, new Vector2(Random.Range(-5f, 5.5f), Random.Range(-4.5f, 4)), newRotation);

            foodSpawned = true;
        }


    }
}

