using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace team09
{
    public class EventManager : MonoBehaviour
    {
        //Should the events be randomized or done in order
        //If true: one event will be selected randomly when the prior event ends (can only run one event at a time)
        //If false: events will begin at their set start time and end when their duration is complete
        public bool randomized;

        //List of bullet events
        public List<BulletEvent> events;

        //Time at which the manager is initialized
        private double startTime;
        //The currently playing event
        //Only used in randomized mode
        private BulletEvent currentEvent;

        // Start is called before the first frame update
        void Start()
        {
            //Set start time
            startTime = Time.timeAsDouble;

            //Initialize bullet events
            foreach (BulletEvent e in events)
            {
                e.Initialize();
            }
        }

        // Update is called once per frame
        void Update()
        {
            double activeTime = Time.timeAsDouble - startTime;

            if (randomized)
            {
                //If there is no currently playing event, or the duration of the current event has completed, choose a new random event
                if (currentEvent == null || activeTime > currentEvent.startTime + currentEvent.duration)
                {
                    currentEvent = events[UnityEngine.Random.Range(0, events.Count)];
                    currentEvent.startTime = (float)activeTime;
                }

                //Run the current event
                currentEvent.run();
            }
            else
            {
                foreach (BulletEvent e in events)
                {
                    //If the event has already completed, ignore it
                    if (activeTime > e.startTime + e.duration) continue;
                    //If the event has started, run it
                    if (activeTime - startTime > e.startTime)
                    {
                        e.run();
                    }
                }
            }
        }
    }

    //Custom editor made following this tutorial: https://www.youtube.com/watch?v=RImM7XYdeAc
    //Considering the scope of the project, just pretend this is a magic black box and don't touch it
    //I don't wanna go through and comment this mess
#if UNITY_EDITOR
    [CustomEditor(typeof(EventManager)), CanEditMultipleObjects]
    public class EventManagerEditor : Editor
    {
        private bool listToggle;
        private List<bool> elementToggles = new List<bool>();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EventManager manager = (EventManager)target;

            EditorGUILayout.BeginHorizontal();
            listToggle = EditorGUILayout.Foldout(listToggle, "Events", true);
            int size = Mathf.Max(0, EditorGUILayout.IntField(manager.events.Count));
            EditorGUILayout.EndHorizontal();

            if (listToggle)
            {
                while (size > manager.events.Count)
                {
                    manager.events.Add(null);
                }
                while (size < manager.events.Count)
                {
                    manager.events.RemoveAt(manager.events.Count - 1);
                }

                while (manager.events.Count > elementToggles.Count)
                {
                    elementToggles.Add(false);
                }
                while (manager.events.Count < elementToggles.Count)
                {
                    elementToggles.RemoveAt(elementToggles.Count - 1);
                }

                for (int i = 0; i < manager.events.Count; i++)
                {
                    elementToggles[i] = EditorGUILayout.Foldout(elementToggles[i], "Event " + i, true);

                    if (elementToggles[i])
                    {
                        DrawPattern(manager.events[i]);

                        DrawBehaviour(manager.events[i]);

                        EditorGUILayout.LabelField("Timing");

                        if (!manager.randomized)
                        {
                            manager.events[i].startTime = EditorGUILayout.FloatField("Start", manager.events[i].startTime);
                        }
                        manager.events[i].duration = EditorGUILayout.FloatField("Duration", manager.events[i].duration);

                        EditorGUILayout.Space();
                    }
                }
            }
        }

        private static void DrawPattern(BulletEvent bulletEvent)
        {
            EditorGUILayout.LabelField("Pattern");

            bulletEvent.patternType = (PatternType)EditorGUILayout.EnumPopup("Bullet Pattern Type", bulletEvent.patternType);

            bulletEvent.spawnPoint = (Transform)EditorGUILayout.ObjectField("Spawn Point", bulletEvent.spawnPoint, typeof(Transform), true);
            bulletEvent.bulletSpeed = EditorGUILayout.FloatField("Bullet Speed", bulletEvent.bulletSpeed);
            bulletEvent.interval = EditorGUILayout.FloatField("Interval", bulletEvent.interval);
            bulletEvent.bulletPrefab = (GameObject)EditorGUILayout.ObjectField("Bullet Type", bulletEvent.bulletPrefab, typeof(GameObject), true);

            switch (bulletEvent.patternType)
            {
                case PatternType.Ring:
                    bulletEvent.extraArgInt = EditorGUILayout.IntField("Ring Density", bulletEvent.extraArgInt);
                    break;

                case PatternType.RingWithGap:
                    bulletEvent.extraArgInt = EditorGUILayout.IntField("Ring Density", bulletEvent.extraArgInt);
                    bulletEvent.extraArgFloat1 = EditorGUILayout.FloatField("Gap Size", bulletEvent.extraArgFloat1);
                    break;

                case PatternType.Line:
                    bulletEvent.extraArgInt = EditorGUILayout.IntField("Line Density", bulletEvent.extraArgInt);
                    bulletEvent.extraArgFloat1 = EditorGUILayout.FloatField("Line Length", bulletEvent.extraArgFloat1);
                    break;

                case PatternType.LineWithGap:
                    bulletEvent.extraArgInt = EditorGUILayout.IntField("Line Density", bulletEvent.extraArgInt);
                    bulletEvent.extraArgFloat1 = EditorGUILayout.FloatField("Line Length", bulletEvent.extraArgFloat1);
                    bulletEvent.extraArgFloat2 = EditorGUILayout.FloatField("Gap Position", bulletEvent.extraArgFloat2);
                    bulletEvent.extraArgFloat3 = EditorGUILayout.FloatField("Gap Size", bulletEvent.extraArgFloat3);
                    break;
            }

            EditorGUILayout.Space();
        }

        private static void DrawBehaviour(BulletEvent bulletEvent)
        {
            EditorGUILayout.LabelField("Behaviour");

            bulletEvent.behaviourType = (BehaviourType)EditorGUILayout.EnumPopup("Bullet Behaviour Type", bulletEvent.behaviourType);

            switch (bulletEvent.behaviourType)
            {
                case BehaviourType.None:
                    bulletEvent.direction = EditorGUILayout.FloatField("Direction", bulletEvent.direction);
                    break;

                case BehaviourType.Aimed:
                    bulletEvent.target = (Transform)EditorGUILayout.ObjectField("Target", bulletEvent.target, typeof(Transform), true);
                    break;

                case BehaviourType.Spiral:
                    bulletEvent.intervalAngle = EditorGUILayout.FloatField("Delta Angle", bulletEvent.intervalAngle);
                    break;

                case BehaviourType.RandomRotation:
                    bulletEvent.direction = EditorGUILayout.FloatField("Direction", bulletEvent.direction);
                    bulletEvent.variance = EditorGUILayout.FloatField("Angle Variance", bulletEvent.variance);
                    break;

                case BehaviourType.RandomPosition:
                    bulletEvent.direction = EditorGUILayout.FloatField("Direction", bulletEvent.direction);
                    bulletEvent.variance = EditorGUILayout.FloatField("Position Variance", bulletEvent.variance);
                    break;
            }

            EditorGUILayout.Space();
        }
    }
#endif
}
