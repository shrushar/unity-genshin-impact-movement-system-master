using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GenshinImpactMovementSystem
{
    public class FindTheNearestTarget
    {
        
        //[SerializeField] private List<GameObject> targets;
       
       // private GameObject theNearestTarget;
        //float minDist = Mathf.Infinity;

        

       
        public GameObject FindNearest(List<GameObject> targets, Vector3 PlayerPos)
        {   
            
            GameObject theNearestTarget = null;
            float minDist = Mathf.Infinity;
            
                foreach (GameObject target in targets)
                {
                    float dist = Vector3.Distance(target.transform.position, PlayerPos);
                    if(dist < minDist)
                    {
                        theNearestTarget = target;
                        minDist = dist;
                    }
                }
                Debug.Log(theNearestTarget == null);
            
            return theNearestTarget;
        }
    }
}
