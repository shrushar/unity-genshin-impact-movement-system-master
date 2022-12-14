using System.Collections.Generic;
using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

namespace GenshinImpactMovementSystem
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerResizableCapsuleCollider))]
    public class Player : MonoBehaviour
    {

        public static event Action StartDialogue;

        [field: Header("References")]
        [field: SerializeField] public PlayerSO Data { get; private set; }

        [field: Header("Collisions")]
        [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

        [field: Header("Camera")]
        [field: SerializeField] public PlayerCameraRecenteringUtility CameraRecenteringUtility { get; private set; }
        [field: SerializeField] public PlayerCameraRecenteringUtility CameraTargetRecenteringUtility { get; private set; }
        

        [field: Header("Animations")]
        [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

        [field: SerializeField] public InteractiveAnimationData interctiveAnimationData { get; private set; }

        [field: Header("Sounds")]
        
        [field: SerializeField] public AudioData audioData { get; private set; }

        public Rig targetFollowTrigger { get; private set; }


        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public PlayerInput Input { get; private set; }
        public PlayerResizableCapsuleCollider ResizableCapsuleCollider { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        [field: SerializeField] public GameObject targetPivot { get; private set; }
        public SetTarget setTarget { get; private set; }
        public List<GameObject> targets = new List<GameObject>();


        public PlayerMovementStateMachine movementStateMachine;
        private PlayerCombatStateMachine combatStateMachine;

        private void Awake()
        {
            CameraRecenteringUtility.Initialize();
            AnimationData.Initialize();

            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();
            

            Input = GetComponent<PlayerInput>();
            ResizableCapsuleCollider = GetComponent<PlayerResizableCapsuleCollider>();

            

            targetFollowTrigger = interctiveAnimationData.TargetFollowTrigger.GetComponent<Rig>();
            targetFollowTrigger.weight = 0;

            setTarget = targetPivot.GetComponent<SetTarget>();

            MainCameraTransform = Camera.main.transform;

            movementStateMachine = new PlayerMovementStateMachine(this);
            combatStateMachine = new PlayerCombatStateMachine(this);

            
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
            combatStateMachine.ChangeState(combatStateMachine.CombatState);
        }

        private void Update()
        {
            movementStateMachine.HandleInput();


            movementStateMachine.Update();
            combatStateMachine.Update();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
            combatStateMachine.PhysicsUpdate();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Bot")
                StartDialogue?.Invoke();
            movementStateMachine.OnTriggerEnter(collider);
            combatStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            movementStateMachine.OnTriggerExit(collider);
            combatStateMachine.OnTriggerExit(collider);
        }
        private void OnTriggerStay(Collider collider)
        {
            combatStateMachine.OnTriggerStay(collider);
        }
        public void OnMovementStateAnimationEnterEvent()
        {
            movementStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            movementStateMachine.OnAnimationExitEvent();
        }

        public void OnMovementStateAnimationTransitionEvent()
        {
            movementStateMachine.OnAnimationTransitionEvent();
        }
        /*private void OnDrawGizmos()
        {
            Vector3 targetProjection = new Vector3(targetPivot.transform.position.x, 0, targetPivot.transform.position.z);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Rigidbody.position, targetProjection);
        }*/
    }
}