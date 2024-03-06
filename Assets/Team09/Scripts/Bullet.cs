using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    public class Bullet : MonoBehaviour
    {
        private float speed;

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

        public virtual void Initialize(float speed)
        {
            this.speed = speed;
        }

        protected virtual void Move()
        {
            rb.MovePosition(rb.position + (Vector2)transform.right * speed * Time.deltaTime);
        }
    }
}
