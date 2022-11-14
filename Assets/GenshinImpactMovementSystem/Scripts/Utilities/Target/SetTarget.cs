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

            Debug.Log(targetToFollow == null);
            if(targetToFollow != null)
            {
                gameObject.transform.position = targetToFollow.transform.position;
            }  
            //gameObject.transform.position = targetToFollow.transform.position;
            /*ConstraintSource targetSource = new ConstraintSource() { 
                sourceTransform = target.GetComponent<Transform>(), 
                weight = 1
            };*/
            //PositionConstraint.AddSource(targetSource);

            //PositionConstraint.SetSource(1,targetSource);
            
        }
        public void RemoveTarget(GameObject target)
        {
            
            //PositionConstraint.RemoveSource(1);
        }

    }
}
