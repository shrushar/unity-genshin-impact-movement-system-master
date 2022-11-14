using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class PlayerTeleportState : PlayerToggledState
    {

        public PlayerTeleportState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
        {
        }

       //turn to target
       //get direction to target
       //with a distence to a tsrget apply forse mode
       // apply to a rigid body with forse
        
        public override void Exit()
        {

        }
        // Update is called once per frame
        public override void Update()
        {
        
        }
        public override void Enter()
        {
            stateMachine.Player.Input.PlayerActions.Movement.Disable();
            base.Enter();

        }
        private void DashToTarget()
        {

        }
    }
}
