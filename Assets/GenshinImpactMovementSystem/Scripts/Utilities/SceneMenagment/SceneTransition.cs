using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GenshinImpactMovementSystem
{
    public class SceneTransition : MonoBehaviour
    {
        private static SceneTransition instance;
        private static bool shouldPlayOpeningAnimation = false;

        private Animator componentAnimator;

        private AsyncOperation loadingSceneOperator;


        public static void SwitchToScene(int sceneIndex)
        {
            Time.timeScale = 1f;
            instance.componentAnimator.SetTrigger("sceneStart");

            instance.loadingSceneOperator = SceneManager.LoadSceneAsync(sceneIndex);
            instance.loadingSceneOperator.allowSceneActivation = false;
        }
        void Start()
        {
            instance = this;

            shouldPlayOpeningAnimation = false;
            componentAnimator = GetComponent<Animator>();
            componentAnimator.SetTrigger("sceneOut");
        }


        public void OnAnimationOver()
        {
            shouldPlayOpeningAnimation = true;
            instance.loadingSceneOperator.allowSceneActivation = true;
        }

        void Update()
        {
        
        }
    }
}
