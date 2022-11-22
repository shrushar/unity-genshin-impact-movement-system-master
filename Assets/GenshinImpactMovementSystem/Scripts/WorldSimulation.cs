using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

using UnityEngine.Events;

namespace GenshinImpactMovementSystem
{
    public class WorldSimulation : MonoBehaviour
    {
        private List<Collectables> _collectables;

        private int collected;

        private DialogueSystem _dialogueSystem;

        [field: SerializeField] public UICounter uiCounter;
        private void Awake()
        {
            
            _collectables = FindObjectsOfType<Collectables>().ToList();
            Collectables.collected += Collectables_collected;
            uiCounter.SetCounter(collected, _collectables.Count);
            _dialogueSystem = FindObjectOfType<DialogueSystem>();
            
            collected = 0;

        }


        private void Collectables_collected()
        {
            collected++;
            uiCounter.SetCounter(collected, _collectables.Count);
        }




    }
}
