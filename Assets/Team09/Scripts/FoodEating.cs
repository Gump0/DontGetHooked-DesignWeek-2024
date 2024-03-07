using System.Collections;
using System.Collections.Generic;
using team09;
using UnityEngine;

namespace team09

{
    public class FoodEating : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("GameController") == true)
            {
                FindObjectOfType<FoodSpawner>().foodSpawned = false;
                FindObjectOfType<PlayerSwim>().playerFishMoveSpeed += 0.01f;
                Destroy(this.gameObject);
            }

        }

        public void isEaten()
        {
            FindObjectOfType<FoodSpawner>().foodSpawned = false;
           FindObjectOfType<PlayerSwim>().playerFishMoveSpeed += 0.01f;
            Destroy(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
