using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GenshinImpactMovementSystem
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        [SerializeField] AnimationCurve animationCurve;
        private bool shouldKeepRotating;
        private bool canStartFalling;
        private bool jumpIsQueded;

        float jumpBufferElipsedTime;
        float jumpBuffer;
        float height;

        Vector3 lastVelocity;

        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = stateMachine.ReusableData.MovementSpeedModifier/1.5f;
            animationCurve = stateMachine.Player.Data.AirborneData.JumpData.JumpHeightAcceliration;

            stateMachine.Player.Input.PlayerActions.Movement.Disable();

            stateMachine.ReusableData.MovementDecelerationForce = airborneData.JumpData.DecelerationForce;

            stateMachine.ReusableData.RotationData = airborneData.JumpData.RotationData;

            shouldKeepRotating = true;

            jumpBuffer = stateMachine.Player.Data.AirborneData.JumpData.jumpBuffer;
            height = stateMachine.Player.Data.AirborneData.JumpData.jumpHeight;

            jumpIsQueded = true;
            jumpBufferElipsedTime = 0f;
            Debug.Log("I'm jumping");
            startJump();
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();
            
            ResetVelocity();
            canStartFalling = false;
        }

        public override void Update()
        {
            base.Update();
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
            bool isBufferExpired = jumpBufferElipsedTime < jumpBuffer;


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
            
            startJump();
            base.PhysicsUpdate();

            if(shouldKeepRotating)
            {
                RotateTowardsTargetRotation();
            }

        }

        private void startJump()
        {
            if (jumpIsQueded)
            {
                lastVelocity = stateMachine.Player.Rigidbody.velocity;
                jumpBufferElipsedTime += Time.deltaTime;
                float progress = jumpBufferElipsedTime / jumpBuffer;

                Vector3 newVelocity = new Vector3(lastVelocity.x, animationCurve.Evaluate(progress) * height, lastVelocity.z);
                stateMachine.ReusableData.CurrentVerticalVelocity = newVelocity;

                //newVelocity = GetJumpForceOnSlope(newVelocity);

                stateMachine.Player.Rigidbody.velocity = newVelocity;
            }

            
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

        /*protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
        }*/
    }
}