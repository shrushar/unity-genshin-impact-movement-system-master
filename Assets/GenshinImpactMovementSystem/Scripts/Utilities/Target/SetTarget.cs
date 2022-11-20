using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace GenshinImpactMovementSystem
{
    public class SetTarget : MonoBehaviour
    {
        
        PositionConstraint PositionConstraint;
        void Start()
        {
           
        }
        public void AddTarget(GameObject targetToFollow)
        {

            
            if(targetToFollow != null)
            {
                gameObject.transform.position = targetToFollow.transform.position;
            }
            
        }
        public void RemoveTarget(GameObject target)
        {
            
            
        }

    }
}
