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


        [SerializeField] GameObject _cutsene;
        [SerializeField] Transform[] _wayPoints;

        private int _currentWayPoint = 0;
        private NavMeshAgent agent;

        public bool cusIsActive;

        

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            _cutsene.SetActive(false);

            agent.autoBraking = false;

            GotoNextPoint();
        }
        
        
        void Update()
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                if(!cusIsActive)
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
            if(collider.tag == "Player")
            {
                _cutsene.SetActive(true);
                cusIsActive = true;
            }
        }
    }
}
