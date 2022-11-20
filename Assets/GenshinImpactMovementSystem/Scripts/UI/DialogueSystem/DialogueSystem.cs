using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GenshinImpactMovementSystem
{
    public class DialogueSystem : MonoBehaviour
    {
        
        public TextMeshProUGUI textComponent;
        public TextMeshProUGUI textNameComponent;

        public string[] lines;
        public string speakerName;
        public float textSpeed;

        private int index;

        private void Awake()
        {
            this.gameObject.SetActive(false);
            Player.StartDialogue += Player_StartDialogue;
        }
        void Start()
        {
            
            textComponent.text = string.Empty;
            textNameComponent.text = string.Empty;
            
            //StartDialogue();
        }

        private void Player_StartDialogue()
        {
            this.gameObject.SetActive(true);
            StartDialogue();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(textComponent.text == lines[index])
                {
                    NextLine();

                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
        public void ManualUpdate()
        {
            StartDialogue();
        }

        public void StartDialogue()
        {

            index = 0;
            textNameComponent.text = speakerName;
            StartCoroutine(TypeLine());
        }

        void NextLine()
        {
            if(index<lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        IEnumerator TypeLine()
        {
            foreach(char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }
}
