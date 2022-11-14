using Cinemachine;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerToggleStateMachine
    {

        
        PlayerCombatStateMachine stateMachine;
        public PlayerToggleStateMachine(PlayerCombatStateMachine playerCombatStateMachine)
        {
            stateMachine = playerCombatStateMachine;
        }
        public void Enter()
        {
            
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
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

        public void PhysicsUpdate()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void SwitchCamera()
        {

        }
         private void InitializeData()
        {

        }
       
    }
}
