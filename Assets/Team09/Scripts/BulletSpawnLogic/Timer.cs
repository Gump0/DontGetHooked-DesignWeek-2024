using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    //Timer class
    //Stores a repeating timer that "rings" every given amount of time
    public class Timer
    {
        //How often the timer should "ring" (in seconds)
        private float interval;
        //Current time (in seconds)
        private float time;

        //Whether the timer has rung since the last time something checked it
        private bool timeReached = false;

        public Timer(float interval)
        {
            this.interval = interval;
        }

        //Get Interval method
        //Returns the interval of the timer (in seconds)
        public float getInterval()
        {
            return interval;
        }

        //Increment method
        //Increases the timer's internal time by the given time
        //Intended to be run in Update()/FixedUpdate() and given Time.deltaTime
        public void increment(float time)
        {
            this.time += time;
            if (this.time > interval)
            {
                timeReached = true;
                this.time -= interval;
            }
        }

        //Is Reached method
        //Returns whether the timer has rung since the last time something checked it
        public bool isReached()
        {
            bool temp = timeReached;
            timeReached = false;
            return temp;
        }
    }
}
