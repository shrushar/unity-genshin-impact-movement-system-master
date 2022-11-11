using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerCombatState : IState
    {
        public PlayerCombatStateMachine stateMachine;

        public PlayerCombatState(PlayerCombatStateMachine playerCombatStateMachine)
        {
            
            stateMachine = playerCombatStateMachine;
            
        }
        public virtual void Enter()
        {
            Debug.Log("StartCombat");
            
            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            Debug.Log("StopCombat");
            RemoveInputActionsCallbacks();
        }
        public virtual void Update()
        {

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

        public void OnTriggerEnter(Collider collider)
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerExit(Collider collider)
        {
            throw new System.NotImplementedException();
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
            
        }

        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.LookOnTarget.started -= OnLookOnTarget;
            stateMachine.Player.Input.PlayerActions.LookOnTarget.canceled -= OnLookOnTarget;
        }

        protected virtual void OnLookOnTarget(InputAction.CallbackContext context)
        {
            if(stateMachine.Player.Input.PlayerActions.LookOnTarget.IsPressed())
                stateMachine.ChangeState(stateMachine.ToggledState);
        }

        
        
    }
}
