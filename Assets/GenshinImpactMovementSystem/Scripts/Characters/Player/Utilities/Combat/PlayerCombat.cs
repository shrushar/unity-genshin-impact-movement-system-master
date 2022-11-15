using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace GenshinImpactMovementSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerCombat : MonoBehaviour
    {
        [field: Header("References")]

        [field: Header("Skills")]

        [field: SerializeField] public GameObject IllusionBlade;

        private void Awake()
        {
            
        }

    }
}
