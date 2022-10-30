using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GenshinImpactMovementSystem
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private Vector3 playerPositionOnEnter;

        bool isCharacterFalling;
   

        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);

            //stateMachine.ReusableData.MovementSpeedModifier = 0f;

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
            Debug.Log(isCharacterFalling);
            if (isCharacterFalling)
                return;
            else
                stateMachine.ChangeState(stateMachine.LightLandingState);

        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            stateMachine.Player.Rigidbody.velocity = JumpUpdate();
        }
        private Vector3 JumpUpdate()
        {
            Vector3 gravity = Vector3.zero;
            Vector3 lastVelocity = stateMachine.Player.Rigidbody.velocity;
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();
            if(lastVelocity.y >= -airborneData.FallData.FallSpeedLimit)
                gravity = Vector3.down * stateMachine.Player.Data.AirborneData.FallData.Gravity * stateMachine.Player.Data.AirborneData.FallData.GravityMultiplayer * Time.deltaTime;
            else 
                return new Vector3(0f, -airborneData.FallData.FallSpeedLimit, 0f);
          
            Vector3 verticalVelocity = lastVelocity + gravity; ;
            stateMachine.ReusableData.CurrentVerticalVelocity = verticalVelocity;
            return verticalVelocity;
        }
        
        protected override void ResetSprintState()
        {
        }

        protected override void OnContactWithGround(Collider collider)
        {
           
            isCharacterFalling = false;
            stateMachine.ChangeState(stateMachine.LightLandingState);

        }
    }
}