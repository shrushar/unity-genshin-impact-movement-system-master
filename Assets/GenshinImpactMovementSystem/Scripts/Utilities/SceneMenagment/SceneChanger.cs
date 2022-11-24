using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GenshinImpactMovementSystem
{
    public class SceneChanger : MonoBehaviour
    {

        public int SceneToLoad;
        private Animator anim;

        public void Awake()
        {
            anim = GetComponent<Animator>();
        }
        public void ChangeScene(int index) 
        {

        }
        public void Exit() 
        { 
            Application.Quit(); 
        }
        
        public void RestartCurrentScene()
        {
            
        }

        public void OnPreload()
        {

            anim.SetTrigger("isTriggered");
            
        }

        public void OnFadeComplited()
        {
            anim.SetTrigger("isTriggered");
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
