using Cinemachine;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerAirborneState : PlayerMovementState
    {
        public bool jumpIsQueded = true;
        public int jumpQue;
        private int jumpQueueLimit;
        //public CharacterJumpInformation characterJumpInformation;

        public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);
            jumpQueueLimit = airborneData.JumpData.JumpsCount;
            stateMachine.ReusableData.ShouldJump = true;
            /*characterJumpInformation = new CharacterJumpInformation()
            {

                //speed = (stateMachine.ReusableData.MovementSpeedModifier != 0) ? stateMachine.ReusableData.MovementSpeedModifier / 1.5f : 3f,
                speed = 5f,
                animationCurve = stateMachine.Player.Data.AirborneData.JumpData.JumpHeightAcceliration,
                height = stateMachine.Player.Data.AirborneData.JumpData.jumpHeight,
                jumpBuffer = characterJumpInformation.height/characterJumpInformation.speed,
                canMove = GetMovementInputDirection() == Vector3.zero,
                PlayerAnimationDataHash = stateMachine.Player.AnimationData.AirborneParameterHash

            };*/



            ResetSprintState();
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);
            
            


        }
        
        protected virtual void ResetSprintState()
        {

            stateMachine.ReusableData.ShouldSprint = false;
        }

        protected override void OnContactWithGround(Collider collider)
        {
            base.OnContactWithGround(collider);
            StopAnimation(stateMachine.Player.AnimationData.SJumpParameterHash);
            stateMachine.ChangeState(stateMachine.LightLandingState);
            jumpQue = 0;
        }
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
        }
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
        }
        public void OnJumpStarted(InputAction.CallbackContext context)
        {
            UpdateJumpQueue();
            //stateMachine.ChangeState(stateMachine.JumpingState);
        }

        #region JumpCountCheck
        protected virtual void UpdateJumpQueue()
        {
            
            
            ++jumpQue;
            if(jumpQue < jumpQueueLimit)
            {
                
                if (jumpQue > 0)
                {
                    /*characterJumpInformation = new CharacterJumpInformation()
                    {
                        speed = (characterJumpInformation.speed == 0) ? characterJumpInformation.speed = 3f : characterJumpInformation.speed * 1.5f,
                        animationCurve = stateMachine.Player.Data.AirborneData.JumpData.JumpHeightAcceliration,
                        height = stateMachine.Player.Data.AirborneData.JumpData.jumpHeight * 3f,
                        jumpBuffer = stateMachine.Player.Data.AirborneData.JumpData.jumpBuffer,
                        canMove = true,
                        PlayerAnimationDataHash = stateMachine.Player.AnimationData.SJumpParameterHash
                    };*/
                    StartAnimation(stateMachine.Player.AnimationData.SJumpParameterHash);
                }else
                {
                    StopAnimation(stateMachine.Player.AnimationData.SJumpParameterHash);
                }
                stateMachine.ChangeState(stateMachine.JumpingState);
                //RemoveInputActionsCallbacks();
            }
            
        }
        public override void OnAnimationTransitionEvent()
        {
            StopAnimation(stateMachine.Player.AnimationData.SJumpParameterHash);
        }
        public struct CharacterJumpInformation
        {
            public float speed;
            public AnimationCurve animationCurve;
            public float height;
            public float jumpBufferElipsedTime;
            public float jumpBuffer;
            public bool canMove;
            public int PlayerAnimationDataHash;
        }
        #endregion
    }
}