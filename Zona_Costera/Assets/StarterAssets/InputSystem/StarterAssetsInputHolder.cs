using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputHolder : MonoBehaviour
	{
		[Header("Character Input Values")]
        [SerializeField] PlayerControllerScriptable_SO playerControllerScriptable_SO;

        public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private void OnEnable()
		{
			playerControllerScriptable_SO.moveEvent += MoveInput;
			playerControllerScriptable_SO.lookEvent += LookInput;
			playerControllerScriptable_SO.jumpEvent += JumpInput;
			playerControllerScriptable_SO.sprintEvent += SprintInput;
		}

		private void OnDestroy()
		{
            playerControllerScriptable_SO.moveEvent -= MoveInput;
            playerControllerScriptable_SO.lookEvent -= LookInput;
            playerControllerScriptable_SO.jumpEvent -= JumpInput;
            playerControllerScriptable_SO.sprintEvent -= SprintInput;
        }

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}