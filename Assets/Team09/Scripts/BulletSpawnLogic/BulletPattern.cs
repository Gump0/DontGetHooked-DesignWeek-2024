using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    //Delegate for storing a bullet spawn behaviour
    public delegate void spawnBehaviour(float bulletSpeed, Vector3 spawnPoint, float direction, Timer timer, GameObject bulletPrefab, params object[] extraArgs);

    public enum PatternType
    {
        Single,
        Ring,
        RingWithGap,
        Line,
        LineWithGap
    }

    //Bullet Pattern class
    //Stores info related to a bullet pattern to be run
    public class BulletPattern
    {
        //Logic for spawning bullets with a given interval, bullet speed, and any extra arguments
        private spawnBehaviour pattern;

        //Bullet for the pattern to spawn
        private GameObject bulletPrefab;

        //Timer that the pattern will use to time its spawns
        private Timer timer;

        public BulletPattern(spawnBehaviour pattern, GameObject bulletPrefab, float interval)
        {
            this.pattern = pattern;
            this.bulletPrefab = bulletPrefab;
            this.timer = new Timer(interval);
        }

        //Get Interval method
        //Returns the interval of the pattern's timer
        public float getInterval()
        {
            return timer.getInterval();
        }

        //Run method
        //Runs the bullet pattern with a given bullet speed, spawn point, direction, and any extra arguments
        public void run(float bulletSpeed, Vector3 spawnPoint, float direction, params object[] extraArgs)
        {
            if (timer != null && pattern != null && bulletPrefab != null)
            {
                if (timer.isReached())
                {
                    pattern(bulletSpeed, spawnPoint, direction, timer, bulletPrefab, extraArgs);
                }

                timer.increment(Time.deltaTime);
            }
        }

        /****Built in spawn patterns****/

        //Single bullet pattern
        //Spawns a single bullet at a time
        public static spawnBehaviour single = (float bulletSpeed, Vector3 spawnPoint, float direction, Timer timer, GameObject bulletPrefab, object[] extraArgs) =>
        {
            GameObject bulletObject = GameObject.Instantiate(bulletPrefab, spawnPoint, Quaternion.Euler(0, 0, direction));
            //Bullet bullet = bulletObject.GetComponent<Bullet>();
            //bullet.initialize(bulletSpeed);
        };

        //Ring bullet pattern
        //Spawns a ring of bullets
        //Extra arguments: int density
        public static spawnBehaviour ring = (float bulletSpeed, Vector3 spawnPoint, float direction, Timer timer, GameObject bulletPrefab, object[] extraArgs) =>
        {
            //Test and grab required extra arguments
            if (extraArgs.Length < 1 || extraArgs[0] is not int)
            {
                Debug.LogError("Improper Parameters given\nRing Bullet Pattern Extra Parameters: int density");
                return;
            }
            int density = (int)extraArgs[0];

            float intervalAngle = 360 / density;
            for (int i = 0; i < 18; i++)
            {
                GameObject bulletObject = GameObject.Instantiate(bulletPrefab, spawnPoint, Quaternion.Euler(0, 0, direction + 20 * i));
                //Bullet bullet = bulletObject.GetComponent<Bullet>();
                //bullet.initialize(bulletSpeed);
            }
        };

        //Ring with gap bullet pattern
        //Spawns a ring of bullets with a gap
        //Extra arguments: int density, float gapSize
        public static spawnBehaviour ringWithGap = (float bulletSpeed, Vector3 spawnPoint, float direction, Timer timer, GameObject bulletPrefab, object[] extraArgs) =>
        {
            //Test and grab required extra arguments
            if (extraArgs.Length < 2 || extraArgs[0] is not int || extraArgs[1] is not float)
            {
                Debug.LogError("Improper Parameters given\nRing Bullet Pattern Extra Parameters: int density, float gapSize");
                return;
            }
            int density = (int)extraArgs[0];
            float gapSize = (float)extraArgs[1];

            float intervalAngle = 360 / density;
            for (int i = 0; i < density; i++)
            {
                float offset = intervalAngle * i;
                if (offset > gapSize / 2 && offset < 360 - gapSize / 2)
                {
                    GameObject bulletObject = GameObject.Instantiate(bulletPrefab, spawnPoint, Quaternion.Euler(0, 0, direction + offset));
                    //Bullet bullet = bulletObject.GetComponent<Bullet>();
                    //bullet.initialize(bulletSpeed);
                }
            }
        };

        public static spawnBehaviour line = (float bulletSpeed, Vector3 spawnPoint, float direction, Timer timer, GameObject bulletPrefab, object[] extraArgs) =>
        {
            //Test and grab required extra arguments
            if (extraArgs.Length < 2 || extraArgs[0] is not int || extraArgs[1] is not float)
            {
                Debug.LogError("Improper Parameters given\nLine Bullet Pattern Extra Parameters: int density, float length");
                return;
            }
            int density = (int)extraArgs[0];
            float length = (float)extraArgs[1];

            float deltaPos = density / length;
            for(int i = 0; i < density; i++)
            {
                float offset = deltaPos * i - length / 2;
                Vector3 pos = spawnPoint + new Vector3(Mathf.Sin(direction * Mathf.Deg2Rad), Mathf.Cos(direction * Mathf.Deg2Rad)) * offset;

                GameObject bulletObject = GameObject.Instantiate(bulletPrefab, pos, Quaternion.Euler(0, 0, direction));
                //Bullet bullet = bulletObject.GetComponent<Bullet>();
                //bullet.initialize(bulletSpeed);
            }
        };

        public static spawnBehaviour lineWithGap = (float bulletSpeed, Vector3 spawnPoint, float direction, Timer timer, GameObject bulletPrefab, object[] extraArgs) =>
        {
            //Test and grab required extra arguments
            if (extraArgs.Length < 4 || extraArgs[0] is not int || extraArgs[1] is not float || extraArgs[2] is not float || extraArgs[3] is not float)
            {
                Debug.LogError("Improper Parameters given\nLine With Gap Bullet Pattern Extra Parameters: int density, float length, float gapPosition, float gapSize");
                return;
            }
            int density = (int)extraArgs[0];
            float length = (float)extraArgs[1];
            float gapPosition = (float)extraArgs[2];
            float gapSize = (float)extraArgs[3];

            float deltaPos = length / density;
            for (int i = 0; i < density; i++)
            {
                float offset = deltaPos * i - length / 2;

                if(offset < gapPosition - gapSize || offset > gapPosition + gapSize)
                {
                    Vector3 pos = spawnPoint + new Vector3(Mathf.Sin(direction * Mathf.Deg2Rad), Mathf.Cos(direction * Mathf.Deg2Rad)) * offset;

                    GameObject bulletObject = GameObject.Instantiate(bulletPrefab, pos, Quaternion.Euler(0, 0, direction));
                    //Bullet bullet = bulletObject.GetComponent<Bullet>();
                    //bullet.initialize(bulletSpeed);
                }
            }
        };
    }
}
