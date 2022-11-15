
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
            Debug.Log("Look On Target");
            targetCamera = stateMachine.Player.CameraTargetRecenteringUtility;
            targetCamera.VirtualCamera.Priority = 11;
            stateMachine.InteractiveAnimation.HardSet(stateMachine.Player.targetFollowTrigger, true);
            Target = findTheNearestTarget.FindNearest(stateMachine.Player.targets, stateMachine.Player.transform.position);
            stateMachine.Player.setTarget.AddTarget(Target);
            AddInputActionsCallbacks();



            //ase.Enter();
        }
        public override void Exit()
        {
            //base.Exit();
            Debug.Log("Stop looking on target");
            stateMachine.InteractiveAnimation.HardSet(stateMachine.Player.targetFollowTrigger, false);
            RemoveInputActionsCallbacks();
            targetCamera.VirtualCamera.Priority = 1;

            
        }
        public override void Update()
        {
            base.Update();
            
            if (stateMachine.Player.targets.Count == 0)
                Exit();
            if (!stateMachine.Player.Input.PlayerActions.LookOnTarget.IsPressed())
                if (Target != null)
                    stateMachine.ChangeState(stateMachine.CombatState);
                
            //if (!stateMachine.Player.Input.PlayerActions.LookOnTarget.IsPressed())



        }
        
        public override void PhysicsUpdate()
        {
            
            base.PhysicsUpdate();

            
        }
        protected override void AddInputActionsCallbacks()
        {
            //base.AddInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.DashToTarget.started += DashToTarget_started;
        }
        protected override void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.LookOnTarget.canceled -= LookOnTargetCanceled;
            stateMachine.Player.Input.PlayerActions.DashToTarget.started -= DashToTarget_started;
        }


        protected override void LookOnTargetCanceled(InputAction.CallbackContext context)
        {
            base.LookOnTargetCanceled(context);
            targetCamera.VirtualCamera.Priority = 1;
            stateMachine.InteractiveAnimation.HardSet(stateMachine.Player.targetFollowTrigger, false);
        }
        
    }
}
