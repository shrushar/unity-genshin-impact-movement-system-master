using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

namespace GenshinImpactMovementSystem
{
    public class Timer : MonoBehaviour
    {
        public float timerStart = 15;
        private int currentScene=1;
        private bool Ibeen;

        [SerializeField] public TextMeshProUGUI textComponent;

        void Start()
        {
            Ibeen = false;
            textComponent.text = String.Empty;
        }

        // Update is called once per frame
        void Update()
        {
            timerStart -= Time.deltaTime;
            textComponent.text = Math.Round(timerStart).ToString();
            if (timerStart <= 0f && !Ibeen)
            {
                timerStart += 1f;
                Ibeen = true;
                SceneTransition.SwitchToScene(currentScene);
            }
               
        }

    }
}
