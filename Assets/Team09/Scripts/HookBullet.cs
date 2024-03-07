using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    public class HookBullet : Bullet
    {
        public override void Initialize(float speed)
        {
            base.Initialize(speed);
            transform.Rotate(0, 0, 90);
        }

        protected override void Move()
        {
            rb.MovePosition(rb.position + (Vector2)transform.up * -speed * Time.deltaTime);
        }
    }
}
