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

        float turnSmoothVelocity;



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
            
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
            stateMachine.Player.Input.PlayerActions.Jump.Enable();

            stateMachine.Player.Input.PlayerActions.LookOnTarget.Enable();

            StopAnimation(stateMachine.Player.AnimationData.TeleportingHash);

            stateMachine.Player.movementStateMachine.ReusableData.ShouldTeleport = false;
            base.Exit();
        }
        public override void Enter()
        {
            
            StopAnimation(stateMachine.Player.AnimationData.GroundedParameterHash);
            StopAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.TeleportingHash);

            stateMachine.Player.Input.PlayerActions.Movement.Disable();
            stateMachine.Player.Input.PlayerActions.LookOnTarget.Disable();
            stateMachine.Player.Input.PlayerActions.Jump.Disable();
            stateMachine.Player.movementStateMachine.ReusableData.ShouldTeleport = true;

            bufferElypsed = 0f;
            buffer = 1f;

            //DashToTarget();

        }
        public override void Update()
        {
            stateMachine.Player.setTarget.AddTarget(Target);
            base.Update();


        }
        public override void PhysicsUpdate()
        {
            RotateTowardsTargetRotation();

            DashToTarget();




            base.PhysicsUpdate();
        }
        
        private void DashToTarget()
        {


            Vector3 lastPosition = stateMachine.Player.Rigidbody.position;

            bool distReached = Vector3.Distance(lastPosition, stateMachine.Player.targetPivot.transform.position) < 1f;
            bool isTargetInAir = stateMachine.Player.targetPivot.transform.position.y > lastPosition.y;

            Vector3 direction = Vector3.MoveTowards(lastPosition, stateMachine.Player.targetPivot.transform.position, 5f * Time.deltaTime);
            //Vector3 direction = (stateMachine.Player.targetPivot.transform.position - stateMachine.Player.Rigidbody.position);
            stateMachine.Player.Rigidbody.velocity = (direction - lastPosition) / Time.deltaTime;
            stateMachine.Player.Rigidbody.MovePosition(direction);
            if (distReached)
                if (isTargetInAir)
                    stateMachine.movementStateMachine.ChangeState(stateMachine.movementStateMachine.FallingState);
                else
                    stateMachine.ChangeState(stateMachine.CombatState);


        }
       
        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            Vector3 direction = ( stateMachine.Player.targetPivot.transform.position - stateMachine.Player.Rigidbody.position);
            direction.y = 0;
            float directionYAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg;
            
            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, directionYAngle, ref stateMachine.movementStateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, Time.deltaTime*10f);
            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            stateMachine.Player.Rigidbody.rotation = targetRotation;
        }
       
        
    }
}
