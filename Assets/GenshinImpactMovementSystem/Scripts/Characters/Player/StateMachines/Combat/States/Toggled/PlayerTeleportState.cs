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


        [SerializeField] private bool _loop;
        public PlayerTeleportState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
        {
        }

       //turn to target
       //get direction to target
       //with a distence to a tsrget apply forse mode
       // apply to a rigid body with forse
        
        public override void Exit()
        {
            Debug.Log("Stop teleporting");
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
            stateMachine.Player.Input.PlayerActions.LookOnTarget.Enable();
            base.Exit();
        }
        public override void Enter()
        {
            Debug.Log("Im teleporting");
            stateMachine.Player.Input.PlayerActions.Movement.Disable();
            stateMachine.Player.Input.PlayerActions.LookOnTarget.Disable();


            bufferElypsed = 0f;
            buffer = 1f;

            //DashToTarget();

        }
        public override void Update()
        {
            stateMachine.Player.setTarget.AddTarget(Target);
            /*bufferElypsed += Time.deltaTime;
            if (bufferElypsed >= buffer)
                stateMachine.ChangeState(stateMachine.CombatState);*/
            base.Update();

            
        }
        public override void PhysicsUpdate()
        {

            Vector3 lastPosition = stateMachine.Player.Rigidbody.position;

            bool distReached = Vector3.Distance(lastPosition, stateMachine.Player.targetPivot.transform.position) < 1f;
            bool isTargetInAir = stateMachine.Player.targetPivot.transform.position.y > lastPosition.y;
            
            Vector3 newPos = Vector3.MoveTowards(lastPosition, stateMachine.Player.targetPivot.transform.position, 5f * Time.deltaTime);

            stateMachine.Player.Rigidbody.velocity = (newPos - lastPosition) / Time.deltaTime;
            stateMachine.Player.Rigidbody.MovePosition(newPos);
            if (distReached)
                if (isTargetInAir)
                    stateMachine.movementStateMachine.ChangeState(stateMachine.movementStateMachine.FallingState);
                else 
                    stateMachine.ChangeState(stateMachine.CombatState);
                        


            
            base.PhysicsUpdate();
        }
        
        private void DashToTarget()
        {
            /*Vector3 dashDirection = stateMachine.Player.Rigidbody.transform.forward;
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                UpdateTargetRotation(GetMovementInputDirection());

                dashDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
            }
            dashBufferEvalute += Time.deltaTime / dashBuffer;
            float movementSpeed = GetMovementSpeed(false) * stateMachine.Player.Data.GroundedData.DashData.DashAcceleration.Evaluate(dashBuffer);
            stateMachine.Player.Rigidbody.velocity = dashDirection * movementSpeed;*/

            Vector3 teleportDirection = stateMachine.Player.Rigidbody.transform.forward;
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
            teleportDirection = Vector3.MoveTowards(stateMachine.Player.Rigidbody.transform.position, Target.transform.position, 10f*Time.deltaTime);

            

            
        }

        protected float UpdateTargetRotation(Vector3 direction) 
        {
            float directionAngle = GetDirectionAngle(direction);

            //UpdateTargetRotationData(directionAngle);

            return directionAngle;
        }
        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }
    }
}
