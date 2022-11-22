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
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
            speed = stateMachine.ReusableData.MovementSpeedModifier / 2f;
            if (speed == 0)
            {
                speed = 3f;
            }
            
            //stateMachine.ReusableData.MovementSpeedModifier = speed;
            
            animationCurve = stateMachine.Player.Data.AirborneData.JumpData.JumpHeightAcceliration;

            //stateMachine.Player.Input.PlayerActions.Movement.Disable();

            //stateMachine.ReusableData.MovementDecelerationForce = airborneData.JumpData.DecelerationForce;

            stateMachine.ReusableData.RotationData = airborneData.JumpData.RotationData;

            shouldKeepRotating = true;
            canMove = GetMovementInputDirection() == Vector3.zero;
            stateMachine.Player.Data.AirborneData.JumpData.canMove = canMove; 

            speed = stateMachine.Player.Data.AirborneData.JumpData.speed;
            
            height = stateMachine.Player.Data.AirborneData.JumpData.jumpHeight;
            //jumpBuffer = height / speed;
            jumpBufferElipsedTime = 0f;
            //speed = height/jumpBuffer;

            speed = stateMachine.Player.Data.AirborneData.JumpData.speed;

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
            canMove = GetMovementInputDirection() == Vector3.zero;
            stateMachine.Player.Data.AirborneData.JumpData.canMove = canMove;

            bool isBufferExpired = jumpBufferElipsedTime < 1;


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

                jumpBufferElipsedTime += Time.deltaTime*speed/2f;
                
                Vector3 newVelocity;
                if (!canMove)
                {
                    newVelocity = 
                    new Vector3(lastVelocity.x/2f, animationCurve.Evaluate(jumpBufferElipsedTime) * height, lastVelocity.z/2f);
                } else
                {
                    newVelocity = 
                    new Vector3(0f, animationCurve.Evaluate(jumpBufferElipsedTime) *height, 0f);
                }
                
                stateMachine.ReusableData.CurrentVerticalVelocity = newVelocity;

                newVelocity = GetJumpForceOnSlope(newVelocity);

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

        public override void OnAnimationEnterEvent()
        {
            Debug.Log("In trigger");
            stateMachine.Player.audioData.PlaySoundOnJump();
        }

        protected override void ResetSprintState()
        {
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {

        }
    }
}