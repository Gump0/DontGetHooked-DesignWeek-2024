using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    //Bullet Event class
    //Stores all necessary data related to a timed bullet event
    [Serializable]
    public class BulletEvent
    {
        //Enums for pattern and behaviour type
        public PatternType patternType;
        public BehaviourType behaviourType;

        //Universal pattern parameters
        public Transform spawnPoint;
        public float bulletSpeed;
        public float direction;
        public float interval;
        public GameObject bulletPrefab;

        //Extra arguments for specific pattern types
        public int extraArgInt;
        public float extraArgFloat1;
        public float extraArgFloat2;
        public float extraArgFloat3;

        //Behaviour parameters
        public Transform target;
        public float intervalAngle;
        private float[] counter;
        public float variance;
        public List<Transform> spawnPoints;

        //Timing parameters
        public float startTime;
        public float duration;

        //The pattern and behaviour the event will use
        private BulletPattern pattern;
        private Action behaviour;

        //Initialize method
        //Sets the pattern and behaviour of the event using its parameters
        //Should be called only once before the event is called
        public void Initialize()
        {
            //Initialize pattern
            switch (patternType)
            {
                case PatternType.Single:
                    this.pattern = new BulletPattern(BulletPattern.single, bulletPrefab, interval);
                    break;

                case PatternType.Ring:
                    this.pattern = new BulletPattern(BulletPattern.ring, bulletPrefab, interval);
                    break;

                case PatternType.RingWithGap:
                    this.pattern = new BulletPattern(BulletPattern.ringWithGap, bulletPrefab, interval);
                    break;

                case PatternType.Line:
                    this.pattern = new BulletPattern(BulletPattern.line, bulletPrefab, interval);
                    break;

                case PatternType.LineWithGap:
                    this.pattern = new BulletPattern(BulletPattern.lineWithGap, bulletPrefab, interval);
                    break;

                case PatternType.BentLine:
                    this.pattern = new BulletPattern(BulletPattern.bentLine, bulletPrefab, interval);
                    break;
            }

            //Initialize behaviour
            //Gives all extra arguments for all behviours regardless of pattern type, if anything breaks in the code it'll be this
            switch (behaviourType)
            {
                case BehaviourType.None:
                    this.behaviour = () => pattern.run(bulletSpeed, spawnPoint.position, direction, extraArgInt, extraArgFloat1, extraArgFloat2, extraArgFloat3);
                    break;

                case BehaviourType.Aimed:
                    this.behaviour = () => BulletSpawnBehaviours.aimed(pattern, spawnPoint.position, bulletSpeed, target.position, extraArgInt, extraArgFloat1, extraArgFloat2, extraArgFloat3);
                    break;

                case BehaviourType.Spiral:
                    counter = new float[1] { 0f };
                    this.behaviour = () => BulletSpawnBehaviours.spiral(pattern, spawnPoint.position, bulletSpeed, intervalAngle, counter, extraArgInt, extraArgFloat1, extraArgFloat2, extraArgFloat3);
                    break;

                case BehaviourType.RandomRotation:
                    this.behaviour = () => BulletSpawnBehaviours.randomRotate(pattern, spawnPoint.position, bulletSpeed, direction - variance, direction + variance, extraArgInt, extraArgFloat1, extraArgFloat2, extraArgFloat3);
                    break;

                case BehaviourType.RandomPosition:
                    this.behaviour = () => BulletSpawnBehaviours.randomPositionLine(pattern, spawnPoint.position, bulletSpeed, direction, -variance, variance, extraArgInt, extraArgFloat1, extraArgFloat2, extraArgFloat3);
                    break;

                case BehaviourType.RandomSpawnPoint:
                    this.behaviour = () => BulletSpawnBehaviours.randomSpawnPoint(pattern, spawnPoints, bulletSpeed, direction, extraArgInt, extraArgFloat1, extraArgFloat2, extraArgFloat3);
                    break;

                case BehaviourType.RandomSpawnPointAimed:
                    this.behaviour = () => BulletSpawnBehaviours.randomSpawnPointAimed(pattern, spawnPoints, bulletSpeed, direction, target.position, extraArgInt, extraArgFloat1, extraArgFloat2, extraArgFloat3);
                    break;
            }
        }

        //Run method
        //Runs the event's behaviour
        public void run()
        {
            if (behaviour != null)
            {
                behaviour();
            }
        }
    }
}
