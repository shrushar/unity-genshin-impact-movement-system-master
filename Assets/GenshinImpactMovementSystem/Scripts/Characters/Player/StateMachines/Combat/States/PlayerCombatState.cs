using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerCombatState : IState
    {
        public PlayerCombatStateMachine stateMachine;
        public FindTheNearestTarget findTheNearestTarget;



        public GameObject Target;

        
        public PlayerCombatState(PlayerCombatStateMachine playerCombatStateMachine)
        {
            findTheNearestTarget = new FindTheNearestTarget();
            
            stateMachine = playerCombatStateMachine;
            
        }
        public virtual void Enter()
        {
            Debug.Log("In combat");
            //stateMachine.Player.targets.Clear();
            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            Debug.Log("Out of combat");
            RemoveInputActionsCallbacks();
        }
        public virtual void Update()
        {
            stateMachine.Player.targets.Remove(stateMachine.Player.targets.Find(p => p.activeSelf == false));
            stateMachine.InteractiveAnimation.ManualUpdate(stateMachine.Player.targetFollowTrigger);
            
        }
        public void HandleInput()
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationEnterEvent()
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationExitEvent()
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationTransitionEvent()
        {
            throw new System.NotImplementedException();
        }
        protected void StartAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, true);
        }

        protected void StopAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, false);
        }
        public void OnTriggerEnter(Collider collider)
        {
            
            if (stateMachine.Player.LayerData.IsEnemyLayer(collider.gameObject.layer))
            {
                stateMachine.Player.targets.Add(collider.gameObject);

                return;
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            
            if (stateMachine.Player.LayerData.IsEnemyLayer(collider.gameObject.layer))
            {
                 stateMachine.Player.targets.Remove(collider.gameObject);
                return;
            }
        }

        public void OnTriggerStay(Collider collider)
        {
            
        }
        public virtual void PhysicsUpdate()
        {
            
        }


        protected virtual void AddInputActionsCallbacks()
        {
     
            stateMachine.Player.Input.PlayerActions.LookOnTarget.started += OnLookOnTarget;
            stateMachine.Player.Input.PlayerActions.LookOnTarget.canceled += LookOnTargetCanceled;
        }

        protected virtual void LookOnTargetCanceled(InputAction.CallbackContext context)
        {
            PlayerCameraRecenteringUtility targetCamera = stateMachine.Player.CameraTargetRecenteringUtility;
            targetCamera.VirtualCamera.Priority = 1;
        }
        
        public void DashToTarget_started(InputAction.CallbackContext context)
        {
            
            stateMachine.ChangeState(stateMachine.TeleportState);
        }
        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.LookOnTarget.started -= OnLookOnTarget;
            //stateMachine.Player.Input.PlayerActions.LookOnTarget.canceled -= LookOnTargetCanceled;
        }

        protected virtual void OnLookOnTarget(InputAction.CallbackContext context)
        {
             if(stateMachine.Player.targets.Count != 0)
                    stateMachine.ChangeState(stateMachine.ToggledState);
        }

    }
}
