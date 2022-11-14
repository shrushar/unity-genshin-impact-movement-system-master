using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class PlayerTeleportState : PlayerCombatState
    {
        private Vector3 positionToMove;
        private float dist;
        float buffer;
        float bufferElypsed;
        public PlayerTeleportState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
        {
        }

       //turn to target
       //get direction to target
       //with a distence to a tsrget apply forse mode
       // apply to a rigid body with forse
        
        public override void Exit()
        {
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
            base.Exit();
        }
        public override void Enter()
        {
            Debug.Log("Im teleporting");
            stateMachine.Player.Input.PlayerActions.Movement.Disable();
            dist = Vector3.Distance(stateMachine.Player.transform.position, Target.transform.position);
            bufferElypsed = 0f;
            buffer = dist / 10f;

            //DashToTarget();

        }
        public override void Update()
        {
            bufferElypsed += Time.deltaTime;
            if (bufferElypsed >= buffer)
                Exit();
            base.Update();
        }
        public override void PhysicsUpdate()
        {
            

            


            
            base.PhysicsUpdate();
        }
        
        private void DashToTarget()
        {
            
        }
    }
}
