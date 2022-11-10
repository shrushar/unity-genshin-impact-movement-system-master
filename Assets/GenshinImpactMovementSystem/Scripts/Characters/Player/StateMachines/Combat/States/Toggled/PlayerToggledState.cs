
using System.Security.Cryptography.X509Certificates;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditorInternal;
using Unity.IO.LowLevel.Unsafe;

namespace GenshinImpactMovementSystem
{

    public class PlayerToggledState : PlayerCombatState
    {
        PlayerCameraRecenteringUtility targetCamera;
        public PlayerToggledState(PlayerCombatStateMachine playerCombatStateMachine):base(playerCombatStateMachine)
        {

        }
        public override void Enter()

        {
            targetCamera = stateMachine.Player.CameraTargetRecenteringUtility;
            targetCamera.VirtualCamera.Priority = 11;
            base.Enter();
        }
        public override void Exit()
        {
            
            base.Exit();
            targetCamera.VirtualCamera.Priority = 1;

            
        }
        public void Update()
        {

            //if (!stateMachine.Player.Input.PlayerActions.LookOnTarget.IsPressed())
                


        }
        public override void PhysicsUpdate()
        {
            if (!stateMachine.Player.Input.PlayerActions.LookOnTarget.IsPressed())
                Exit();
            base.PhysicsUpdate();

        }
        protected override void LookOnTargetCanceled(InputAction.CallbackContext context)
        {
            base.LookOnTargetCanceled(context);
            targetCamera.VirtualCamera.Priority = 1;
        }
    }
}
