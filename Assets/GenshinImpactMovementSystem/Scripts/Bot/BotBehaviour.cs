using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;

namespace GenshinImpactMovementSystem
{
    public class BotBehaviour : MonoBehaviour
    {
        


        [SerializeField] Transform[] _wayPoints;

        private int _currentWayPoint = 0;
        private NavMeshAgent agent;

        public bool IsLoop;

        

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            agent.autoBraking = false;

            GotoNextPoint();
        }
        
        
        void Update()
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }

        private void GotoNextPoint()
        {
            
            if (_wayPoints.Length == 0)
                return;

            
            agent.destination = _wayPoints[_currentWayPoint].position;

            
            _currentWayPoint = (_currentWayPoint + 1) % _wayPoints.Length;
        }

        public void OnTriggerEnter(Collider collider)
        {
            
        }
    }
}
