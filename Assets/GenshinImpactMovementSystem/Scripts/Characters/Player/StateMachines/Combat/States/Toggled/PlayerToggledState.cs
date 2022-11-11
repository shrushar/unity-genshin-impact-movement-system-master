
using System.Security.Cryptography.X509Certificates;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditorInternal;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.Animations.Rigging;

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
            stateMachine.InteractiveAnimation.HardSet(stateMachine.Player.targetFollowTrigger, true);
            stateMachine.InteractiveAnimation.ManualUpdate(stateMachine.Player.targetFollowTrigger);



            base.Enter();
        }
        public override void Exit()
        {
            
            base.Exit();
            targetCamera.VirtualCamera.Priority = 1;

            
        }
        public override void Update()
        {
            base.Update();
            stateMachine.InteractiveAnimation.ManualUpdate(stateMachine.Player.targetFollowTrigger);
            //if (!stateMachine.Player.Input.PlayerActions.LookOnTarget.IsPressed())



        }
        public override void PhysicsUpdate()
        {
            
            base.PhysicsUpdate();

            
        }
        protected override void LookOnTargetCanceled(InputAction.CallbackContext context)
        {
            base.LookOnTargetCanceled(context);
            targetCamera.VirtualCamera.Priority = 1;

            stateMachine.InteractiveAnimation.HardSet(stateMachine.Player.targetFollowTrigger, false);
        }
    }
}
