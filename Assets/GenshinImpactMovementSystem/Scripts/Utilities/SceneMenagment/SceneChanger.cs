using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GenshinImpactMovementSystem
{
    public class SceneChanger : MonoBehaviour
    {
       
        public void ChangeScene(int index) 
        {
            SceneManager.LoadSceneAsync(index);
            //SceneManager.LoadScene(index); 
        }
        public void Exit() 
        { 
            Application.Quit(); 
        }
        public void OnTriggerEnter(Collider collider)
        {
            if(collider.tag == "Player")
                ChangeScene(2);
        }
    }
}
