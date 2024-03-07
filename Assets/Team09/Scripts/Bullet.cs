using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    public class Bullet : MonoBehaviour
    {
        //Movement speed of the bullet
        private float speed;

        //Reference to the bullet's rigidbody2d
        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

        }

        private void FixedUpdate()
        {
            Move();
        }

        //Initializes bullet with the given speed
        //Called when the bullet is instantiated
        //Can be overwritten by children but only takes a speed parameter so honestly prolly not worth messing with it
        public virtual void Initialize(float speed)
        {
            this.speed = speed;
        }

        //Moves the bullet
        //Called in FixedUpdate()
        //Can be overwritten with different movement logic for unique bullet types
        protected virtual void Move()
        {
            rb.MovePosition(rb.position + (Vector2)transform.right * speed * Time.deltaTime);
        }
    }
}
