using System;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class PlayerFallData
    {
        [field: Tooltip("Having higher numbers might not read collisions with shallow colliders correctly.")]

        [field: SerializeField] [field: Range(0f, 10f)] public float FallSpeedLimit { get; private set; } = 10f;
        [field: SerializeField] [field: Range(0f, 100f)] public float MinimumDistanceToBeConsideredHardFall { get; private set; } = 3f;
        [field: SerializeField] public float Gravity { get; private set; } = 9.8f;
        [field: SerializeField] public float GravityMultiplayer { get; private set; } = 1.5f;    
    }
}