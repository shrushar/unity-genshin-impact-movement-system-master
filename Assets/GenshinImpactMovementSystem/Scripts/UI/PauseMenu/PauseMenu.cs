using Cinemachine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{

       
    public class PauseMenu : MonoBehaviour
    {
        
        [SerializeField] private InputActionReference MenuToggleInputAction;
        [SerializeField] private CinemachineInputProvider inputProvider;
        [SerializeField] GameObject menuUI;

        [SerializeField] private bool disableCameraLookOnCursorVisible;
        [SerializeField] private bool disableCameraZoomOnCursorVisible;

        [SerializeField] private bool fixedCinemachineVersion;
        void Awake()
        {
            MenuToggleInputAction.action.started += Action_started;
        }

        private void Action_started(InputAction.CallbackContext context)
        {
            
            ToggleCursor();
        }
        private void OnEnable()
        {
            MenuToggleInputAction.asset.Enable();
        }

        private void OnDisable()
        {
            MenuToggleInputAction.asset.Disable();
            MenuToggleInputAction.action.started -= Action_started;
        }

        public void PauseGame()
        {
            menuUI.SetActive(true);
            Time.timeScale = 0f;

        }

        public void ContinueGame()
        {
            menuUI.SetActive(false);
            Time.timeScale = 1f;
        }
        private void ToggleCursor()
        {
            Cursor.visible = !Cursor.visible;
            PauseGame();
            if (!Cursor.visible)
            {
                
                Cursor.lockState = CursorLockMode.Locked;
                ContinueGame();

                if (!fixedCinemachineVersion)
                {
                    inputProvider.enabled = true;

                    return;
                }

                inputProvider.XYAxis.action.Enable();
                inputProvider.ZAxis.action.Enable();

                return;
            }

            Cursor.lockState = CursorLockMode.None;

            if (!fixedCinemachineVersion)
            {
                inputProvider.enabled = false;

                return;
            }

            if (disableCameraLookOnCursorVisible)
            {
                inputProvider.XYAxis.action.Disable();
            }

            if (disableCameraZoomOnCursorVisible)
            {
                inputProvider.ZAxis.action.Disable();
            }
        }
        
    
    }
}
