using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class CollectableMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _waitTime;
        [SerializeField] private bool _loop;

        private int _currentWaypoint = 0;
        private int _increment = 1;
        private float _elapsedWaitTime;

        public Vector3 Velocity { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 waypointPosition = _waypoints[_currentWaypoint].position;
            Vector3 lastPosition = _rigidbody.position;

            bool destinationReached = Vector3.Distance(waypointPosition, lastPosition) < 0.1f;

            Vector3 newPosition = Vector3.MoveTowards(lastPosition, waypointPosition, Time.deltaTime * _moveSpeed);

            Velocity = (newPosition - lastPosition) / Time.deltaTime;

            _rigidbody.MovePosition(newPosition);

            if (!destinationReached)
                return;

            _elapsedWaitTime += Time.deltaTime;

            if (_elapsedWaitTime > _waitTime)
            {
                _elapsedWaitTime = 0f;
                PeekNextWaypoint();
            }

            void PeekNextWaypoint()
            {
                if (_loop)
                {
                    _currentWaypoint += 1;
                }
                else
                {
                    _currentWaypoint += _increment;
                    if (_currentWaypoint == _waypoints.Length - 1 || _currentWaypoint == 0)
                        _increment = -_increment;
                }

                _currentWaypoint %= _waypoints.Length;
            }
        }
    }
}
