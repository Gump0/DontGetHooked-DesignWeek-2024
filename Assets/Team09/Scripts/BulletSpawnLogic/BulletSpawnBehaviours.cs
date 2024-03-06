using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    public enum BehaviourType
    {
        None,
        Aimed,
        Spiral,
        RandomRotation,
        RandomPosition
    }

    //Bullet Spawn Behaviours class
    //Holds a variety of spawn behaviour presets
    public static class BulletSpawnBehaviours
    {
        //Aimed behaviour
        //Sets the direction of the pattern to the direction to the given target
        public static void aimed(BulletPattern pattern, Vector3 spawnPoint, float speed, Vector3 target, params object[] extraArgs)
        {
            Vector3 targetDirection = target - spawnPoint;
            float targetAngle = Vector2.SignedAngle(Vector2.right, targetDirection);

            pattern.run(speed, spawnPoint, targetAngle, extraArgs);
        }

        //Spiral behaviour
        //Changes the direction of the pattern by the given interval each time a bullet is spawned
        public static void spiral(BulletPattern pattern, Vector3 spawnPoint, float speed, float intervalAngle, float[] counter, params object[] extraArgs)
        {
            float direction = intervalAngle * counter[0];
            //Yes this is fucked up and gross it's a counter in spirit
            counter[0] += Time.deltaTime / pattern.getInterval();

            pattern.run(speed, spawnPoint, direction, extraArgs);
        }

        //Random rotation behaviour
        //Randomly changes the direction of the pattern within a given range
        public static void randomRotate(BulletPattern pattern, Vector3 spawnPoint, float speed, float min, float max, params object[] extraArgs)
        {
            float direction = UnityEngine.Random.Range(min, max);

            pattern.run(speed, spawnPoint, direction, extraArgs);
        }

        //Random rotation behaviour
        //Randomly changes the direction of the pattern using a given random function
        public static void randomRotate(BulletPattern pattern, Vector3 spawnPoint, float speed, Func<float> randomFunc, params object[] extraArgs)
        {
            float direction = randomFunc();

            pattern.run(speed, spawnPoint, direction, extraArgs);
        }

        //Random position on line behaviour
        //Randomizes the position of the pattern within a given range on a set line perpendicular the the direction of the bullets
        public static void randomPositionLine(BulletPattern pattern, Vector3 spawnPoint, float speed, float direction, float min, float max, params object[] extraArgs)
        {
            float offset = UnityEngine.Random.Range(min, max);
            Vector3 pos = spawnPoint + new Vector3(Mathf.Sin(direction * Mathf.Deg2Rad), Mathf.Cos(direction * Mathf.Deg2Rad)) * offset;

            pattern.run(speed, pos, direction, extraArgs);
            
        }

        //Random position on line behaviour
        //Randomizes the position of the pattern using a given random function on a set line perpendicular the the direction of the bullets
        public static void randomPositionLine(BulletPattern pattern, Vector3 spawnPoint, float speed, float direction, Func<float> randomFunc, params object[] extraArgs)
        {
            float offset = randomFunc();
            Vector3 pos = spawnPoint + new Vector3(Mathf.Sin(direction * Mathf.Deg2Rad), Mathf.Cos(direction * Mathf.Deg2Rad)) * offset;

            pattern.run(speed, pos, direction, extraArgs);
        }
    }
}
