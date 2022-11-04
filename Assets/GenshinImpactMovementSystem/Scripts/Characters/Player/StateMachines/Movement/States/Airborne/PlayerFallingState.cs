using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GenshinImpactMovementSystem
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private Vector3 playerPositionOnEnter;

        bool isCharacterFalling;
        private bool canMove;

        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //StopAnimation(stateMachine.Player.AnimationData.SJumpParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);

            stateMachine.ReusableData.MovementSpeedModifier =1.5f;
            canMove = airborneData.JumpData.canMove;
            playerPositionOnEnter = stateMachine.Player.transform.position;
            isCharacterFalling = true;
            ResetVerticalVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.ReusableData.CurrentVerticalVelocity = Vector3.zero;
            StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
        }
        public override void Update()
        {
            base.Update();
            if (isCharacterFalling)
                return;
            else
                stateMachine.ChangeState(stateMachine.LightLandingState);

        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            stateMachine.Player.Rigidbody.velocity = UpdateJump();
        }
        /*private Vector3 JumpUpdate()
        {
            Vector3 gravity = Vector3.zero;
            
            Vector3 lastVelocity = stateMachine.Player.Rigidbody.velocity;
            if(lastVelocity.y >= -airborneData.FallData.FallSpeedLimit)
                gravity = Vector3.down * stateMachine.Player.Data.AirborneData.FallData.Gravity * stateMachine.Player.Data.AirborneData.FallData.GravityMultiplayer * Time.deltaTime;
            else 
                return new Vector3(lastVelocity.x, -airborneData.FallData.FallSpeedLimit, lastVelocity.y);
          
            Vector3 verticalVelocity = new Vector3(lastVelocity.x, lastVelocity.y + gravity.y, lastVelocity.z) ;
            if(GetMovementInputDirection() == Vector3.zero)
                    verticalVelocity = new Vector3(0f, lastVelocity.y, 0f) ;

            stateMachine.ReusableData.CurrentVerticalVelocity = verticalVelocity;
            return verticalVelocity;
        }*/
        private Vector3 UpdateJump()
        {
            Vector3 gravity = Vector3.zero;
            Vector3 lastVelocity = stateMachine.Player.Rigidbody.velocity;

            Vector3 jumpVelocityChange;
            if (GetMovementInputDirection() == Vector3.zero || characterJumpInformation.canMove)
            {
                
                lastVelocity = new Vector3 (lastVelocity.x/2, lastVelocity.y, lastVelocity.z/2);
            }
            if (lastVelocity.y > -airborneData.FallData.FallSpeedLimit)
                gravity = Vector3.down * stateMachine.Player.Data.AirborneData.FallData.Gravity * stateMachine.Player.Data.AirborneData.FallData.GravityMultiplayer * Time.deltaTime;
            else
                return jumpVelocityChange = new Vector3(lastVelocity.x, -airborneData.FallData.FallSpeedLimit, lastVelocity.z);
            jumpVelocityChange = lastVelocity + gravity;
            stateMachine.ReusableData.CurrentVerticalVelocity = jumpVelocityChange;
            return jumpVelocityChange;

        }
        protected override void ResetSprintState()
        {
        }

        /*protected override void OnContactWithGround(Collider collider)
        {
           
            isCharacterFalling = false;
            jumpQue = 0;
            stateMachine.ChangeState(stateMachine.IdlingState);

        }*/
        public override void OnAnimationTransitionEvent()
        {
            StopAnimation(stateMachine.Player.AnimationData.SJumpParameterHash);
        }
    }
}