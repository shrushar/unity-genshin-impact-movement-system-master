using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class RestartScene : MonoBehaviour
    {
        

        public int SceneIndex;
        private bool Ibeen;
        void Start()
        {
            Ibeen = false;
        }

        
        void Update()
        {
        
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player"&&!Ibeen)
            {
                Ibeen = true;
                SceneTransition.SwitchToScene(SceneIndex);
            }
        }
    }
}
