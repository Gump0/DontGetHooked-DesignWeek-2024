using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team09
{
    public class EventManagerManager : MicrogameEvents
    {
        public GameObject eventManager30seconds;
        public GameObject eventManager15seconds;
        public GameObject eventManager5seconds;

        protected override void OnGameStart()
        {
            eventManager30seconds.GetComponent<EventManager>().Activate();
        }

        protected override void OnFifteenSecondsLeft()
        {
            eventManager30seconds.GetComponent<EventManager>().Deactivate();
            eventManager15seconds.GetComponent<EventManager>().Activate();
        }

        protected override void OnFiveSecondsLeft()
        {
            eventManager15seconds.GetComponent<EventManager>().Deactivate();
            eventManager5seconds.GetComponent<EventManager>().Activate();
        }
    }
}
