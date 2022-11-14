
using System.Security.Cryptography.X509Certificates;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditorInternal;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.Animations.Rigging;
using System.Collections.Generic;

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
            
            stateMachine.Player.setTarget.AddTarget(findTheNearestTarget.FindNearest(stateMachine.Player.targets, stateMachine.Player.transform.position));
            //Debug.Log(enemies.Count);
            //findTheNearestTarget.FindNearest(enemies, stateMachine.Player.transform.position);




            base.Enter();
        }
        public override void Exit()
        {
            
           
            targetCamera.VirtualCamera.Priority = 1;

            
        }
        public override void Update()
        {
            base.Update();
            stateMachine.InteractiveAnimation.ManualUpdate(stateMachine.Player.targetFollowTrigger);
            if (stateMachine.Player.targets.Count == 0)
                Exit();
            //if (!stateMachine.Player.Input.PlayerActions.LookOnTarget.IsPressed())



        }
        
        public override void PhysicsUpdate()
        {
            
            base.PhysicsUpdate();

            
        }
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            //stateMachine.Player.Input.PlayerActions.Dash.started += Dash_started;
        }

        private void Dash_started(InputAction.CallbackContext context)
        {
            //stateMachine.ChangeState(stateMachine.TeleportState);
        }

        protected override void LookOnTargetCanceled(InputAction.CallbackContext context)
        {
            base.LookOnTargetCanceled(context);
            targetCamera.VirtualCamera.Priority = 1;
            //stateMachine.Player.setTarget.RemoveTarget(target);
            stateMachine.InteractiveAnimation.HardSet(stateMachine.Player.targetFollowTrigger, false);
        }
        
    }
}
