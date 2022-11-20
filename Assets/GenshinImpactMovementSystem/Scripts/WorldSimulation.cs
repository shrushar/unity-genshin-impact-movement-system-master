using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

using UnityEngine.Events;

namespace GenshinImpactMovementSystem
{
    public class WorldSimulation : MonoBehaviour
    {
        private Character[] _characters;
        



        //private Collectables[] _collectables;
        private List<Collectables> _collectables;

        private int collected;

        private DialogueSystem _dialogueSystem;

        // Start is called before the first frame update
        private void Awake()
        {
            
            _collectables = FindObjectsOfType<Collectables>().ToList();
            Collectables.collected += Collectables_collected;

            _dialogueSystem = FindObjectOfType<DialogueSystem>();
            
            collected = 0;

        }

        private void Player_StartDialogue()
        {
            _dialogueSystem.ManualUpdate();
        }

        private void Collectables_collected()
        {
            collected++;
        }




    }
}
