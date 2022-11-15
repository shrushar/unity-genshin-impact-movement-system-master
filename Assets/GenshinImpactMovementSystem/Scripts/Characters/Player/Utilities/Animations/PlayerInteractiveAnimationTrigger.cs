using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace GenshinImpactMovementSystem
{
    public class PlayerInteractiveAnimationTrigger : PlayerCombatState
    {
        public Rig rig;
        private float targetWeight = 0f;

        public PlayerInteractiveAnimationTrigger(PlayerCombatStateMachine playerCombatStateMachine) :base(playerCombatStateMachine)
        {
            
        }

    
        public void ManualUpdate(Rig rig)
        {
            rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * 10f);
        }

        public void HardSet(Rig rig, bool flag)
        {
            if (flag)
                targetWeight = 1f;
            else
                targetWeight = 0f;
        }
         
    }
}
