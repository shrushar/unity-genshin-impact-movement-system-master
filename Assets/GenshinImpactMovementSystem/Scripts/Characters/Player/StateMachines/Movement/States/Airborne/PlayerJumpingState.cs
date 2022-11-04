using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GenshinImpactMovementSystem
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        [SerializeField] AnimationCurve animationCurve;
        private bool shouldKeepRotating;
        private bool canMove;
        

        private float jumpBufferElipsedTime;
        private float jumpBuffer;
        private float height;
        private float speed;

        Vector3 lastVelocity;

        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //speed = stateMachine.ReusableData.MovementSpeedModifier / 2f;
            if (speed == 0)
            {
                speed = 1f;
            }
            stateMachine.ReusableData.MovementSpeedModifier = speed;
            
            animationCurve = stateMachine.Player.Data.AirborneData.JumpData.JumpHeightAcceliration;

            //stateMachine.Player.Input.PlayerActions.Movement.Disable();

            //stateMachine.ReusableData.MovementDecelerationForce = airborneData.JumpData.DecelerationForce;

            stateMachine.ReusableData.RotationData = airborneData.JumpData.RotationData;

            shouldKeepRotating = true;
            canMove = GetMovementInputDirection() == Vector3.zero;
            stateMachine.Player.Data.AirborneData.JumpData.canMove = canMove; 

            jumpBuffer = stateMachine.Player.Data.AirborneData.JumpData.jumpBuffer;
            height = stateMachine.Player.Data.AirborneData.JumpData.jumpHeight;
            jumpBufferElipsedTime = 0f;



            startJump();
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();
            
            ResetVelocity();
            //StopAnimation(stateMachine.Player.AnimationData.SJumpParameterHash);
        }

        public override void Update()
        {
            base.Update();
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
            bool isBufferExpired = jumpBufferElipsedTime < characterJumpInformation.jumpBuffer;


            if (!isBufferExpired)
            {
                stateMachine.ChangeState(stateMachine.FallingState);
            }
            if (stateMachine.Player.Input.PlayerActions.Jump.IsPressed())
                return;
            else
                stateMachine.ChangeState(stateMachine.FallingState);
        }

        public override void PhysicsUpdate()
        {
            
            base.PhysicsUpdate();
            startJump();

            if (shouldKeepRotating)
            {
                RotateTowardsTargetRotation();
            }

        }

        private void startJump()
        {

            
            
                lastVelocity = stateMachine.Player.Rigidbody.velocity;

                jumpBufferElipsedTime += Time.deltaTime;
                float progress = jumpBufferElipsedTime / characterJumpInformation.jumpBuffer * characterJumpInformation.speed;
                Vector3 newVelocity;
                if (!characterJumpInformation.canMove)
                {
                    newVelocity = 
                    new Vector3(lastVelocity.x/1.2f, characterJumpInformation.animationCurve.Evaluate(progress) * characterJumpInformation.height, lastVelocity.z/1.2f);
                } else
                {
                    newVelocity = 
                    new Vector3(0f, characterJumpInformation.animationCurve.Evaluate(progress) * characterJumpInformation.height, 0f);
                }
                
                stateMachine.ReusableData.CurrentVerticalVelocity = newVelocity;

                //newVelocity = GetJumpForceOnSlope(newVelocity);

                stateMachine.Player.Rigidbody.velocity = newVelocity;


        }
        

        private Vector3 GetJumpForceOnSlope(Vector3 jumpForce)
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ResizableCapsuleCollider.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, airborneData.JumpData.JumpToGroundRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                if (IsMovingUp())
                {
                    float forceModifier = airborneData.JumpData.JumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);

                    jumpForce.x += forceModifier;
                    jumpForce.z += forceModifier;
                }

                if (IsMovingDown())
                {
                    float forceModifier = airborneData.JumpData.JumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);

                    jumpForce.y += forceModifier;
                }
            }

            return jumpForce;
        }

          
        
        protected override void ResetSprintState()
        {
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
        }
        
    }
}