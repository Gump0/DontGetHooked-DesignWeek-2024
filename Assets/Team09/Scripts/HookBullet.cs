using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    public class HookBullet : Bullet
    {
        protected override void Move()
        {
            rb.MovePosition(rb.position + (Vector2)transform.up * -speed * Time.deltaTime);
        }
    }
}
