using System.Collections;
using System.Collections.Generic;
using team09;
using team99;
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
                Debug.Log("eat");
                StartCoroutine("IsEaten");
            }

        }

        

        

        // Update is called once per frame
        void Update()
        {

        }
    }
}
