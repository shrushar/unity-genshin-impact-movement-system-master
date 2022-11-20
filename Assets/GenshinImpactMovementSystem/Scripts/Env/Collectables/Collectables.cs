using System;
using UnityEngine;
using UnityEngine.Events;

namespace GenshinImpactMovementSystem
{
    public class Collectables : MonoBehaviour
    {
        

        public static event Action collected;

       

        


        public void Awake()
        {
            
            
        }

        public void OnTriggerEnter(Collider collider)
        {
            if(collider.tag == "Player")
            {
                collect();
            }
        }

        public void collect()
        {
            

            this.gameObject.SetActive(false);
            
            Debug.Log("collected");

            collected?.Invoke();
        }


    }
}
